using System;
using Ploeh.AutoFixture;

namespace NInteract
{
    public class AutoFixtureDependencyContainer<TSut, TCollaborator> : IDependencyContainer<TSut, TCollaborator> where TSut          : class
                                                                                                                 where TCollaborator : class
    {
        private Fixture _fixture;

        public AutoFixtureDependencyContainer()
        {
            _fixture = new Fixture();
        }

        public void RegisterCollaborator(Func<TCollaborator> creator)
        {
            _fixture.Register(creator);
        }

        public TSut CreateSut()
        {
            return _fixture.Create<TSut>();
        }
    }
}
