# Decision Tree Implementation - Bug Classification System

## Project Overview
A from-scratch novice implementation of a decision tree classifier in C#, focusing on real-world software engineering applications. This project demonstrates practical machine learning concepts through the lens of bug classification and software quality assurance.

## Installation and Setup

### Prerequisites
- .NET 8.0 SDK
- Git
- VS Code or preferred IDE

### Getting Started
#### Clone the repository and run the project:

##### Clone the repository
```
git clone https://github.com/jeff-ewers/decision-tree.git
cd decision-tree
```

##### Build the project
```dotnet build```

##### Run the application
```dotnet run```

### Project structure
```
decision-tree/
  ├── Program.cs           # Main program and example usage
  ├── DataPoint.cs         # Data representation
  ├── DecisionTree.cs      # Core algorithm implementation
  └── decision-tree.csproj # Project configuration
```
## Technical Highlights

- Custom decision tree algorithm implementation without ML libraries
- Recursive tree building with entropy-based optimization
- Null-safety handling and robust error management
- Generic feature handling supporting both categorical and continuous data
- Modern C# features including nullable reference types, pattern matching, and LINQ

## Core Components
### DecisionTree Class

- Implements recursive binary tree construction
- Uses information gain and entropy calculations for optimal splitting
- Handles tree traversal and prediction logic

### DataPoint Class

- Generic feature vector representation
- Support for both binary and continuous features
- Type-safe data handling

## Bug Classification Implementation
Demonstrates practical software engineering metrics:

- User impact assessment
- System criticality evaluation
- Error frequency analysis
- Historical pattern recognition

## Technical Concepts Demonstrated
### Machine Learning

- Information Theory (Entropy calculations)
- Decision Tree algorithms
- Feature selection and evaluation
- Overfitting prevention

### Software Engineering

- Object-Oriented Design
- Data Structure Implementation
- Recursive Algorithms
- SOLID Principles

### C# Best Practices

- Null Safety
- Exception Handling
- Generic Type Implementation
- Modern C# Features

## Development Process
This project emphasizes:

- Clean Code principles
- Proper version control with meaningful commits
- Type safety and null handling
- Comprehensive documentation

### Potential Enhancements

- Unit test implementation
- Cross-validation support
- Feature importance analysis
- Model persistence
- Performance metrics
- Integration with bug tracking systems

## Example Usage
```
// create and train the decision tree
var tree = new DecisionTree(maxDepth: 4);
tree.Train(trainingData);

// make a prediction
var features = new double[] { 800, 1, 1, 40, 0 };
string prediction = tree.Predict(features);
```
## Skills Demonstrated

- Algorithm Implementation
- Data Structure Design
- Machine Learning Fundamentals
- Developing C# Language Proficiency
- Problem-Solving
- Code Organization


This project was developed to demonstrate proficiency in both theoretical computer science concepts and practical software engineering skills, with a focus on maintainable, production-ready code. It is a student project, and, at present, should not be deployed for any other use-case.
