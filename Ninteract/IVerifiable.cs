// Copyright (c) 2013 Guillaume Lebur. All rights reserved.
//
// This software may be modified and distributed under the terms 
// of the MIT license.  See the LICENSE file for details.

using System;
using System.Linq.Expressions;

namespace Ninteract
{
     public interface IVerifiable<TCollaborator> where TCollaborator : class
     {
         void                       ShouldTell(Expression<Action<TCollaborator>> action);
         IVerifiable<TCollaborator> ShouldTell(Expression<Action<TCollaborator>> action, IShouldChainer shouldChainer);

         void                       ShouldAsk<TResult>(Expression<Func<TCollaborator, TResult>> function);
         IVerifiable<TCollaborator> ShouldAsk<TResult>(Expression<Func<TCollaborator, TResult>> function, IShouldChainer shouldChainer);

         void                       ShouldGet<TResult>(Expression<Func<TCollaborator, TResult>> function);
         IVerifiable<TCollaborator> ShouldGet<TResult>(Expression<Func<TCollaborator, TResult>> function, IShouldChainer shouldChainer);

         void                       ShouldSet(Action<TCollaborator> setAction);
         IVerifiable<TCollaborator> ShouldSet(Action<TCollaborator> setAction, IShouldChainer shouldChainer);

         // Necessarily final verification method in chain since exception interrupts program flow
         void ShouldThrow<TException>() where TException : Exception; 
     }
}