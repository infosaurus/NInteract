namespace NInteract
{
    public interface IFakeFactory<T> where T : class
    {
        IFake<T> Create();
    }
}
