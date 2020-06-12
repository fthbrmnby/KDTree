using System;
namespace KDTree.Tree
{
  public class Node
  {
    public int K { get; private set; }
    public Point Data { get; private set; }
    private Node Left;
    public  Node Right;

    public Node(int k) => K = k;

    public Node(Point data)
    {
      if (data.NumDimensions == 0)
        throw new ArgumentException("Number of dimensions cannot be zero", nameof(data));

      K = data.NumDimensions;
      Data = data;
    }

    public void Insert(Point data)
    {
      var newNode = new Node(data);
      Insert(newNode);
    }

    public void Insert(Node newNode, int depth=0)
    {
      if (newNode == null)
        throw new ArgumentNullException(nameof(newNode));

      if (newNode.K != K)
        throw new ArgumentException("Number of dimensions does not match", nameof(newNode.K));

      if (depth < 0)
        throw new ArgumentException("Depth cannot be null", nameof(depth));

      if (Data == null)
      {
        Data = newNode.Data;
        return;
      }

      var axis = depth % K;

      if (newNode.Data[axis] < Data[axis])
      {
        if (Left == null)
          Left = newNode;
        else
          Left.Insert(newNode, depth+1);
      }
      else
      {
        if (Right == null)
          Right = newNode;
        else
          Right.Insert(newNode, depth + 1);
      }
    }

    public Point GetNearestPoint(Point query, int depth=0)
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
