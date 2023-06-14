using System;

namespace CalculateDistanceNamespace
{
    public class LatLng
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class CalculateDistanceClass
    {
        private double DegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180;
        }

        public double CalculateDistance(LatLng point1, LatLng point2)
        {
            const double earthRadius = 6371; // Radius of the Earth in kilometers

            double lat1 = DegreesToRadians(point1.Latitude);
            double lon1 = DegreesToRadians(point1.Longitude);
            double lat2 = DegreesToRadians(point2.Latitude);
            double lon2 = DegreesToRadians(point2.Longitude);

            double dlon = lon2 - lon1;
            double dlat = lat2 - lat1;

            double a = Math.Pow(Math.Sin(dlat / 2), 2) +
                Math.Cos(lat1) * Math.Cos(lat2) * Math.Pow(Math.Sin(dlon / 2), 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            double distance = earthRadius * c; // Distance in kilometers
            return distance;
        }

        public double CalculateDistanceToPolygon(LatLng point, LatLng[] polygonPoints)
        {
            double minDistance = double.PositiveInfinity;

            for (int i = 0; i < polygonPoints.Length; i++)
            {
                LatLng p1 = polygonPoints[i];
                LatLng p2 = polygonPoints[(i + 1) % polygonPoints.Length];

                double distance = CalculateDistanceToSegment(point, p1, p2);
                minDistance = Math.Min(distance, minDistance);
            }

            return minDistance;
        }

        public double CalculateDistanceToSegment(LatLng point, LatLng segmentStart, LatLng segmentEnd)
        {
            double segmentLength = CalculateDistance(segmentStart, segmentEnd);

            if (segmentLength == 0)
            {
                return CalculateDistance(point, segmentStart);
            }

            double t = Math.Max(0, Math.Min(1, DotProduct(point, segmentStart, segmentEnd)));
            LatLng projectedPoint = new LatLng
            {
                Latitude = segmentStart.Latitude + t * (segmentEnd.Latitude - segmentStart.Latitude),
                Longitude = segmentStart.Longitude + t * (segmentEnd.Longitude - segmentStart.Longitude)
            };

            return CalculateDistance(point, projectedPoint);
        }

        public double DotProduct(LatLng point, LatLng segmentStart, LatLng segmentEnd)
        {
            double x1 = segmentStart.Latitude - point.Latitude;
            double y1 = segmentStart.Longitude - point.Longitude;
            double x2 = segmentEnd.Latitude - point.Latitude;
            double y2 = segmentEnd.Longitude - point.Longitude;

            return (x1 * x2 + y1 * y2) / CalculateDistance(segmentStart, segmentEnd);
        }
    }

 /*   public class Program
    {
        public static void Main(string[] args)
        {
            // Usage example:
            CalculateDistanceClass calculator = new CalculateDistance();
            LatLng point1 = new LatLng { Latitude = 40.7128, Longitude = -74.0060 };
            LatLng point2 = new LatLng { Latitude = 34.0522, Longitude = -118.2437 };
            double distance = calculator.CalculateDistance(point1, point2);
            Console.WriteLine($"Distance: {distance} km");
        }
    }*/
}
