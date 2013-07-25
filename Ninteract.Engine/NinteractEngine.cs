// Copyright (c) 2013 Guillaume Lebur. All rights reserved.
//
// This software may be modified and distributed under the terms 
// of the MIT license.  See the LICENSE file for details.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Ninteract.Engine
{
    public class NinteractEngine<TSut, TCollaborator> where TSut          : class 
                                                      where TCollaborator : class
    {
        private readonly TSut _sut;
        private readonly IList<IFake<TCollaborator>>                    _fakeCollaborators          = new List<IFake<TCollaborator>>();
        private readonly IList<ICollaboratorExpectation<TCollaborator>> _collaboratorExpectations   = new List<ICollaboratorExpectation<TCollaborator>>();
        private readonly IFakeFactory<TCollaborator>                    _fakeCollaboratorFactory;
        private readonly IDependencyContainer<TSut, TCollaborator>      _dependencyContainer;
        private IEncompassingExpectation                                _headEncompassingExpectation;
        

        public Stimulus<TSut> Stimulus { get; set; }

        public NinteractEngine(IDependencyContainer<TSut, TCollaborator> dependencyContainer, IFakeFactory<TCollaborator> fakeFactory)
        {
            _dependencyContainer     = dependencyContainer;
            _fakeCollaboratorFactory = fakeFactory;

            _dependencyContainer.RegisterCollaborator(() =>
                                {
                                    var collaborator = _fakeCollaboratorFactory.Create();
                                    _fakeCollaborators.Add(collaborator);
                                    return collaborator.Illusion;
                                });

            _sut = _dependencyContainer.CreateSut();
        }

        public void Run()
        {
            if (_headEncompassingExpectation != null)
                _headEncompassingExpectation.TriggerAndVerify(Stimulus, _sut);
            else
                TriggerStimulus();

            var mock = GetFakeCollaboratorOrThrow();
            _collaboratorExpectations.ToList().ForEach(e => e.VerifyAgainst(mock));
        }

        private void TriggerStimulus()
        {
            Stimulus.ApplyTo(_sut);
        }

        private IFake<TCollaborator> GetFakeCollaboratorOrThrow(Func<IFake<TCollaborator>, bool> fakeSelector = null)
        {
            IFake<TCollaborator> fake;
            var noCollaboratorSelectorPrecisionMessage = string.Empty;
            if (fakeSelector == null)
            {
                if (_fakeCollaborators.Count > 1)
                    throw new InvalidOperationException(
                        string.Format("Type {0} has more than 1 injectable collaborator of type {1}. Use a selector to choose one of them.",
                                      typeof(TSut).FullName, 
                                      typeof(TCollaborator).FullName));

                fakeSelector = anyFake => true; // there's only one anyway
            }
            else
            {
                noCollaboratorSelectorPrecisionMessage = "matching selector";
            }

            fake = _fakeCollaborators.FirstOrDefault(fakeSelector);

            if (fake == null)
            {
                throw new InvalidOperationException(string.Format("Type {0} has no injectable collaborator of type {1} {2}.", 
                                                                  typeof(TSut).FullName, typeof(TCollaborator).FullName, 
                                                                  noCollaboratorSelectorPrecisionMessage));
            }
            return fake;
        }

        public void Expect(ICollaboratorExpectation<TCollaborator> expectation)
        {
            _collaboratorExpectations.Add(expectation);
        }

        public void AddEncompassingExpectation(IEncompassingExpectation expectation)
        {
            if (_headEncompassingExpectation == null)
                _headEncompassingExpectation = expectation;
            else
                _headEncompassingExpectation.Enqueue(expectation);
        }

        public void Assume<TValue>(ReturnsAssumption<TCollaborator, TValue> returnsAssumption)
        {
            var fake = GetFakeCollaboratorOrThrow();
            returnsAssumption.ApplyOn(fake);
        }

        public void Assume<TException>(ActionThrowsAssumption<TCollaborator, TException> actionThrowsAssumption) 
            where TException : Exception, new()
        {
            var fake = GetFakeCollaboratorOrThrow();
            actionThrowsAssumption.ApplyOn(fake);
        }

        public void Assume<TException, TValue>(FunctionThrowsAssumption<TCollaborator, TException, TValue> actionThrowsAssumption) 
            where TException : Exception, new()
        {
            var fake = GetFakeCollaboratorOrThrow();
            actionThrowsAssumption.ApplyOn(fake);
        }

        public void Assume<TException>(SetActionThrowsAssumption<TCollaborator, TException> actionThrowsAssumption)
            where TException : Exception, new()
        {
            var fake = GetFakeCollaboratorOrThrow();
            actionThrowsAssumption.ApplyOn(fake);
        }
    }
}
