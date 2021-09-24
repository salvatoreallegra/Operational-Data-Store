using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using ODSApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODSApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ODSApi", Version = "v1" });
            });
            services.AddMicrosoftIdentityWebApiAuthentication(Configuration);
            services.AddSingleton<ILogService>(InitializeCosmosClientInstanceAsyncLogs(Configuration.GetSection("CosmosDb")).GetAwaiter().GetResult());
            services.AddSingleton<ITimeToBetterService>(InitializeCosmosClientInstanceAsyncTimeToBetter(Configuration.GetSection("CosmosDb")).GetAwaiter().GetResult());
            services.AddSingleton<IMatchRunService>(InitializeCosmosClientInstanceAsyncMatchRun(Configuration.GetSection("CosmosDb")).GetAwaiter().GetResult());
            services.AddSingleton<IMortalitySlopeService>(InitializeCosmosClientInstanceAsyncMortalitySlope(Configuration.GetSection("CosmosDb")).GetAwaiter().GetResult());

            services.AddApplicationInsightsTelemetry();
        }
    

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ODSApi v1"));
            }
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ODSApi v1"));
            app.UseHttpsRedirection();

            app.UseRouting();
         
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        private static async Task<LogService> InitializeCosmosClientInstanceAsyncLogs(IConfigurationSection configurationSection)
        {
            var databaseName = configurationSection["DatabaseName"];
            var containerName = configurationSection["ContainerName"];
            var account = configurationSection["Account"];
            var key = configurationSection["Key"];

            var client = new Microsoft.Azure.Cosmos.CosmosClient(account, key);
            var database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
            await database.Database.CreateContainerIfNotExistsAsync(containerName, "/id");

            var cosmosDbService = new LogService(client, databaseName, containerName);
            return cosmosDbService;
        }
        private static async Task<TimeToBetterService> InitializeCosmosClientInstanceAsyncTimeToBetter(IConfigurationSection configurationSection)
        {
            var databaseName = configurationSection["DatabaseName"];
            var containerName = "TimeToBetter";
            var account = configurationSection["Account"];
            var key = configurationSection["Key"];

            var client = new Microsoft.Azure.Cosmos.CosmosClient(account, key);
            var database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
            await database.Database.CreateContainerIfNotExistsAsync(containerName, "/id");

            var cosmosDbService = new TimeToBetterService(client, databaseName, containerName);
            return cosmosDbService;
        }
        private static async Task<IMatchRunService> InitializeCosmosClientInstanceAsyncMatchRun(IConfigurationSection configurationSection)
        {
            var databaseName = configurationSection["DatabaseName"];
            var containerName = "MatchRun";
            var account = configurationSection["Account"];
            var key = configurationSection["Key"];

            var client = new Microsoft.Azure.Cosmos.CosmosClient(account, key);
            var database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
            await database.Database.CreateContainerIfNotExistsAsync(containerName, "/id");
            

            var cosmosDBService = new MatchRunService(client, databaseName, containerName);

            return cosmosDBService;
        }
        private static async Task<IMortalitySlopeService> InitializeCosmosClientInstanceAsyncMortalitySlope(IConfigurationSection configurationSection)
        {
            var databaseName = configurationSection["DatabaseName"];
            var containerName = "MortalitySlope";
            var account = configurationSection["Account"];
            var key = configurationSection["Key"];

            var client = new Microsoft.Azure.Cosmos.CosmosClient(account, key);
            var database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
            await database.Database.CreateContainerIfNotExistsAsync(containerName, "/id");

            var cosmosDbService = new MortalitySlopeService(client, databaseName, containerName);
            return cosmosDbService;
        }
    }
}
