using System;

namespace NInteract
{
    public interface IDependencyContainer<TSut, TCollaborator> where TSut          : class
                                                               where TCollaborator : class
    {
        void RegisterCollaborator(Func<TCollaborator> creator);
        TSut CreateSut();
    }
}