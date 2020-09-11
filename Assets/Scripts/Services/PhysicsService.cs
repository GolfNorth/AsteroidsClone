using UnityEngine;

namespace AsteroidsClone
{
    public sealed class PhysicsService
    {
        public bool PointAndCircleContact(Vector2 point, CircleShape circle)
        {
            var distX = point.x - circle.Center.x;
            var distY = point.y - circle.Center.y;
            var distance = Mathf.Sqrt((distX * distX) + (distY * distY));

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
            float d1 = Vector2.Distance(point, line.PointA);
            float d2 = Vector2.Distance(point, line.PointB);

            float lineLen = Vector2.Distance(line.PointA, line.PointB);

            var buffer = 0.1f;

            return d1 + d2 >= lineLen - buffer && d1 + d2 <= lineLen + buffer;
        }

        public bool LineAndCircleContact(LineShape line, CircleShape circle)
        {
            var inside1 = PointAndCircleContact(line.PointA, circle);
            var inside2 = PointAndCircleContact(line.PointB, circle);

            if (inside1 || inside2) return true;

            var distX = line.PointA.x - line.PointB.x;
            var distY = line.PointA.y - line.PointB.y;
            var len = Mathf.Sqrt((distX * distX) + (distY * distY));

            var dot = (((circle.Center.x - line.PointA.x) * (line.PointB.x - line.PointA.x)) 
                + ((circle.Center.y - line.PointA.y) * (line.PointB.y - line.PointA.y))) 
                / Mathf.Pow(len, 2);

            var closestX = line.PointA.x + (dot * (line.PointB.x - line.PointA.x));
            var closestY = line.PointA.y + (dot * (line.PointB.y - line.PointA.y));

            var onSegment = LineAndPointContact(line, new Vector2(closestX, closestY));

            if (!onSegment) return false;

            distX = closestX - circle.Center.x;
            distY = closestY - circle.Center.y;
            var distance = Mathf.Sqrt((distX * distX) + (distY * distY));

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

                if (((vc.y >= point.y && vn.y < point.y) || (vc.y < point.y && vn.y >= point.y)) &&
                     (point.x < (vn.x - vc.x) * (point.y - vc.y) / (vn.y - vc.y) + vc.x))
                {
                    collision = !collision;
                }
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

                var hit = LineAndLineContact(line, new LineShape { PointA = polygon.Points[current], PointB = polygon.Points[next] });

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

                var collision = PolygonAndLineContact(polygonB, new LineShape { 
                    PointA = polygonA.Points[current], 
                    PointB = polygonA.Points[next] 
                });

                if (collision) return true;
            }

            var pointInside = PolygonAndPointContact(polygonA, polygonB.Points[0]);

            if (pointInside) return true;

            return false;
        }

        public bool PolygonAndCircleContact(PolygonShape polygon, CircleShape circle)
        {
            var next = 0;

            for (var current = 0; current < polygon.Points.Length; current++)
            {
                next = current + 1;

                if (next == polygon.Points.Length) next = 0;

                var collision = LineAndCircleContact(new LineShape { PointA = polygon.Points[current], PointB = polygon.Points[next] }, circle);
                
                if (collision) return true;
            }

            var centerInside = PolygonAndPointContact(polygon, circle.Center);

            if (centerInside) return true;

            return false;
        }
    }
}