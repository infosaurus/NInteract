// Copyright (c) 2013 Guillaume Lebur. All rights reserved.
//
// This software may be modified and distributed under the terms 
// of the MIT license.  See the LICENSE file for details.

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