namespace Lilypad.Helpers; 

public static class VariableMath {
    static int _tempIndex;
    
    public static T Evaluate<T>(this Function function, T output, string expression, params IVariable[] variables) where T : IVariable { 
        function.SetVariable(output, Evaluate(function, expression, variables));
        return output;
    }
    
    public static IVariable Evaluate(this Function function, string expression, params IVariable[] variables) {
        var datapack = function.Datapack;
        var node = Parse(expression, datapack, variables.ToDictionary(v => v.ToString()!, v => v));
        var result = Evaluate(function, node);
        _tempIndex = 0;
        return result;
    }
    
    static IVariable Evaluate(Function function, INode node) {
        switch (node) {
            case VariableNode variableNode:
                return variableNode.Variable;
            
            case OperatorNode operatorNode:
                var left = Evaluate(function, operatorNode.Left!);
                var right = Evaluate(function, operatorNode.Right!);
                
                var result = Temp.Get(function.Datapack, $"#math{_tempIndex++}");
                function
                    .Operation(result, Operation.Assign, left)
                    .Operation(result, operatorNode.Operator, right);
                return result;
            default:
                throw new MathException($"Unexpected node type {node.GetType().Name}");
        }
    }
    
    static readonly Dictionary<char, (Operation operation, float priority)> _operations = new() {
        ['+'] = (Operation.Add, 2),
        ['-'] = (Operation.Subtract, 2),
        ['*'] = (Operation.Multiply, 1),
        ['/'] = (Operation.Divide, 1),
        ['%'] = (Operation.Modulo, 1)
    };

    interface INode { }
    
    class VariableNode : INode {
        public IVariable Variable;

        public VariableNode(IVariable variable) {
            Variable = variable;
        }
    }
    
    class OperatorNode : INode {
        public Operation Operator;
        public float Priority;
        public INode? Left;
        public INode? Right;
    }
    
    static INode Parse(string expression, Datapack datapack, IReadOnlyDictionary<string, IVariable> variables) {
        var nodes = new List<INode>();
        
        var index = 0;
        var parens = 0;
        
        while (index < expression.Length) {
            var c = expression[index];
            
            if (char.IsWhiteSpace(c)) {
                index++;
            } else if (c == '(') {
                parens++;
                index++;
            } else if (c == ')') {
                parens--;
                index++;
            } else if (_operations.TryGetValue(c, out var operation)) {
                nodes.Add(new OperatorNode {
                    Operator = operation.operation,
                    Priority = operation.priority + index / 100f - parens * 3,
                });
                index += 1;
            } else {
                ParseValue();
            }
        }

        var operatorNodes = nodes.OfType<OperatorNode>().OrderBy(n => n.Priority).ToArray();
        
        foreach (var operatorNode in operatorNodes) {
            var i = nodes.IndexOf(operatorNode);
            if (i == 0) {
                throw new MathException("Unexpected operator at start of expression");
            }
            if (i == nodes.Count - 1) {
                throw new MathException("Unexpected operator at end of expression");
            }
            operatorNode.Left = nodes[i - 1];
            operatorNode.Right = nodes[i + 1];
            nodes.RemoveAt(i + 1);
            nodes.RemoveAt(i - 1);
        }
        
        return nodes[0];
        
        void ParseValue() {
            var c = expression[index];
            if (char.IsDigit(c)) {
                ParseNumber();
            } else if (char.IsLetter(c)) {
                ParseVariable();
            } else {
                throw new MathException($"Unexpected character '{c}' at index {index}");
            }
        }
        
        void ParseNumber() {
            var start = index;
            while (index < expression.Length && char.IsDigit(expression[index])) {
                index++;
            }
            var value = int.Parse(expression[start..index]);
            nodes.Add(new VariableNode(Constants.Get(datapack, value)));
        }
        
        void ParseVariable() {
            var start = index;
            while (
                index < expression.Length && 
                !char.IsWhiteSpace(expression[index]) &&
                !_operations.ContainsKey(expression[index])
            ) {
                index++;
            }
            var name = expression[start..index];
            if (!variables.TryGetValue(name, out var variable)) {
                throw new MathException($"Unknown variable '{name}' at index {start}");
            }
            nodes.Add(new VariableNode(variable));
        }
    }
    
    class MathException : Exception {
        public MathException(string message) : base(message) { }
    }
}