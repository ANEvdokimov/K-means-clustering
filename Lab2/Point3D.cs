namespace Lab2
{
    public class Point3D : Point
    {
        public double Z { get; set; }

        public Point3D(double x, double y, double z) : base(x, y)
        {
            Z = z;
        }

        public new Point3D Clone()
        {
            return new Point3D(X, Y, Z);
        }

        public override bool Equals(object obj)
        {
            return obj is Point3D d &&
                   base.Equals(obj) &&
                   Z == d.Z;
        }

        public override int GetHashCode()
        {
            var hashCode = 71168995;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + Z.GetHashCode();
            return hashCode;
        }
    }
}
