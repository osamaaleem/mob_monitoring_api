using mob_monitoring_api.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace mob_monitoring_api.Utilities
{
    public class Utility
    {
        public bool CheckPointInPolygon(RedZoneCoordinates[] polygon, LatLng point)
        {
            PointF[] lpf = new PointF[100];

            for (int i = 0; i < polygon.Count(); i++)
            {

                lpf[i] = ConvertToPoint((double)polygon[i].RedZoneLat, (double)polygon[i].RedZoneLon);
            }

            PointF pf = ConvertToPoint(point.lat, point.lng);


            bool r = IsPointInPolygon(lpf, pf);
            return r;
        }

        public PointF ConvertToPoint(double lat, double lon)
        {
            double R = 6371;

            double x = R * Math.Cos(lat) * Math.Cos(lon);
            double y = R * Math.Cos(lat) * Math.Sin(lon);

            return new PointF(Convert.ToSingle(x), Convert.ToSingle(y));
        }

        public bool IsPointInPolygon(PointF[] polygon, PointF point)
        {



            bool isInside = false;
            for (int i = 0, j = polygon.Length - 1; i < polygon.Length; j = i++)
            {
                if (((polygon[i].Y > point.Y) != (polygon[j].Y > point.Y)) &&
                (point.X < (polygon[j].X - polygon[i].X) * (point.Y - polygon[i].Y) / (polygon[j].Y - polygon[i].Y) + polygon[i].X))
                {
                    isInside = !isInside;
                }
            }
            return isInside;
        }
    }
}