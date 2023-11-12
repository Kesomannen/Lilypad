namespace Lilypad.Helpers; 

public static class TreeGenerator {
    public static void GenerateTreeWithDepth(this Function function, IVariable variable, Range<int> range, int depth, Action<Function, int> build) {
        Assert.IsTrue(depth > 0, "Depth must be greater than 0");
        
        var branches = Math.Max(Math.Pow(range.Max - range.Min, 1.0 / depth), 2);
        function.GenerateTree(variable, range, (int)branches, build);
    }
    
    public static void GenerateTree(this Function function, IVariable variable, Range<int> range, Action<Function, int> build) {
        function.GenerateTree(variable, range, 2, build);
    }
    
    public static void GenerateTree(this Function function, IVariable variable, Range<int> range, int branches, Action<Function, int> build) {
        Assert.IsTrue(range.Max > range.Min, "Min must be less than or equal to max");
        Assert.IsTrue(branches >= 2, "Branches must be greater than or equal to 2");
        
        var score = function.CopyToScore(variable, "#tree");
        function.If(Condition.Score(score, range), f => {
            f.Call(GenerateBranch(function, score, range, branches, build));
        }).Else(f => {
            f.LogWarning($"Value <{score}> out of generated tree range {range}", function);
        });
    }
    
    static Function GenerateBranch(Resource root, in ScoreVariable variable, Range<int> range, int branches, Action<Function, int> build) {
        var count = range.Max - range.Min + 1;
        var function = root.Datapack.Functions.Create($"{root.Name}/tree/{range.Min}_{range.Max}");

        if (count <= branches) {
            for (var i = range.Min; i <= range.Max; i++) {
                var index = i;
                function.Execute().If(Condition.Score(variable, i)).Run(f => build(f, index));
            }
        } else {
            for (var i = 0; i < branches; i++) {
                var min = range.Min + count * i / branches + 1;
                var max = range.Min + count * (i + 1) / branches;
                if (i == 0) min--;
                if (i == branches - 1) max--;

                var branchRange = (min, max);
                var branch = GenerateBranch(root, variable, branchRange, branches, build);
                function.Execute().If(Condition.Score(variable, branchRange)).Run(branch);
            }
        }
        return function;
    }
}