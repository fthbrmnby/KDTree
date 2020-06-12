using System;
using System.Collections.Generic;
using KDTree.Tree;
using NUnit.Framework;

namespace Tests
{
  [TestFixture]
  public class PointTests
  {
    [Test]
    public void TestCalculateDistance()
    {
      var p1 = new Point(new[] { 2.0, 2.0 });
      var p2 = new Point(new[] { 3.0, 3.0 });

      var dist = Math.Round(Point.Distance(p1, p2), 4);
      Assert.AreEqual(1.4142, dist);
    }
  }
}
