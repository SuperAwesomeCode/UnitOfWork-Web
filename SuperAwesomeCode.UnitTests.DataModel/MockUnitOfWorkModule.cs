using System.Collections.Generic;
using System.Linq;
using Ninject.Modules;
using SuperAwesomeCode.DataModel;

namespace SuperAwesomeCode.UnitTests.DataModel
{
	public sealed class MockUnitOfWorkModule : NinjectModule
	{
		private IUnitOfWork _unitOfWork;

		public MockUnitOfWorkModule(IUnitOfWork unitOfWork = null)
		{
			this._unitOfWork = unitOfWork;
		}

		/// <summary>Loads the module into the kernel.</summary>
		public override void Load()
		{
			if (this._unitOfWork == null)
			{
				this.Bind<IUnitOfWork>().To<MockUnitOfWork>();
			}
			else
			{
				this.Bind<IUnitOfWork>().ToConstant(this._unitOfWork);
			}
		}
	}
}