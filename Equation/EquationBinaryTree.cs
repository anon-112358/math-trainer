namespace math_trainer.Equation
{
    record class EquationBinaryBranch(INode Left, INode Right, Operator ChildrenRelation) : INode
    {
        public static string Format(EquationBinaryBranch tree)
        {
            throw new NotImplementedException();
        }

        public static string Solve(EquationBinaryBranch tree)
        {
            throw new NotImplementedException();
        }

        public class EndNode(Operation<decimal> op) : INode
        {
            public Operation<decimal> Operation = op;
        }
    }
}
