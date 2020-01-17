using System.Collections.Generic;
using System.Windows.Media;

namespace Lab2
{
    public class KMeans
    {
        private readonly Brush[] brushes = { Brushes.Red, Brushes.Blue, Brushes.Green, Brushes.Purple, Brushes.Orange };

        private readonly int numberOfClusters;
        private readonly int numberOfIterations;

        public KMeans(int numberOfClusters = 2, int maxNumberOfIterations = 20)
        {
            this.numberOfClusters = numberOfClusters;
            this.numberOfIterations = maxNumberOfIterations;
        }

        public List<Centroid> Run(List<Point> dataset)
        {
            List<Centroid> centroidList = new List<Centroid>();

            for (int i = 0; i < numberOfClusters && i < dataset.Count; i++)
            {
                Centroid centroid;
                if (dataset.GetType().Equals(typeof(List<Point3D>)))
                {
                    centroid = new Centroid3D((dataset[i] as Point3D).Clone(), brushes[i]);
                }
                else
                {
                    centroid = new Centroid(dataset[i].Clone(), brushes[i]);
                }
                centroidList.Add(centroid);
            }

            return Clusterisation(dataset, centroidList);
        }

        private List<Centroid> Clusterisation(List<Point> dataset, List<Centroid> centroidList)
        {
            for (int iteration = 0; iteration < numberOfIterations; iteration++)
            {
                foreach (Centroid centroid in centroidList)
                {
                    centroid.Reset();
                }

                foreach (var point in dataset)
                {
                    int closestIndex = -1;
                    double minDistance = double.MaxValue;
                    for (int k = 0; k < centroidList.Count; k++)
                    {
                        double distance = EuclideanDistance.Calculate(centroidList[k].Position, point);
                        if (distance < minDistance)
                        {
                            closestIndex = k;
                            minDistance = distance;
                        }
                    }
                    centroidList[closestIndex].AddPoint(point);
                }

                foreach (Centroid centroid in centroidList)
                {
                    centroid.MoveCentroid();
                }

                bool hasChanged = false;
                foreach (Centroid centroid in centroidList)
                {
                    if (centroid.HasChanged)
                    {
                        hasChanged = true;
                        break;
                    }
                }

                if (!hasChanged)
                {
                    break;
                }
            }

            return centroidList;
        }        
    }
}
