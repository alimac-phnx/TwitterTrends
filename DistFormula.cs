using System;

namespace TwitterTrends
{
    public class DistFormula
    {
        private double Lat1 { get; set; }
        private double Lng1 { get; set; }

        private double Cl1 { get; set; }
        private double Sl1 { get; set; }

        private double Lat2 { get; set; }
        private double Lng2 { get; set; }

        private double Cl2 { get; set; }
        private double Sl2 { get; set; }

        private double Delta { get; set; }
        private double Cdelta { get; set; }
        private double Sdelta { get; set; }

        private double Y { get; set; }
        private double X { get; set; }

        private double Ad { get; set; }

        public DistFormula(double lat1, double lng1, double lat2, double lng2)
        {
            Lat1 = lat1;
            Lng1 = lng1;
            Cl1 = Math.Cos(lat1);
            Sl1 = Math.Sin(lat1);
            Lat2 = lat2;
            Lng2 = lng2;
            Cl2 = Math.Cos(lat2);
            Sl2 = Math.Sin(lat2);
            Delta = lng2 - lng1;
            Cdelta = Math.Cos(Delta);
            Sdelta = Math.Sin(Delta);
            Y = Math.Sqrt(Math.Pow(Cl2 * Sdelta, 2) + Math.Pow(Cl1 * Sl2 - Sl1 * Cl2 * Cdelta, 2));
            X = Sl1 * Sl2 + Cl1 * Cl2 * Cdelta;
            Ad = Math.Atan2(Y, X);
        }

        public double Calculate()
        {
            return Ad * 6372795;
        }
    }
}