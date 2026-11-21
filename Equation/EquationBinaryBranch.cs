namespace math_trainer.Equation
{
    public record class EquationBinaryBranch(INode Left, INode Right, Operator ChildrenRelation)
        : INode
    {
        public string Format()
        {
            throw new NotImplementedException();
        }

        public static EquationBinaryBranch Format(string tree)
        {
            throw new NotImplementedException();
        }

        public decimal Solve()
        {
            return new Operation(Left.Solve(), ChildrenRelation, Right.Solve()).Solve();
        }

        EquationBinaryBranch INode.GetBranch()
        {
            return this;
        }

        Operation INode.GetOperation()
        {
            throw new Exception();
        }

        bool INode.IsOperation()
        {
            return false;
        }
    }
}
