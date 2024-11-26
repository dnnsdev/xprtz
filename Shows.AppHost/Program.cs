IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

IResourceBuilder<SqlServerServerResource> sqlContainer = builder.AddSqlServer("hoi")
                                                                .WithDataVolume("xprtz");

IResourceBuilder<SqlServerDatabaseResource> sqlDatabase = sqlContainer.AddDatabase("showsdb");

builder.AddProject<Projects.MigrationService>("migrationservice")
       .WithReference(sqlDatabase);

builder.AddProject<Projects.Shows_Api>("shows")
       .WithReference(sqlDatabase);

await builder.Build().RunAsync();