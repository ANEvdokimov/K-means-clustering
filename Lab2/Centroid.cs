using System.Collections.Generic;
using System.Windows.Media;

namespace Lab2
{
    public class Centroid
    {
        private Point oldPosition;
        public Point Position { get; private set; }
        public Brush Brush { get; }
        public List<Point> PointsList { get; private set; }

        public Centroid(Brush brush)
        {
            Brush = brush;
            PointsList = new List<Point>();
        }

        public Centroid(Point position, Brush brush)
        {
            Brush = brush;
            Position = position;
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

            oldPosition = Position;
            if (!double.IsNaN(centroidX) && !double.IsNaN(centroidY))
            {
                Position = new Point(centroidX, centroidY);
            }
            else
            {
                Position = null;
            }
        }

        public bool HasChanged => !oldPosition.Equals(Position);

        public void AddPoint(Point point) => PointsList.Add(point);

        public void Reset() => PointsList.Clear();
    }
}
