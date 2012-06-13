using System;
using System.Linq;
using Ninject;
using Ninject.Modules;

namespace SuperAwesomeCode
{
	/// <summary>Class that is used to retrieve objects using Inversion of Control.</summary>
	public static class IoC
	{
		/// <summary>Ninject Kernel.</summary>
		private static IKernel _Kernel;

		/// <summary>Use to resolve a type by using IoC.</summary>
		/// <typeparam name="T">Type of object to retrieve.</typeparam>
		/// <returns>Object of type T.</returns>
		public static T Resolve<T>()
		{
			return IoC._Kernel.Get<T>();
		}

		/// <summary>Use to resolve a type by using IoC.</summary>
		/// <param name="type">The type of object to retrieve.</param>
		/// <returns>Object of the given type.</returns>
		public static object Resolve(Type type)
		{
			return IoC._Kernel.Get(type);
		}

		/// <summary>Release's an object bound in IoC.</summary>
		/// <param name="instance">Instance of the object to release.</param>
		/// <returns>True if the object was released, otherwise fale.</returns>
		public static bool Release(object instance)
		{
			return IoC._Kernel.Release(instance);
		}

		/// <summary>Disposes the IoC.</summary>
		public static void Dispose()
		{
			IoC._Kernel.Dispose();
			IoC._Kernel = null;
		}

		/// <summary>Initializes a new IoC Kernel.</summary>
		/// <param name="settings">Settings to use for the kernel or null.</param>
		/// <param name="modules">Moules to use for the kernel or null.</param>
		public static void Initialize(INinjectSettings settings, params INinjectModule[] modules)
		{
			if (IoC._Kernel != null)
			{
				throw new InvalidOperationException("IoC has already been initialized.");
			}

			IoC._Kernel = new StandardKernel(settings ?? new NinjectSettings(), modules.Where(i => i != null).ToArray());
		}
	}
}