﻿using System;
using System.Data.EntityClient;
using System.Data.SqlClient;

namespace SuperAwesomeCode.DataModel.Entities
{
	/// <summary>Class for Entity Connection Settings.</summary>
	public sealed class EntityConnectionSettings
	{
		/// <summary>Determines if the database is backed by a file, which needs a different connection string.</summary>
		private bool _isAttachedDbFile;

		/// <summary>Initializes a new instance of the <see cref="EntityConnectionSettings"/> class.</summary>
		/// <param name="providerName">Name of the provider.</param>
		/// <param name="serverName">Name of the server.</param>
		/// <param name="databaseName">Name of the database.</param>
		/// <param name="metaDataRes">The meta data res.</param>
		/// <param name="userId">The user id.</param>
		/// <param name="password">The password.</param>
		public EntityConnectionSettings(
			string providerName, 
			string serverName, 
			string databaseName, 
			string metaDataRes, 
			string userId = null, 
			string password = null)
		{
			this.ProviderName = providerName;
			this.ServerName = serverName;
			this.DatabaseName = databaseName;
			this.MetaDataRes = metaDataRes;
			this.UserId = userId;
			this.Password = password;

			this._isAttachedDbFile = this.DatabaseName.StartsWith("|DataDirectory|", StringComparison.OrdinalIgnoreCase);
		}

		/// <summary>Gets the name of the provider.</summary>
		/// <value>The name of the provider.</value>
		public string ProviderName { get; private set; }

		/// <summary>Gets the name of the server.</summary>
		/// <value>The name of the server.</value>
		public string ServerName { get; private set; }

		/// <summary>Gets the name of the database.</summary>
		/// <value>The name of the database.</value>
		public string DatabaseName { get; private set; }

		/// <summary>Gets the meta data res.</summary>
		public string MetaDataRes { get; private set; }

		/// <summary>Gets the user id.</summary>
		public string UserId { get; private set; }

		/// <summary>Gets the password.</summary>
		public string Password { get; private set; }

		/// <summary>Builds the connection.</summary>
		/// <returns></returns>
		internal EntityConnection BuildConnection()
		{
			// Initialize the connection string builder for the underlying provider.
			SqlConnectionStringBuilder sqlBuilder;

			if (this._isAttachedDbFile)
			{
				sqlBuilder = new SqlConnectionStringBuilder()
				{
					DataSource = this.ServerName,
					AttachDBFilename = this.DatabaseName,
					IntegratedSecurity = true,
					UserInstance = true //Not sure if this is needed.
				};
			}
			else if (string.IsNullOrEmpty(this.UserId))
			{
				sqlBuilder = new SqlConnectionStringBuilder()
				{
					DataSource = this.ServerName,
					InitialCatalog = this.DatabaseName,
					IntegratedSecurity = true,
				};
			}
			else
			{
				sqlBuilder = new SqlConnectionStringBuilder()
				{
					DataSource = this.ServerName,
					InitialCatalog = this.DatabaseName,
					PersistSecurityInfo = true,
					UserID = this.UserId,
					Password = this.Password
				};
			}

			// Initialize the EntityConnectionStringBuilder.
			EntityConnectionStringBuilder entityBuilder = new EntityConnectionStringBuilder()
			{
				Provider = this.ProviderName,
				ProviderConnectionString = sqlBuilder.ToString(),
				Metadata = string.Format(@"res://*/{0}.csdl|res://*/{0}.ssdl|res://*/{0}.msl", this.MetaDataRes),
			};

			return new EntityConnection(entityBuilder.ToString());
		}
	}
}
