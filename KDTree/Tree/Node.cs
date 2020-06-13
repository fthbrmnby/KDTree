using System;
using System.Collections.Generic;
using System.Linq;

namespace KDTree.Tree
{
    public class Node
    {
        public int K { get; private set; }
        public Point Data { get; private set; }
        private Node Left;
        public Node Right;

        public Node(int k)
        {
            if (k <= 0)
                throw new ArgumentException("Number of dimensions must be greater than zero.", nameof(k));

            K = k;
        }

        public Node(Point data)
        {
            if (data.NumDimensions <= 0)
                throw new ArgumentException("Number of dimensions must be greater than zero.", nameof(data));

            K = data.NumDimensions;
            Data = data;
        }

        public void Insert(Point data)
        {
            var newNode = new Node(data);
            Insert(new List<Node> { newNode });
        }

        public void Insert(IEnumerable<Point> points)
        {
            Insert(points.Select(p => new Node(p)));
        }

        public void Insert(IEnumerable<Node> newNodes, int depth = 0)
        {
            if (newNodes == null)
                throw new ArgumentNullException(nameof(newNodes));

            if (newNodes.Count() == 0)
                return;

            var dims = newNodes.Select(n => n.K).Distinct();
            if (dims.Count() > 1)
                throw new ArgumentException("All points must have the same number of dimensions", nameof(newNodes));

            if (dims.First() != K)
                throw new ArgumentException("Number of dimensions does not match");

            if (depth < 0)
                throw new ArgumentException("Depth cannot be null", nameof(depth));

            var axis = depth % K;
            var orderedNodes = newNodes.OrderBy(n => n.Data[axis]).ToList();
            var pivot = orderedNodes.Count / 2;
            var pivotPoint = orderedNodes[pivot];

            if (Data == null)
                Data = pivotPoint.Data;

            if (Left == null)
                Left = new Node(K);

            if (Right == null)
                Right = new Node(K);

            Left.Insert(orderedNodes.Take(pivot), depth + 1);
            Right.Insert(orderedNodes.Skip(pivot).Take(orderedNodes.Count), depth + 1);
        }

        public Point GetNearestPoint(Point query, int depth = 0)
        {
            if (query == null)
                throw new ArgumentNullException(nameof(query));

            if (depth < 0)
                throw new ArgumentException("Depth cannot be negative", nameof(depth));

            if (Data == null)
                return null;

            var axis = depth % K;

            if (query[axis] < Data[axis])
                return Left?.GetNearestPoint(query, depth + 1) ?? Data;
            else
                return Right?.GetNearestPoint(query, depth + 1) ?? Data;
        }
    }
}
