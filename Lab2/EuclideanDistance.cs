using System;
using System.Windows;

namespace Lab2
{
    class EuclideanDistance
    {
        public static double Calculate(Point point1, Point point2)
        {
            double res = Math.Pow(point1.X - point2.X, 2);
            res += Math.Pow(point1.Y - point2.Y, 2);
            return Math.Sqrt(res);
        }
    }
}
