using System.Collections.Generic;
using Ninject.Modules;
using System.Linq;

namespace SuperAwesomeCode.Data
{
	/// <summary>
	/// 	Module for the Cooptimum.Farmers.Data Assembly.
	/// </summary>
	internal class DataApplicationModule : NinjectModule
	{
		internal IEnumerable<EntityConnectionContainer> _EntityConnectionContainers;

		/// <summary>
		/// Initializes a new instance of the Cooptimum.Farmers.Data.DataApplicationModule class.
		/// </summary>
		/// <param name="entityConnectionContainers">The entity connection containers.</param>
		internal DataApplicationModule(IEnumerable<EntityConnectionContainer> entityConnectionContainers)
		{
			this._EntityConnectionContainers = entityConnectionContainers;
		}

		/// <summary>
		/// 	Loads the module into the kernel.
		/// </summary>
		public override void Load()
		{
			foreach (var container in this._EntityConnectionContainers)
			{
				//TOOD: Figure out why Ninject stopped using the correct constructor
				this.Bind(container.ObjectContextType).ToMethod(container.GetObjectContext).InTransientScope();
			}

			this.Bind<BatchedDataContext>().ToMethod(this.ObtainBatchedDataContext).InTransientScope();

			this.Bind<IUnitOfWork>().To<UnitOfWork>();
		}

		private BatchedDataContext ObtainBatchedDataContext(Ninject.Activation.IContext context)
		{
			return new BatchedDataContext(this._EntityConnectionContainers.Select(i => i.ObjectContextType));
		}
	}
}