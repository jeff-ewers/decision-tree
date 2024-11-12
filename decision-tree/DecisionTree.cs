namespace DecisionTree 
{
    internal class DecisionTree
    {
        public class Node
        {
            public bool isLeaf { get; set; } // whether this is final decision point
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
    }
}