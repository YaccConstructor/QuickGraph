namespace QuickGraph.Unit
{
    public interface ITestCaseFilter
    {
        bool Filter(IFixture fixture, ITestCase test);
    }
}
