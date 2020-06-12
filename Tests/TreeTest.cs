using System;
using System.Collections.Generic;
using KDTree.Tree;
using NUnit.Framework;

namespace Tests
{
  [TestFixture]
  public class TreeTest
  {
    [Test]
    public void TestInsertDimensionsNotMatch([Values(
      new[] { 1, 2.0 },
      new[] { 2.0, 10, 9, -2 })] double[] p)
    {
      var root = new Node(1);
      var point = new Point(p);
      Assert.Throws<ArgumentException>(() => root.Insert(point));
    }

    [Test]
    public void TestGetNearestPoint()
    {
      var root = new Node(2);

      var points = new List<Point> {
        new Point(new double[] { 1, 2 }),
        new Point(new double[] { 2, 3 }),
        new Point(new double[] { 12, 6 }),
        new Point(new double[] { 60, 27 }),
        new Point(new double[] { 211, 90 })
      };

      for (int i = 1; i < points.Count; i++)
      {
        root.Insert(points[i]);
      }

      var nearest = root.GetNearestPoint(points[0]);

      Assert.AreEqual(points[1], nearest);
    }
  }
}
