namespace Lilypad.Helpers; 

public static class VariableMath {
    static int _tempIndex = 0;
    
    public static T Evaluate<T>(this Function function, T output, string expression, params IVariable[] variables) where T : IVariable { 
        function.SetVariable(output, Evaluate(function, expression, variables));
        return output;
    }
    
    public static IVariable Evaluate(this Function function, string expression, params IVariable[] variables) {
        var datapack = function.Datapack;
        var node = Parse(expression, datapack, variables.ToDictionary(v => v.ToString()!, v => v));
        var result = Evaluate(function, node);
        _tempIndex = 0;
        return result.Variable;
    }
    
    static (IVariable Variable, bool Mutable) Evaluate(Function function, INode node) {
        switch (node) {
            case NumberNode numberNode:
                return (Constants.Get(function, numberNode.Value), false);
            
            case VariableNode variableNode:
                return (variableNode.Variable, false);
            
            case OperatorNode operatorNode:
                var left = Evaluate(function, operatorNode.Left!);
                var right = Evaluate(function, operatorNode.Right!);
                
                if (!left.Mutable) {
                    left.Variable = function.CopyTempScore(left.Variable, $"#math{_tempIndex++}");
                }
                
                function.Operation(left.Variable, operatorNode.Operator, right.Variable);
                return (left.Variable, true);
            default:
                throw new MathException($"Unexpected node type {node.GetType().Name}");
        }
    }
    
    static readonly Dictionary<char, (OperationType operation, float priority)> _operations = new() {
        ['+'] = (OperationType.Add, 2),
        ['-'] = (OperationType.Subtract, 2),
        ['*'] = (OperationType.Multiply, 1),
        ['/'] = (OperationType.Divide, 1),
        ['%'] = (OperationType.Modulo, 1)
    };

    interface INode { }
    
    class NumberNode : INode {
        public int Value;

        public NumberNode(int value) {
            Value = value;
        }
    }
    
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
            nodes.Add(new NumberNode(value));
        }
        
        void ParseVariable() {
            var start = index;
            while (
                index < expression.Length && (
                    expression[index] == IVariable.Separator ||
                    expression[index] == IVariable.Space || 
                        !char.IsWhiteSpace(expression[index]) && 
                        !_operations.ContainsKey(expression[index]) &&
                        expression[index] != '(' &&
                        expression[index] != ')'
                )
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