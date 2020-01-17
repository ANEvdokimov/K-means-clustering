using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace Lab2
{
    public class Centroid3D : Centroid
    {
        private Point3D oldPosition;
        public new Point3D Position { get; private set; }

        public Centroid3D(Point3D position, Brush brush) : base(brush)
        {
            Position = position;
        }

        new public void MoveCentroid()
        {
            double centroidX = 0;
            double centroidY = 0;
            double centroidZ = 0;

            foreach (var point in PointsList)
            {
                centroidX += point.X;
                centroidY += point.Y;
                centroidZ += (point as Point3D).Z;
            }

            centroidX /= PointsList.Count;
            centroidY /= PointsList.Count;
            centroidZ /= PointsList.Count;

            oldPosition = Position;
            if (!double.IsNaN(centroidX) && !double.IsNaN(centroidY) && !double.IsNaN(centroidZ))
            {
                Position = new Point3D(centroidX, centroidY, centroidZ);
            }
            else
            {
                Position = null;
            }
        }

        public new bool HasChanged() => !oldPosition.Equals(Position);

        public new void AddPoint(Point point) => PointsList.Add(point as Point3D);
    }
}
