using System.Data.EntityClient;
using System.Data.SqlClient;

namespace SuperAwesomeCode.Data
{
	/// <summary>
	/// Class for Entity Connection Settings.
	/// </summary>
	public sealed class EntityConnectionSettings
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="EntityConnectionSettings"/> class.
		/// </summary>
		/// <param name="providerName">Name of the provider.</param>
		/// <param name="serverName">Name of the server.</param>
		/// <param name="databaseName">Name of the database.</param>
		/// <param name="metaDataRes">The meta data res.</param>
		public EntityConnectionSettings(string providerName, string serverName, string databaseName, string metaDataRes)
		{
			this.ProviderName = providerName;
			this.ServerName = serverName;
			this.DatabaseName = databaseName;
			this.MetaDataRes = metaDataRes;
		}

		/// <summary>
		/// Gets the name of the provider.
		/// </summary>
		public string ProviderName { get; private set; }

		/// <summary>
		/// Gets the name of the server.
		/// </summary>
		public string ServerName { get; private set; }

		/// <summary>
		/// Gets the name of the database.
		/// </summary>
		public string DatabaseName { get; private set; }

		/// <summary>
		/// Gets the meta data res.
		/// </summary>
		public string MetaDataRes { get; private set; }

		/// <summary>
		/// Builds the connection.
		/// </summary>
		/// <param name="entityConnectionSettings">The entity connection settings.</param>
		/// <param name="metaDataRes">The meta data res.</param>
		/// <returns></returns>
		internal EntityConnection BuildConnection()
		{
			// Initialize the connection string builder for the underlying provider.
			SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder()
			{
				DataSource = this.ServerName,
				InitialCatalog = this.DatabaseName,
				IntegratedSecurity = true
			};

			// Initialize the EntityConnectionStringBuilder.
			EntityConnectionStringBuilder entityBuilder = new EntityConnectionStringBuilder()
			{
				Provider = this.ProviderName,
				ProviderConnectionString = sqlBuilder.ToString(),
				Metadata = string.Format(@"res://*/{0}.csdl|res://*/{0}.ssdl|res://*/{0}.msl", this.MetaDataRes)
			};

			return new EntityConnection(entityBuilder.ToString());
		}
	}
}
