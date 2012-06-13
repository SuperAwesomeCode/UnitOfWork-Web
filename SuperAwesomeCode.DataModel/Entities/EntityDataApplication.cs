using System;
using Ninject;
using Ninject.Modules;

namespace SuperAwesomeCode.DataModel.Entities
{
	/// <summary>Class that initializes the application.</summary>
	public static class EntityDataApplication
	{
		//TODO: Should NinjectSettings be exposed?
		//TODO: Not allow DataApplicationSettings to be changeable

		/// <summary>Initializes the application.</summary>
		/// <param name="ninjectModule">Custom NinjectModule.</param>
		/// <param name="ninjectSettings">Custom NinjectSettings.</param>
		/// <param name="entityConnectionContainers">The entity connection containers.</param>
		public static void Initialize(
			INinjectModule ninjectModule = null,
			INinjectSettings ninjectSettings = null,
			params EntityConnectionContainer[] entityConnectionContainers)
		{
			Guard.AgainstNull(entityConnectionContainers, "entityConnectionContainers");

			try
			{
				IoC.Initialize(ninjectSettings, new EntityDataApplicationModule(entityConnectionContainers), ninjectModule);
			}
			catch (Exception)
			{
				//Do Nothing
			}
		}
	}
}