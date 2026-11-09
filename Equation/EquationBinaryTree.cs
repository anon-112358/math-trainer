namespace math_trainer.Equation
{
    class EquationBinaryTree
    {
        Operation<decimal> value;
        readonly EquationBinaryTree? left = default;
        readonly EquationBinaryTree? right = default;

        public EquationBinaryTree(
            Operation<decimal> inValue = default,
            EquationBinaryTree? inLeft = default,
            EquationBinaryTree? inRight = default
        )
        {
            if (value != default && (left != default || right != default))
                throw new Exception();
            value = inValue;
            left = inLeft;
            right = inRight;
        }

        public static string Format(EquationBinaryTree tree)
        {
            throw new NotImplementedException();
        }

        public static string Solve(EquationBinaryTree tree)
        {
            throw new NotImplementedException();
        }
    }
}
