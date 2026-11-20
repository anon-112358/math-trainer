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
            if (Left.IsOperation() && Right.IsOperation())
            {
                return new Operation(
                    Left.GetOperation().Solve(),
                    ChildrenRelation,
                    Right.GetOperation().Solve()
                ).Solve();
            }
            else if (!Left.IsOperation() && !Right.IsOperation())
            {
                return new Operation(
                    (Left.GetBranch().Solve()),
                    ChildrenRelation,
                    Right.GetBranch().Solve()
                ).Solve();
            }
            else if (Left.IsOperation() && !Right.IsOperation())
            {
                return new Operation(
                    Left.GetOperation().Solve(),
                    ChildrenRelation,
                    Right.GetBranch().Solve()
                ).Solve();
            }
            else if (!Left.IsOperation() && Right.IsOperation())
            {
                return new Operation(
                    Left.GetBranch().Solve(),
                    ChildrenRelation,
                    Right.GetOperation().Solve()
                ).Solve();
            }
            else
            {
                throw new Exception();
            }
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
