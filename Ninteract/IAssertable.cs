using System;

namespace Ninteract
{
    public interface IAssertable<TSut, TCollaborator> : IAssumable<TSut, TCollaborator>,
                                                        IVerifiable<TCollaborator> 
                                                        where TSut          : class
                                                        where TCollaborator : class
    {
    }
}
