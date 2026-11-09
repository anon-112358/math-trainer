namespace math_trainer.Equation
{
    public interface INode
    {
        public bool IsOperation();
        public Operation<decimal> GetOperation();
        public EquationBinaryBranch GetBranch();
    }
}
