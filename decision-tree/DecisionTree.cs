using System;
using System.Linq;

namespace DecisionTree 
{
    internal class DecisionTree
    {
        public class Node
        {
            public bool IsLeaf { get; set; } // whether this is final decision point
            public string Label { get; set; }   // for leaf nodes (final decision)
            public int FeatureIndex { get; set; } // feature we're splitting on
            public double SplitValue { get; set; } // value to split at
            public Node Left { get; set; } // for values <= split val
            public Node Right { get; set; } // for values > split val
        }
    // main tree properties
    private Node Root;
    private int maxDepth;
    // constructor
    public DecisionTree(int maxDepth = 5) 
    {
        this.maxDepth = maxDepth;
        this.Root = null;
    }

    public void Train(List<DataPoint> data)
    {
        // start recursive tree build
        Root = BuildTree(data, depth: 0);
    }

    public string Predict(double[] features)
    {
        if (Root == null)
            throw new InvalidOperationException("Tree must be trained before prediction.");

        Node current = Root;
        while (!current.isLeaf)
        {
            if (features[current.FeatureIndex] <= current.SplitValue)
                current = current.Left;
            else
                current = current.Right;
        }
        return current.Label;

    }

    private Node BuildTree (List<DataPoint> data, int depth)
    {
        // stop if we run out of either tree or data (returns null-labeled node)
        // TODO: add helper methods
        if (depth >= maxDepth || data.Count == 0)
            return CreateLeafNode(data); 
        
        if (data.All(d => d.Label == data[0].Label))
            return CreateLeafNode(data);
        
        // TODO: add helper methods
        var (bestFeature, bestValue) = FindBestSplit(data);

        // if we can't find a good split, return a leaf node
        if (bestFeature == -1)
            return CreateLeafNode(data);

        // split the data
        var leftData = data.Where(d => d.Features[bestFeature] <= bestValue).ToList();
        var rightData = data.Where(d => d.Features[bestFeature] > bestValue).ToList();
        return new Node
        {
            IsLeaf = false,
            FeatureIndex = bestFeature,
            SplitValue = bestValue,
            Left = BuildTree(leftData, depth+1),
            Right = BuildTree(rightData, depth+1)
        };
    }

    private Node CreateLeafNode(List<DataPoint> data)
    {
        // return null to stop recursion if we have no subset data 
        if (data.Count == 0)
        {
            return new Node {IsLeaf = true, Label = null};
        }
        // find the most common label
        var mostCommonLabel = data 
            .GroupBy(d => d.Label)
            .OrderByDescending(g => g.Count())
            .First()
            .Key;

        return new Node
        {
            IsLeaf = true,
            Label = mostCommonLabel
        };
    }
    }
}