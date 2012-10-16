using System.Collections.Generic;
using Ninject;
using Ninject.Activation;
using Ninject.Modules;

namespace SuperAwesomeCode.DataModel.Entities
{
    /// <summary>EntityDataModule class.</summary>
    public class EntityDataModule : NinjectModule
    {
        /// <summary>Initializes a new instance of the <see cref="EntityDataModule"/> class.</summary>
        /// <param name="entityConnectionContainers">The entity connection containers.</param>
        public EntityDataModule(params EntityConnectionContainer[] entityConnectionContainers)
        {
            this.EntityConnectionContainers = entityConnectionContainers;
        }

        /// <summary>Gets or sets the entity connection containers.</summary>
        /// <value>The entity connection containers.</value>
        protected IEnumerable<EntityConnectionContainer> EntityConnectionContainers { get; set; }

        /// <summary>Loads the module into the kernel.</summary>
        public override void Load()
        {
            this.Bind<IObjectContextOrchestrator>().ToMethod(this.ObtainObjectContextOrchestrator).InTransientScope();
            this.Bind<IUnitOfWork>().ToMethod(this.ObtainUnitOfWork).InTransientScope();
        }

        /// <summary>Obtains the unit of work.</summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        protected virtual IUnitOfWork ObtainUnitOfWork(IContext context)
        {
            return new EntityUnitOfWork(this.Kernel.Get<IObjectContextOrchestrator>(), this.Kernel);
        }

        /// <summary>Obtains the BatchedEntityDataContext.</summary>
        /// <param name="context">The ninject context.</param>
        /// <returns></returns>
        protected virtual IObjectContextOrchestrator ObtainObjectContextOrchestrator(Ninject.Activation.IContext context)
        {
            return new ObjectContextOrchestrator(this.EntityConnectionContainers);
        }
    }
}