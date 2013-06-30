using Ninteract.Engine;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace Ninteract.Adapters
{
    public class AutoFixtureParameterFactory : IParameterFactory
    {
        private IFixture _fixture = new Fixture().Customize(new AutoMoqCustomization());

        public T Create<T>()
        {
            return _fixture.Create<T>();
        }
    }
}