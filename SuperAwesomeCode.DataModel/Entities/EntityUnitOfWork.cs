using System;
using System.Collections.Generic;
using Ninject;

namespace SuperAwesomeCode.DataModel.Entities
{
    /// <summary>Class which Implements of IUnitOfWork for Entities repositories.</summary>
    internal sealed class EntityUnitOfWork : IUnitOfWork
    {
        /// <summary>IObjectContextOrchestrator for this unit of work.</summary>
        private IObjectContextOrchestrator _objectContextOrchestrator;

        /// <summary>Activation context to resolve unkown repositories.</summary>
        private IKernel _kernel;

        /// <summary>Dictionary of Repositories.</summary>
        private Dictionary<Type, object> _repositories;

        /// <summary>Initializes a new instance of the <see cref="EntityUnitOfWork" /> class.</summary>
        /// <param name="objectContextOrchestrator">The object context orchestrator.</param>
        /// <param name="kernel">The ninject kernel.</param>
        public EntityUnitOfWork(IObjectContextOrchestrator objectContextOrchestrator, IKernel kernel)
        {
            Guard.AgainstNull(objectContextOrchestrator);
            this._objectContextOrchestrator = objectContextOrchestrator;
            this._repositories = new Dictionary<Type, object>();
            this._kernel = kernel;
        }

        /// <summary>Determines whether the TEntityType has repository.</summary>
        /// <typeparam name="TEntityType">TEntityType to see if a RepositryExists.</typeparam>
        /// <returns></returns>
        public bool HasRepository<TEntityType>() where TEntityType : class
        {
            return this._objectContextOrchestrator.GetObjectContext<TEntityType>() != null;
        }

        /// <summary>Retrieves a Repository for the given type.</summary>
        /// <typeparam name="TEntityType">TEntityType for the Repository to retrieve.</typeparam>
        /// <returns></returns>
        public IRepository<TEntityType> Repository<TEntityType>() where TEntityType : class
        {
            if (!this._repositories.ContainsKey(typeof(TEntityType)))
            {
                var objectContext = this._objectContextOrchestrator.GetObjectContext<TEntityType>();
                if (objectContext != null)
                {
                    this._repositories.Add(typeof(TEntityType), new GenericEntityRepository<TEntityType>(objectContext));
                }
                else
                {
                    var repository = this._kernel.Get<IRepository<TEntityType>>();
                    if (repository == null)
                    {
                        throw new Exception(string.Format("Unable to resolve repository for ({0}).", typeof(TEntityType).ToString()));
                    }

                    this._repositories.Add(typeof(TEntityType), repository);
                }
            }

            return this._repositories[typeof(TEntityType)] as IRepository<TEntityType>;
        }

        /// <summary>Saves the unit of work.</summary>
        public void Save()
        {
            this._objectContextOrchestrator.Save();
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            if (this._objectContextOrchestrator != null)
            {
                this._objectContextOrchestrator.Dispose();
            }
        }
    }
}
