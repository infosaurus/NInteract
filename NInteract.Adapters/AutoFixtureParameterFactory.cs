// Copyright (c) 2013 Guillaume Lebur. All rights reserved.
//
// This software may be modified and distributed under the terms 
// of the MIT license.  See the LICENSE file for details.

using Ninteract.Engine;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace Ninteract.Adapters
{
    public class AutoFixtureParameterFactory : IParameterFactory
    {
        private readonly IFixture _fixture = new Fixture().Customize(new AutoMoqCustomization());

        public T Create<T>()
        {
            return _fixture.Create<T>();
        }
    }
}