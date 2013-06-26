using Moq;

namespace NInteract
{
    class MoqFactory<T> : IFakeFactory<T> where T : class
    {
        public IFake<T> Create()
        {
            return new MoqFake<T>();
        }
    }
}