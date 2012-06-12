using System;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Reflection;

namespace SuperAwesomeCode.DataModel.Entities
{
	/// <summary>
	/// 
	/// </summary>
	public sealed class EntityConnectionContainer
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="EntityConnectionSettings"/> class.
		/// </summary>
		/// <param name="objectContextType">Type of the object context.</param>
		/// <param name="settings">The settings.</param>
		private EntityConnectionContainer(Type objectContextType, EntityConnectionSettings settings)
		{
			this._Settings = settings;
			this._ConstructorInfo = objectContextType.GetConstructor(new Type[] { typeof(EntityConnection) });

			this.ObjectContextType = objectContextType;
		}

		private ConstructorInfo _ConstructorInfo;

		private EntityConnectionSettings _Settings;

		public Type ObjectContextType { get; private set; }

		/// <summary>
		/// Creates the specified settings.
		/// </summary>
		/// <typeparam name="TObjectContext">The type of the object context.</typeparam>
		/// <param name="settings">The settings.</param>
		/// <returns></returns>
		public static EntityConnectionContainer Create<TObjectContext>(EntityConnectionSettings settings) 
			where TObjectContext : ObjectContext
		{
			return new EntityConnectionContainer(typeof(TObjectContext), settings);
		}

		/// <summary>
		/// Creates the specified provider name.
		/// </summary>
		/// <typeparam name="TObjectContext">The type of the object context.</typeparam>
		/// <param name="providerName">Name of the provider.</param>
		/// <param name="serverName">Name of the server.</param>
		/// <param name="databaseName">Name of the database.</param>
		/// <param name="metaDataRes">The meta data res.</param>
		/// <returns></returns>
		public static EntityConnectionContainer Create<TObjectContext>(string providerName, string serverName, string databaseName, string metaDataRes)
			where TObjectContext : ObjectContext
		{
			return EntityConnectionContainer.Create<TObjectContext>(new EntityConnectionSettings(providerName, serverName, databaseName, metaDataRes));
		}

		/// <summary>
		/// Gets the object context.
		/// </summary>
		/// <returns></returns>
		public ObjectContext GetObjectContext(Ninject.Activation.IContext context)
		{
			var x  = this._ConstructorInfo.Invoke(new object[] { this._Settings.BuildConnection() }) as ObjectContext;
			return x;
		}
	}
}