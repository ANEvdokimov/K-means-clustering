using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Lab2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Point> points;
        private List<Point> points3D;
        private List<Centroid> centroids3D;
        private Random random;
        public MainWindow()
        {
            points = new List<Point>();
            points3D = new List<Point>();
            centroids3D = new List<Centroid>();
            random = new Random();

            InitializeComponent();
        }

        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DrawPoint(e.GetPosition(Canvas).X, e.GetPosition(Canvas).Y, Brushes.Black);
            if (CB_3DMode.IsChecked && Iud_Level.Value != null)
            {
                points3D.Add(new Point3D(e.GetPosition(Canvas).X, e.GetPosition(Canvas).Y, (double)Iud_Level.Value));
            }
        }

        private void MI_ClearClick(object sender, RoutedEventArgs e)
        {
            ClearAll();
        }

        private void DrawPoint(Point point, Brush brush, int strokeThickness = 5, bool saveInListPoints = true)
        {
            Canvas.Children.Add(new Line
            {
                X1 = point.X,
                Y1 = point.Y,
                X2 = point.X,
                Y2 = point.Y,

                StrokeStartLineCap = PenLineCap.Round,
                StrokeEndLineCap = PenLineCap.Round,
                StrokeThickness = strokeThickness,
                Stroke = brush
            });

            if (saveInListPoints)
            {
                points.Add(point);
            }
        }

        private void DrawPoint(double x, double y, Brush brush, int strokeThickness = 5)
        {
            DrawPoint(new Point(x, y), brush, strokeThickness);
        }

        private void GenerateFivePoints(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 5; i++)
            {
                if (CB_3DMode.IsChecked)
                {
                    Point point = new Point3D(random.Next(0, (int)Canvas.ActualWidth), random.Next(
                        0, (int)Canvas.ActualHeight), random.Next((int)Iud_Level.Minimum, (int)Iud_Level.Maximum + 1));
                    points3D.Add(point);
                    if ((point as Point3D).Z == (double)Iud_Level.Value)
                    {
                        DrawPoint(point, Brushes.Black);
                    }
                }
                else
                {
                    DrawPoint(random.Next(0, (int)Canvas.ActualWidth), random.Next(0, (int)Canvas.ActualHeight), Brushes.Black);
                }
            }
        }

        private void GenerateTenPoints(object sender, RoutedEventArgs e)
        {
            GenerateFivePoints(sender, e);
            GenerateFivePoints(sender, e);
        }

        private void GenerateFifteenPoints(object sender, RoutedEventArgs e)
        {
            GenerateFivePoints(sender, e);
            GenerateFivePoints(sender, e);
            GenerateFivePoints(sender, e);
        }

        private void GenerateThirtyPoints(object sender, RoutedEventArgs e)
        {
            GenerateFifteenPoints(sender, e);
            GenerateFifteenPoints(sender, e);
        }

        private void ClearCanvas()
        {
            Canvas.Children.Clear();
            points.Clear();
        }

        private void ClearAll()
        {
            ClearCanvas();
            points3D.Clear();
        }

        private void Repaint3D(List<Centroid> centroids, int z)
        {
            ClearCanvas();

            foreach (var centroid in centroids)
            {
                DrawPoints(centroid.PointsList.Where(item => (item as Point3D).Z == z), centroid.Brush);
            }
        }

        private void Repaint3D(int z)
        {
            ClearCanvas();

            if (centroids3D.Count != 0)
            {
                foreach (var centroid in centroids3D)
                {
                    DrawPoints(centroid.PointsList.Where(item => (item as Point3D).Z == z), centroid.Brush);
                }
            }
            else
            {
                DrawPoints(points3D.Where(item => (item as Point3D).Z == z), Brushes.Black);
            }
        }

        private void Repaint(List<Centroid> centroids)
        {
            ClearCanvas();

            foreach (var centroid in centroids)
            {
                DrawPoint(centroid.Position, centroid.Brush, 10, false);
                DrawPoints(centroid.PointsList, centroid.Brush);
            }
        }

        private void DrawPoints(IEnumerable<Point> points, Brush brush)
        {
            if (points != null)
            {
                foreach (var point in points)
                {
                    DrawPoint(point, brush);
                }
            }
        }

        private void PrintPoints3D(List<Centroid> centroids)
        {
            foreach(var centroid in centroids)
            {
                Console.WriteLine("Cluster :" + centroid.Brush.ToString());
                foreach(var point in centroid.PointsList)
                {
                    Console.WriteLine($"Point : {point.X} {point.Y} {(point as Point3D).Z}");
                }
            }
        }

        private void Start(object sender, RoutedEventArgs e)
        {
            var copySender = sender as MenuItem;
            int numberOfClusters = int.Parse(copySender.CommandParameter.ToString());
            KMeans kMeans = new KMeans(numberOfClusters, 100); ;

            if (CB_3DMode.IsChecked == true)
            {
                centroids3D = kMeans.Run(points3D);
                Repaint3D(centroids3D, (int)Iud_Level.Value);
                PrintPoints3D(centroids3D);
            }
            else
            {
                Repaint(kMeans.Run(points));
            }
        }

        private void TurnOn3DMode(object sender, RoutedEventArgs e)
        {
            Iud_Level.IsEnabled = true;
            points3D.AddRange(points.Select(item => new Point3D(item.X, item.Y, (double)Iud_Level.Value) as Point));
        }

        private void TurnOff3DMode(object sender, RoutedEventArgs e)
        {
            Iud_Level.IsEnabled = false;
            Iud_Level.Value = 0;
        }

        private void Level_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            Repaint3D((int)Iud_Level.Value);
        }
    }
}
