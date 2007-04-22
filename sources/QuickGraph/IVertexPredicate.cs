
namespace QuickGraph
{
    public interface IVertexPredicate<Vertex>
    {
        bool Test(Vertex v);
    }
}
