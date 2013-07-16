// Copyright (c) 2013 Guillaume Lebur. All rights reserved.
//
// This software may be modified and distributed under the terms 
// of the MIT license.  See the LICENSE file for details.

using System;
using Ninteract.Engine;
using Ploeh.AutoFixture;

namespace Ninteract.Adapters
{
    public class AutoFixtureDependencyContainer<TSut, TCollaborator> : IDependencyContainer<TSut, TCollaborator> where TSut          : class
                                                                                                                 where TCollaborator : class
    {
        private readonly Fixture _fixture;

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
