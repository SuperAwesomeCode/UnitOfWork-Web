using System;
using Ninject;
using Ninject.Modules;
using System.Collections.Generic;

namespace SuperAwesomeCode.Data
{
	/// <summary>
	/// 	Class that initializes the application.
	/// </summary>
	public static class DataApplication
	{
		//TODO: Should NinjectSettings be exposed?
		//TODO: Not allow DataApplicationSettings to be changeable

		/// <summary>
		/// Initializes the application.
		/// </summary>
		/// <param name="entityConnectionContainers">The entity connection containers.</param>
		/// <param name="ninjectModule">Custom NinjectModule.</param>
		/// <param name="ninjectSettings">Custom NinjectSettings.</param>
		public static void Initialize(
			IEnumerable<EntityConnectionContainer> entityConnectionContainers,
			INinjectModule ninjectModule = null,
			INinjectSettings ninjectSettings = null)
		{
			Guard.AgainstNull(entityConnectionContainers, "entityConnectionContainers");

			try
			{
				IoC.Initialize(ninjectSettings, new DataApplicationModule(entityConnectionContainers), ninjectModule);
			}
			catch (Exception)
			{
				//Do Nothing
			}
		}
	}
}