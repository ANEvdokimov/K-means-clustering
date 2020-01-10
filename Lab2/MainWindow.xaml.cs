using System;
using System.Collections.Generic;
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
        public MainWindow()
        {
            InitializeComponent();

            points = new List<Point>();
        }

        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DrawPoint(e.GetPosition(Canvas).X, e.GetPosition(Canvas).Y, Brushes.Black);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            ClearCanvas();
        }

        private void DrawPoint(Point point, Brush brush)
        {
            Canvas.Children.Add(new Line
            {
                X1 = point.X,
                Y1 = point.Y,
                X2 = point.X,
                Y2 = point.Y,

                StrokeStartLineCap = PenLineCap.Round,
                StrokeEndLineCap = PenLineCap.Round,
                StrokeThickness = 5,
                Stroke = brush
            });

            points.Add(point);
        }

        private void DrawPoint(double x, double y, Brush brush)
        {
            DrawPoint(new Point(x, y), brush);   
        }

        private void GenerateFivePoints(object sender, RoutedEventArgs e)
        {
            Random random = new Random();

            for (int i = 0; i < 5; i++)
            {
                DrawPoint(random.Next(0, (int)Canvas.ActualWidth), random.Next(0, (int)Canvas.ActualHeight), Brushes.Black);
            }
        }

        private void GenerateTenPoints(object sender, RoutedEventArgs e)
        {
            Random random = new Random();

            for (int i = 0; i < 10; i++)
            {
                DrawPoint(random.Next(0, (int)Canvas.ActualWidth), random.Next(0, (int)Canvas.ActualHeight), Brushes.Black);
            }
        }

        private void GenerateFifteenPoints(object sender, RoutedEventArgs e)
        {
            Random random = new Random();

            for (int i = 0; i < 15; i++)
            {
                DrawPoint(random.Next(0, (int)Canvas.ActualWidth), random.Next(0, (int)Canvas.ActualHeight), Brushes.Black);
            }
        }

        private void GenerateThirtyPoints(object sender, RoutedEventArgs e)
        {
            Random random = new Random();

            for (int i = 0; i < 30; i++)
            {
                DrawPoint(random.Next(0, (int)Canvas.ActualWidth), random.Next(0, (int)Canvas.ActualHeight), Brushes.Black);
            }
        }

        private void ClearCanvas()
        {
            Canvas.Children.Clear();
            points.Clear();
        }

        private void Repaint(List<Centroid> centroids)
        {
            ClearCanvas();

            foreach (var centroid in centroids)
            {
                foreach(var point in centroid.PointsList)
                {
                    DrawPoint(point, centroid.Brush);
                }
            }
        }

        private void Start(object sender, RoutedEventArgs e)
        {
            var copySender = sender as MenuItem;
            int numberOfClusters = int.Parse(copySender.CommandParameter.ToString());
            KMeans kMeans = new KMeans(numberOfClusters, 10);
            Repaint(kMeans.Run(points, Canvas.ActualWidth, Canvas.ActualHeight));
        }
    }
}
