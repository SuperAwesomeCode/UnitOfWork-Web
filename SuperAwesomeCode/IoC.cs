using System;
using System.Linq;
using Ninject;
using Ninject.Modules;

namespace SuperAwesomeCode
{
    /// <summary>Class that is used to retrieve objects using Inversion of Control.</summary>
    public class IoC : IDisposable
    {
        /// <summary>Ninject Kernel.</summary>
        private IKernel _kernel;

        /// <summary>Initializes a new instance of the <see cref="IoC"/> class.</summary>
        /// <param name="settings">Settings to use for the kernel or null.</param>
        /// <param name="modules">>Modules to use for the kernel or null</param>
        public IoC(INinjectSettings settings = null, params INinjectModule[] modules)
        {
            this._kernel = new StandardKernel(settings ?? new NinjectSettings(), modules.Where(i => i != null).ToArray());
        }

        /// <summary>Use to resolve a type by using IoC.</summary>
        /// <typeparam name="T">Type of object to retrieve.</typeparam>
        /// <returns>Object of type T.</returns>
        public T Resolve<T>()
        {
            return this._kernel.Get<T>();
        }

        /// <summary>Use to resolve a type by using IoC.</summary>
        /// <param name="type">The type of object to retrieve.</param>
        /// <returns>Object of the given type.</returns>
        public object Resolve(Type type)
        {
            return this._kernel.Get(type);
        }

        /// <summary>Release's an object bound in IoC.</summary>
        /// <param name="instance">Instance of the object to release.</param>
        /// <returns>True if the object was released, otherwise fale.</returns>
        public bool Release(object instance)
        {
            return this._kernel.Release(instance);
        }

        /// <summary>Disposes the IoC.</summary>
        public void Dispose()
        {
            this._kernel.Dispose();
            this._kernel = null;
        }
    }
}