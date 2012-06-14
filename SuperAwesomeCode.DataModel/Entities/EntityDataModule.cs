using System.Collections.Generic;
using System.Linq;
using Ninject.Modules;

namespace SuperAwesomeCode.DataModel.Entities
{
	/// <summary>EntityDataModule class.</summary>
	public sealed class EntityDataModule : NinjectModule
	{
		/// <summary>All of the connection containers.</summary>
		private IEnumerable<EntityConnectionContainer> _EntityConnectionContainers;

		/// <summary>Initializes a new instance of the <see cref="EntityDataModule"/> class.</summary>
		/// <param name="entityConnectionContainers">The entity connection containers.</param>
		public EntityDataModule(params EntityConnectionContainer[] entityConnectionContainers)
		{
			this._EntityConnectionContainers = entityConnectionContainers;
		}

		/// <summary>Loads the module into the kernel.</summary>
		public override void Load()
		{
			this.Bind<BatchedEntityDataContext>().ToMethod(this.ObtainBatchedDataContext).InTransientScope();

			this.Bind<IUnitOfWork>().To<EntityUnitOfWork>();
		}

		/// <summary>Obtains the BatchedEntityDataContext.</summary>
		/// <param name="context">The ninject context.</param>
		/// <returns></returns>
		private BatchedEntityDataContext ObtainBatchedDataContext(Ninject.Activation.IContext context)
		{
			return new BatchedEntityDataContext(this._EntityConnectionContainers);
		}
	}
}