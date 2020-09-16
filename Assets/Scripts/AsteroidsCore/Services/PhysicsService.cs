using System;

namespace AsteroidsCore
{
    public sealed class PhysicsService
    {
        #region Methods

        public bool PointAndCircleContact(Vector2 point, CircleShape circle)
        {
            var distX = point.x - circle.Center.x;
            var distY = point.y - circle.Center.y;
            var distance = (float) Math.Sqrt(distX * distX + distY * distY);

            return distance <= circle.Radius;
        }

        public bool LineAndLineContact(LineShape lineA, LineShape lineB)
        {
            var uA = ((lineB.PointB.x - lineB.PointA.x) * (lineA.PointA.y - lineB.PointA.y)
                      - (lineB.PointB.y - lineB.PointA.y) * (lineA.PointA.x - lineB.PointA.x))
                     / ((lineB.PointB.y - lineB.PointA.y) * (lineA.PointB.x - lineA.PointA.x)
                        - (lineB.PointB.x - lineB.PointA.x) * (lineA.PointB.y - lineA.PointA.y));
            var uB = ((lineA.PointB.x - lineA.PointA.x) * (lineA.PointA.y - lineB.PointA.y)
                      - (lineA.PointB.y - lineA.PointA.y) * (lineA.PointA.x - lineB.PointA.x))
                     / ((lineB.PointB.y - lineB.PointA.y) * (lineA.PointB.x - lineA.PointA.x)
                        - (lineB.PointB.x - lineB.PointA.x) * (lineA.PointB.y - lineA.PointA.y));

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

            var distX = line.PointA.x - line.PointB.x;
            var distY = line.PointA.y - line.PointB.y;
            var len = (float) Math.Sqrt(distX * distX + distY * distY);

            var dot = ((circle.Center.x - line.PointA.x) * (line.PointB.x - line.PointA.x)
                       + (circle.Center.y - line.PointA.y) * (line.PointB.y - line.PointA.y))
                      / (float) Math.Pow(len, 2);

            var closestX = line.PointA.x + dot * (line.PointB.x - line.PointA.x);
            var closestY = line.PointA.y + dot * (line.PointB.y - line.PointA.y);

            var onSegment = LineAndPointContact(line, new Vector2(closestX, closestY));

            if (!onSegment) return false;

            distX = closestX - circle.Center.x;
            distY = closestY - circle.Center.y;
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

                if ((vc.y >= point.y && vn.y < point.y || vc.y < point.y && vn.y >= point.y) &&
                    point.x < (vn.x - vc.x) * (point.y - vc.y) / (vn.y - vc.y) + vc.x)
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

            var xa = line.PointA.x - line.Center.x;
            var ya = line.PointA.y - line.Center.y;
            var xb = line.PointB.x - line.Center.x;
            var yb = line.PointB.y - line.Center.y;

            line.PointA.x = line.Center.x + xa * cos - ya * sin;
            line.PointA.y = line.Center.y + xa * sin + ya * cos;
            line.PointB.x = line.Center.x + xb * cos - yb * sin;
            line.PointB.y = line.Center.y + xb * sin + yb * cos;
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
                var x = polygon.Points[i].x - polygon.Center.x;
                var y = polygon.Points[i].y - polygon.Center.y;

                polygon.Points[i].x = polygon.Center.x + x * cos - y * sin;
                polygon.Points[i].y = polygon.Center.y + x * sin + y * cos;
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
                Center = new Vector2(original.Center.x, original.Center.y)
            };
        }

        public LineShape CloneLine(LineShape original)
        {
            return new LineShape
            {
                PointA = new Vector2(original.PointA.x, original.PointA.y),
                PointB = new Vector2(original.PointB.x, original.PointB.y),
                Center = new Vector2(original.Center.x, original.Center.y)
            };
        }

        public PolygonShape ClonePolygon(PolygonShape original)
        {
            var clone = new PolygonShape
            {
                Points = new Vector2[original.Points.Length],
                Center = new Vector2(original.Center.x, original.Center.y)
            };

            for (var i = 0; i < original.Points.Length; i++)
                clone.Points[i] = new Vector2(original.Points[i].x, original.Points[i].y);

            return clone;
        }

        #endregion
    }
}