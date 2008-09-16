namespace QuickGraph.Unit
{
    public interface ITestCaseDecorator
    {
        ITestCase Decorate(ITestCase test);
    }
}
