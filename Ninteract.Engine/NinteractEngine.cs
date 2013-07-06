using System;
using System.Collections.Generic;
using System.Linq;

namespace Ninteract.Engine
{
    public class NinteractEngine<TSut, TCollaborator> where TSut          : class 
                                                      where TCollaborator : class
    {
        private TSut _sut;
        private IList<IFake<TCollaborator>> _fakeCollaborators = new List<IFake<TCollaborator>>();
        private IList<IExpectation<TCollaborator>> _expectations = new List<IExpectation<TCollaborator>>();
        private IFakeFactory<TCollaborator> _fakeCollaboratorFactory;
        private IDependencyContainer<TSut, TCollaborator> _dependencyContainer;

        public Stimulus<TSut> Stimulus { get; set; }

        public NinteractEngine(IDependencyContainer<TSut, TCollaborator> dependencyContainer, IFakeFactory<TCollaborator> fakeFactory)
        {
            _dependencyContainer = dependencyContainer;
            _fakeCollaboratorFactory = fakeFactory;

            _dependencyContainer.RegisterCollaborator(() =>
            {
                var collaborator = _fakeCollaboratorFactory.Create();
                _fakeCollaborators.Add(collaborator);
                return collaborator.Illusion;
            });
            _sut = _dependencyContainer.CreateSut();
        }

        public void Expect(IExpectation<TCollaborator> expectation)
        {
            _expectations.Add(expectation);
        }

        public void Run()
        {
            TriggerStimulus();
            var mock = GetCollaboratorMockOrThrow();

            _expectations.ToList().ForEach(e => e.VerifyAgainst(mock));
        }

        private void TriggerStimulus()
        {
            Stimulus.ApplyTo(_sut);
        }

        private IFake<TCollaborator> GetCollaboratorMockOrThrow(Func<IFake<TCollaborator>, bool> mockSelector = null)
        {
            IFake<TCollaborator> mock;
            var noCollaboratorSelectorPrecisionMessage = string.Empty;
            if (mockSelector == null)
            {
                if (_fakeCollaborators.Count > 1)
                    throw new InvalidOperationException(
                        string.Format(
                            "Type {0} has more than 1 injectable collaborator of type {1}. Use a selector to choose one of them.",
                            typeof(TSut).FullName, typeof(TCollaborator).FullName));
                mockSelector = anyMock => true; // there's only one anyway
            }
            else
            {
                noCollaboratorSelectorPrecisionMessage = "matching selector";
            }
            mock = _fakeCollaborators.FirstOrDefault(mockSelector);
            if (mock == null)
            {
                throw new InvalidOperationException(string.Format("Type {0} has no injectable collaborator of type {1} {2}.", typeof(TSut).FullName, typeof(TCollaborator).FullName, noCollaboratorSelectorPrecisionMessage));
            }
            return mock;
        }
    }
}
