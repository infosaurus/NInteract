namespace Ninteract.Engine
{
    public interface IParameterFactory
    {
        T Create<T>();
    }
}