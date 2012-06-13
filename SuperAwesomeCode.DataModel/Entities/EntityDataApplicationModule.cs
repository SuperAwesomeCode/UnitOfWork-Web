using System.Collections.Generic;
using System.Linq;
using Ninject.Modules;

namespace SuperAwesomeCode.DataModel.Entities
{
	/// <summary>EntityDataApplicationModule class.</summary>
	internal sealed class EntityDataApplicationModule : NinjectModule
	{
		/// <summary>All of the connection containers.</summary>
		private IEnumerable<EntityConnectionContainer> _EntityConnectionContainers;

		/// <summary>Initializes a new instance of the <see cref="EntityDataApplicationModule"/> class.</summary>
		/// <param name="entityConnectionContainers">The entity connection containers.</param>
		internal EntityDataApplicationModule(IEnumerable<EntityConnectionContainer> entityConnectionContainers)
		{
			this._EntityConnectionContainers = entityConnectionContainers;
		}

		/// <summary>Loads the module into the kernel.</summary>
		public override void Load()
		{
			foreach (var container in this._EntityConnectionContainers)
			{
				//TOOD: Figure out why Ninject stopped using the correct constructor
				this.Bind(container.ObjectContextType).ToMethod(container.GetObjectContext).InTransientScope();
			}

			this.Bind<BatchedEntityDataContext>().ToMethod(this.ObtainBatchedDataContext).InTransientScope();

			this.Bind<IUnitOfWork>().To<EntityUnitOfWork>();
		}

		/// <summary>Obtains the BatchedEntityDataContext.</summary>
		/// <param name="context">The ninject context.</param>
		/// <returns></returns>
		private BatchedEntityDataContext ObtainBatchedDataContext(Ninject.Activation.IContext context)
		{
			return new BatchedEntityDataContext(this._EntityConnectionContainers.Select(i => i.ObjectContextType));
		}
	}
}