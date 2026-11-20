namespace math_trainer.Equation
{
    public interface INode
    {
        public bool IsOperation();
        public string ToString();
        public decimal Solve();
        public Operation GetOperation();
        public EquationBinaryBranch GetBranch();
    }
}
