namespace SimpleDLL
{
    public class SimpleDLLFeedback : ISimpleDLLFeedback
    {
        private IWriter Writer { get; }

        public SimpleDLLFeedback(IWriter writer) => Writer = writer;

        public void SimpleMethod() => Writer.Write("SimpleMethod called");
    }
}
