using System;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Reflection;

namespace SuperAwesomeCode.DataModel.Entities
{
    /// <summary>Class that contains an entity connection and constructor information.</summary>
    public sealed class EntityConnectionContainer
    {
        /// <summary>Cached constructor info of the EntityConnection.</summary>
        private ConstructorInfo _constructorInfo;

        /// <summary>EntitySettings used to create the connection.</summary>
        private EntityConnectionSettings _settings;

        /// <summary>Prevents a default instance of the <see cref="EntityConnectionContainer"/> class from being created.</summary>
        private EntityConnectionContainer()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="EntityConnectionContainer"/> class.</summary>
        /// <param name="objectContextType">Type of the object context.</param>
        /// <param name="settings">The entity connection settings.</param>
        private EntityConnectionContainer(Type objectContextType, EntityConnectionSettings settings)
        {
            if (!typeof(ObjectContext).IsAssignableFrom(objectContextType))
            {
                throw new ArgumentException(string.Format("{0} is not an ObjectContext.", objectContextType.FullName));
            }

            this._settings = settings;
            this._constructorInfo = objectContextType.GetConstructor(new Type[] { typeof(EntityConnection) });

            this.ObjectContextType = objectContextType;
        }

        /// <summary>
        /// Gets the type of the object context.</summary>
        public Type ObjectContextType { get; private set; }

        /// <summary>Creates the specified settings.</summary>
        /// <typeparam name="TObjectContext">The type of the object context.</typeparam>
        /// <param name="settings">The settings.</param>
        /// <returns></returns>
        public static EntityConnectionContainer Create<TObjectContext>(EntityConnectionSettings settings)
            where TObjectContext : ObjectContext
        {
            return new EntityConnectionContainer(typeof(TObjectContext), settings);
        }

        /// <summary>Creates the specified provider name.</summary>
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

        /// <summary>Gets the object context.</summary>
        /// <returns></returns>
        public ObjectContext GetObjectContext()
        {
            return this._constructorInfo.Invoke(new object[] { this._settings.BuildConnection() }) as ObjectContext;
        }
    }
}