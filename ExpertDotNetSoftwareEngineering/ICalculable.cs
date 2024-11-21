namespace ExpertDotNetSoftwareEngineering;

public interface ICalculable<T>
 where T : ICalculable<T>
{
    static abstract T Add(T a, T b);
    static abstract T Subtract(T a, T b);
}
