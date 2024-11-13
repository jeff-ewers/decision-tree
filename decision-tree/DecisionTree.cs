using System;
using System.Linq;

namespace DecisionTree 
{
    internal class DecisionTree
    {
        public class Node
        {
            public bool IsLeaf { get; set; } // whether this is final decision point
            public string? Label { get; set; }   // for leaf nodes (final decision)
            public int FeatureIndex { get; set; } // feature we're splitting on
            public double SplitValue { get; set; } // value to split at
            public Node? Left { get; set; } // for values <= split val
            public Node? Right { get; set; } // for values > split val
        }
    // main tree properties
    private Node? Root;
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
        while (!current.IsLeaf)
        {
            
            if (features[current.FeatureIndex] <= current.SplitValue)
                current = current.Left;
            else
                current = current.Right;

            current = features[current.FeatureIndex] <= current.SplitValue 
                ? current = current.Left ?? throw new InvalidOperationException("Left node is null")
                : current = current.Right ?? throw new InvalidOperationException("Right node is null");
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
            return new Node {IsLeaf = true, Label = "Unknown"};
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

    private (int bestFeature, double bestValue) FindBestSplit(List<DataPoint> data)
    {
        
        if (data.Count == 0 || data[0].Features.Length == 0)
            return (-1,0);
        
        double bestGain = 0;
        int bestFeature = -1;
        double bestValue = 0;

        // for each feature
        for (int feature = 0; feature < data[0].Features.Length; feature++)
        {
            // get unique values for the feature
            var values = data
                .Select(d => d.Features[feature])
                .Distinct()
                .OrderBy(v => v)
                .ToList();

            // try splitting on each value

            for (int i = 0; i < values.Count - 1; i++)
            {
                double splitValue = (values[i] + values[i+1]) / 2;
                double gain = CalculateInformationGain(data, feature, splitValue);

                if (bestGain > bestGain)
                {
                    bestGain = gain;
                    bestFeature = feature;
                    bestValue = splitValue;
                }
            }
        }

        return (bestFeature, bestValue);


    }

    private double CalculateInformationGain(List<DataPoint> data, int feature, double splitValue)
    {
        var leftData = data.Where(d => d.Features[feature] <= splitValue).ToList();
        var rightData = data.Where(d => d.Features[feature] > splitValue).ToList();

        // zero gain if split results in empty set
        if (rightData.Count == 0 || leftData.Count == 0)
        {
            return 0;
        }

        double entropyBefore = CalculateEntropy(data);
        double entropyAfter =
            (leftData.Count * CalculateEntropy(leftData) +
            rightData.Count * CalculateEntropy(rightData)) / data.Count;         
        return entropyBefore - entropyAfter;

    }

    private double CalculateEntropy(List<DataPoint> data)
    {
        var labelCounts = data
            .GroupBy(d => d.Label)
            .Select(g => (double)g.Count() / data.Count);

        return labelCounts.Sum(p => p * Math.Log2(p));
    }
    }
}