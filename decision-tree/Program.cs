namespace DecisionTree
{
    class Program 
    {
        static void Main(string[] args) 
        {
            var trainingData = new List<DataPoint>
            {
                // features: [
                //   number of users affected (reported),
                //   is customer-facing (1=yes, 0=no),
                //   affects core functionality (1=yes, 0=no),
                //   number of error reports in last hour,
                //   similar bugs in last 30 days
                // ]

                // critical bugs
                new DataPoint
                {
                    Features = new double[] { 1000, 1, 1, 50, 2},
                    Label = "Critical"
                },
                new DataPoint 
                { 
                    Features = new double[] { 500, 1, 1, 30, 0 },
                    Label = "Critical" 
                },
                
                // major bugs
                new DataPoint 
                { 
                    Features = new double[] { 100, 1, 0, 5, 1 },
                    Label = "Major" 
                },
                new DataPoint 
                { 
                    Features = new double[] { 50, 0, 1, 2, 2 },
                    Label = "Major" 
                },
                
                // minor bugs
                new DataPoint 
                { 
                    Features = new double[] { 10, 0, 0, 1, 0 },
                    Label = "Minor" 
                },
                new DataPoint 
                { 
                    Features = new double[] { 5, 0, 0, 1, 1 },
                    Label = "Minor" 
                }
            };

            // create and train tree
            var tree = new DecisionTree(maxDepth: 4);
            tree.Train(trainingData);

            var testData = new List<(double[] Features, string Description)>
            {
                (new double[] { 800, 1, 1, 40, 0 },
                "Login system error affecting multiple users"),

                (new double[] { 20, 0, 0, 1, 1 }, 
                 "UI formatting issue in admin panel"),
                
                (new double[] { 150, 1, 0, 8, 2 }, 
                 "Search results pagination error")
            };

            Console.WriteLine("Bug Severity Predictions: \n");
            foreach (var (features, description) in testData)
            {
                string prediction = tree.Predict(features);
                Console.WriteLine($"Bug: {description}");
                Console.WriteLine($"Predicted severity: {prediction}");
                Console.WriteLine($"Metrics:");
                Console.WriteLine($"- Users affected: {features[0]}");
                Console.WriteLine($"- Customer-facing: {(features[1] == 1 ? "Yes" : "No")}");
                Console.WriteLine($"- Core functionality: {(features[2] == 1 ? "Yes" : "No")}");
                Console.WriteLine($"- Error reports / hour: {features[3]}");
                Console.WriteLine($"- Similar recent bugs: {features[4]}\n");

            }
        }
    }
}

