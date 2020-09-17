using System;
using System.Numerics;

namespace AsteroidsCore
{
    public sealed class PhysicsService
    {
        #region Methods

        public bool PointAndCircleContact(Vector2 point, CircleShape circle)
        {
            var distX = point.X - circle.Center.X;
            var distY = point.Y - circle.Center.Y;
            var distance = (float) Math.Sqrt(distX * distX + distY * distY);

            return distance <= circle.Radius;
        }

        public bool LineAndLineContact(LineShape lineA, LineShape lineB)
        {
            var uA = ((lineB.PointB.X - lineB.PointA.X) * (lineA.PointA.Y - lineB.PointA.Y)
                      - (lineB.PointB.Y - lineB.PointA.Y) * (lineA.PointA.X - lineB.PointA.X))
                     / ((lineB.PointB.Y - lineB.PointA.Y) * (lineA.PointB.X - lineA.PointA.X)
                        - (lineB.PointB.X - lineB.PointA.X) * (lineA.PointB.Y - lineA.PointA.Y));
            var uB = ((lineA.PointB.X - lineA.PointA.X) * (lineA.PointA.Y - lineB.PointA.Y)
                      - (lineA.PointB.Y - lineA.PointA.Y) * (lineA.PointA.X - lineB.PointA.X))
                     / ((lineB.PointB.Y - lineB.PointA.Y) * (lineA.PointB.X - lineA.PointA.X)
                        - (lineB.PointB.X - lineB.PointA.X) * (lineA.PointB.Y - lineA.PointA.Y));

            return uA >= 0 && uA <= 1 && uB >= 0 && uB <= 1;
        }

        public bool LineAndPointContact(LineShape line, Vector2 point)
        {
            var d1 = Vector2.Distance(point, line.PointA);
            var d2 = Vector2.Distance(point, line.PointB);

            var lineLen = Vector2.Distance(line.PointA, line.PointB);

            const float buffer = 0.1f;

            return d1 + d2 >= lineLen - buffer && d1 + d2 <= lineLen + buffer;
        }

        public bool LineAndCircleContact(LineShape line, CircleShape circle)
        {
            var inside1 = PointAndCircleContact(line.PointA, circle);
            var inside2 = PointAndCircleContact(line.PointB, circle);

            if (inside1 || inside2) return true;

            var distX = line.PointA.X - line.PointB.X;
            var distY = line.PointA.Y - line.PointB.Y;
            var len = (float) Math.Sqrt(distX * distX + distY * distY);

            var dot = ((circle.Center.X - line.PointA.X) * (line.PointB.X - line.PointA.X)
                       + (circle.Center.Y - line.PointA.Y) * (line.PointB.Y - line.PointA.Y))
                      / (float) Math.Pow(len, 2);

            var closestX = line.PointA.X + dot * (line.PointB.X - line.PointA.X);
            var closestY = line.PointA.Y + dot * (line.PointB.Y - line.PointA.Y);

            var onSegment = LineAndPointContact(line, new Vector2(closestX, closestY));

            if (!onSegment) return false;

            distX = closestX - circle.Center.X;
            distY = closestY - circle.Center.Y;
            var distance = (float) Math.Sqrt(distX * distX + distY * distY);

            return distance <= circle.Radius;
        }

        public bool PolygonAndPointContact(PolygonShape polygon, Vector2 point)
        {
            var collision = false;
            var next = 0;

            for (var current = 0; current < polygon.Points.Length; current++)
            {
                next = current + 1;

                if (next == polygon.Points.Length) next = 0;

                var vc = polygon.Points[current];
                var vn = polygon.Points[next];

                if ((vc.Y >= point.Y && vn.Y < point.Y || vc.Y < point.Y && vn.Y >= point.Y) &&
                    point.X < (vn.X - vc.X) * (point.Y - vc.Y) / (vn.Y - vc.Y) + vc.X)
                    collision = !collision;
            }

            return collision;
        }

        public bool PolygonAndLineContact(PolygonShape polygon, LineShape line)
        {
            var next = 0;

            for (var current = 0; current < polygon.Points.Length; current++)
            {
                next = current + 1;

                if (next == polygon.Points.Length) next = 0;

                var hit = LineAndLineContact(line,
                    new LineShape {PointA = polygon.Points[current], PointB = polygon.Points[next]});

                if (hit) return true;
            }

            return false;
        }

