// Copyright (c) 2013 Guillaume Lebur. All rights reserved.
//
// This software may be modified and distributed under the terms 
// of the MIT license.  See the LICENSE file for details.

using System;

namespace Ninteract.Engine.Exceptions
{
    public class ParameterNotFoundException : Exception
    {
        public ParameterNotFoundException(string name) : base(name)
        {
        }
    }
}