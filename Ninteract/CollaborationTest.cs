﻿// Copyright (c) 2013 Guillaume Lebur. All rights reserved.
//
// This software may be modified and distributed under the terms 
// of the MIT license.  See the LICENSE file for details.


using System;
using System.Linq.Expressions;
using Ninteract.Adapters;
using Ninteract.Engine;

namespace Ninteract
{
    /// <summary>
    /// Base class for NInteract collaboration tests. Provides syntax starters and shortcuts for expressing assertions
    /// </summary>
    /// <typeparam name="TSut"></typeparam>
    /// <typeparam name="TCollaborator"></typeparam>
    public class CollaborationTest<TSut, TCollaborator> where TSut          : class 
                                                        where TCollaborator : class
    {
        private AssertionBuilder<TSut, TCollaborator> _assertionBuilder;
        private IParameterEngine                      _parameterEngine;

        public ICallable<TSut, TCollaborator> A 
        { 
            get
            {
                _assertionBuilder = new AssertionBuilder<TSut, TCollaborator>();
                _parameterEngine  = new AutoMoqParameterEngine();
                return _assertionBuilder;
            } 
        }

        public IShouldChainer And { get { return new AndShouldChainer(); } }

        public T Some<T>()
        {
            return _parameterEngine.Some<T>();
        }

        public T Some<T>(Expression<Predicate<T>> predicate)
        {
            return _parameterEngine.Some<T>(predicate);
        }

        public T TheSame<T>()
        {
            return _parameterEngine.TheSame<T>();
        }

        public T Any<T>()
        {
            return _parameterEngine.Any<T>();
        }
    }
}