        public bool PolygonAndPolygonContact(PolygonShape polygonA, PolygonShape polygonB)
        {
            var next = 0;

            for (var current = 0; current < polygonA.Points.Length; current++)
            {
                next = current + 1;

                if (next == polygonA.Points.Length) next = 0;

                var collision = PolygonAndLineContact(polygonB, new LineShape
                {
                    PointA = polygonA.Points[current],
                    PointB = polygonA.Points[next]
                });

                if (collision) return true;
            }

            var pointInside = PolygonAndPointContact(polygonA, polygonB.Points[0]);

            return pointInside;
        }

        public bool PolygonAndCircleContact(PolygonShape polygon, CircleShape circle)
        {
            var next = 0;

            for (var current = 0; current < polygon.Points.Length; current++)
            {
                next = current + 1;

                if (next == polygon.Points.Length) next = 0;

                var collision =
                    LineAndCircleContact(
                        new LineShape {PointA = polygon.Points[current], PointB = polygon.Points[next]}, circle);

                if (collision) return true;
            }

            var centerInside = PolygonAndPointContact(polygon, circle.Center);

            return centerInside;
        }

        public void TranslateCircle(ref CircleShape circle, Vector2 deltaPosition)
        {
            circle.Center += deltaPosition;
        }

        public void RotateLine(ref LineShape line, float deltaAngle)
        {
            deltaAngle = (float) Math.PI * deltaAngle / 180f;

            var cos = (float) Math.Cos(deltaAngle);
            var sin = (float) Math.Sin(deltaAngle);

            var xa = line.PointA.X - line.Center.X;
            var ya = line.PointA.Y - line.Center.Y;
            var xb = line.PointB.X - line.Center.X;
            var yb = line.PointB.Y - line.Center.Y;

            line.PointA.X = line.Center.X + xa * cos - ya * sin;
            line.PointA.Y = line.Center.Y + xa * sin + ya * cos;
            line.PointB.X = line.Center.X + xb * cos - yb * sin;
            line.PointB.Y = line.Center.Y + xb * sin + yb * cos;
        }

        public void TranslateLine(ref LineShape line, Vector2 deltaPosition)
        {
            line.Center += deltaPosition;
            line.PointA += deltaPosition;
            line.PointB += deltaPosition;
        }

        public void RotatePolygon(ref PolygonShape polygon, float deltaAngle)
        {
            deltaAngle = (float) Math.PI * deltaAngle / 180f;

            var cos = (float) Math.Cos(deltaAngle);
            var sin = (float) Math.Sin(deltaAngle);

            for (var i = 0; i < polygon.Points.Length; i++)
            {
                var x = polygon.Points[i].X - polygon.Center.X;
                var y = polygon.Points[i].Y - polygon.Center.Y;

                polygon.Points[i].X = polygon.Center.X + x * cos - y * sin;
                polygon.Points[i].Y = polygon.Center.Y + x * sin + y * cos;
            }
        }

        public void TranslatePolygon(ref PolygonShape polygon, Vector2 deltaPosition)
        {
            polygon.Center += deltaPosition;

            for (var i = 0; i < polygon.Points.Length; i++) polygon.Points[i] += deltaPosition;
        }

        public CircleShape CloneCircle(CircleShape original)
        {
            return new CircleShape
            {
                Radius = original.Radius,
                Center = new Vector2(original.Center.X, original.Center.Y)
            };
        }

        public LineShape CloneLine(LineShape original)
        {
            return new LineShape
            {
                PointA = new Vector2(original.PointA.X, original.PointA.Y),
                PointB = new Vector2(original.PointB.X, original.PointB.Y),
                Center = new Vector2(original.Center.X, original.Center.Y)
            };
        }

        public PolygonShape ClonePolygon(PolygonShape original)
        {
            var clone = new PolygonShape
            {
                Points = new Vector2[original.Points.Length],
                Center = new Vector2(original.Center.X, original.Center.Y)
            };

            for (var i = 0; i < original.Points.Length; i++)
                clone.Points[i] = new Vector2(original.Points[i].X, original.Points[i].Y);

            return clone;
        }

        #endregion
    }
}