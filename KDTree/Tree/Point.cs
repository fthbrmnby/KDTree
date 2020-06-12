using System;
using System.Collections.Generic;
using System.Linq;

namespace KDTree.Tree
{
  public class Point
  {
    public double[] AxisValues { get; private set; }
    public int NumDimensions => AxisValues.Count();
    public double this[int index]
    {
      get
      {
        if (index > NumDimensions || index < 0)
          throw new IndexOutOfRangeException();

        return AxisValues[index];
      }
    }

    public Point(int k)
    {
      if (k <= 0)
        throw new ArgumentException("Number of dimensions must be greater than zero.", nameof(k));
      AxisValues = new double[k];
    }

    public Point(IEnumerable<double> values)
    {
      AxisValues = values.ToArray();
    }

    public static double Distance(Point p1, Point p2)
    {
      if (p1.NumDimensions != p2.NumDimensions)
        throw new ArgumentException("Points must have the same number of dimensions");

      var nDims = p1.NumDimensions;
      var sum = 0d;

      for (int i = 0; i < nDims; i++)
      {
        sum += (p1[i] - p2[i]) * (p1[i] - p2[i]);
      }

      return Math.Sqrt(sum);
    }

    public static bool operator ==(Point p1, Point p2) => Equals(p1, p2);
    public static bool operator !=(Point p1, Point p2) => !Equals(p1, p2);

    public override bool Equals(object obj)
    {
      var other = (Point)obj;
      if (other == null)
        return false;

      if (NumDimensions != other.NumDimensions)
        return false;

      for (int i = 0; i < NumDimensions; i++)
      {
        if (AxisValues[i] != other[i])
          return false;
      }

      return true;
    }

    public override string ToString()
    {
      return String.Format("Point({0})", String.Join(',', AxisValues));
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(AxisValues);
    }
  }
}
