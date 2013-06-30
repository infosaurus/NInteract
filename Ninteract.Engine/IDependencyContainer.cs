using System;

namespace Ninteract.Engine
{
    public interface IDependencyContainer<TSut, TCollaborator> where TSut          : class
                                                               where TCollaborator : class
    {
        void RegisterCollaborator(Func<TCollaborator> creator);
        TSut CreateSut();
    }
}