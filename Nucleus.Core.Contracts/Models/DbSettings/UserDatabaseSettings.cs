﻿namespace Nucleus.Core.Contracts.Models.DbSettings
{
    public class UserDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string UsersCollectionName { get; set; } = null!;
    }
}
