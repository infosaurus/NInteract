using Ninteract.Engine;

namespace Ninteract.Adapters
{
    public class MoqFactory<T> : IFakeFactory<T> where T : class
    {
        public IFake<T> Create()
        {
            return new MoqFake<T>();
        }
    }
}