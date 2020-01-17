using System;

namespace Lab2
{
    class EuclideanDistance
    {
        public static double Calculate(Point point1, Point point2)
        {
            double result = Math.Pow(point1.X - point2.X, 2);
            result += Math.Pow(point1.Y - point2.Y, 2);

            if(point1.GetType().Equals(typeof(Point3D)) && point2.GetType().Equals(typeof(Point3D)))
            {
                result += Math.Pow((point1 as Point3D).Z - (point2 as Point3D).Z, 2);
            }
            
            return Math.Sqrt(result);
        }
    }
}
