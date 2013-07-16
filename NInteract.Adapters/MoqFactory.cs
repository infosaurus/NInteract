// Copyright (c) 2013 Guillaume Lebur. All rights reserved.
//
// This software may be modified and distributed under the terms 
// of the MIT license.  See the LICENSE file for details.

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