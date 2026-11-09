namespace math_trainer.Equation
{
    public record class EquationBinaryBranch(INode Left, INode Right, Operator ChildrenRelation)
        : INode
    {
        public static string Format(EquationBinaryBranch tree)
        {
            throw new NotImplementedException();
        }

        public static EquationBinaryBranch Format(string tree)
        {
            throw new NotImplementedException();
        }

        public static decimal Solve(EquationBinaryBranch tree)
        {
            if (tree.Left.IsOperation() && tree.Right.IsOperation())
            {
                return new Operation<decimal>(
                    tree.Left.GetOperation().Solve(),
                    tree.ChildrenRelation,
                    tree.Right.GetOperation().Solve()
                ).Solve();
            }
            else if (!tree.Left.IsOperation() && !tree.Right.IsOperation())
            {
                return new Operation<decimal>(
                    Solve(tree.Left.GetBranch()),
                    tree.ChildrenRelation,
                    Solve(tree.Right.GetBranch())
                ).Solve();
            }
            else if (tree.Left.IsOperation() && !tree.Right.IsOperation())
            {
                return new Operation<decimal>(
                    tree.Left.GetOperation().Solve(),
                    tree.ChildrenRelation,
                    Solve(tree.Right.GetBranch())
                ).Solve();
            }
            else if (!tree.Left.IsOperation() && tree.Right.IsOperation())
            {
                return new Operation<decimal>(
                    Solve(tree.Left.GetBranch()),
                    tree.ChildrenRelation,
                    tree.Right.GetOperation().Solve()
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

        Operation<decimal> INode.GetOperation()
        {
            throw new Exception();
        }

        bool INode.IsOperation()
        {
            return false;
        }
    }
}
