using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Lab2
{
    public class Centroid
    {
        private Point oldPosition;
        private Point position;
        public Point Position
        {
            get => position;
        }

        public Brush Brush { get; }

        /// <summary>
        /// Точки у данного центроида
        /// </summary>
        public List<Point> PointsList { get; set; }

        private static readonly Random random = new Random();

        public Centroid(Brush brush, double widthOfCanvas, double heightOfCanvas)
        {
            Brush = brush;
            position = new Point(random.Next(0, (int)widthOfCanvas), random.Next(0, (int)heightOfCanvas));
            PointsList = new List<Point>();
        }

        public void MoveCentroid()
        {
            double centroidX = 0;
            double centroidY = 0;

            foreach (var point in PointsList)
            {
                centroidX += point.X;
                centroidY += point.Y;
            }

            centroidX /= PointsList.Count;
            centroidY /= PointsList.Count;

            oldPosition = position;
            position = new Point(centroidX, centroidY);
        }

        public bool HasChanged() => oldPosition != position;

        public void AddPoint(Point point) => PointsList.Add(point);

        public void Reset() => PointsList.Clear();
    }
}
