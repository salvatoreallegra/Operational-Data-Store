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
using ODSApi.DBServices;
using ODSApi.Middleware;
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
            services.AddSingleton<ILogDBService>(InitializeCosmosClientInstanceAsyncLogs(Configuration.GetSection("CosmosDb")).GetAwaiter().GetResult());
            services.AddSingleton<ITimeToNextOfferDBService>(InitializeCosmosClientInstanceAsyncTimeToNextOffer(Configuration.GetSection("CosmosDb")).GetAwaiter().GetResult());
            services.AddSingleton<IMatchRunDBService>(InitializeCosmosClientInstanceAsyncMatchRun(Configuration.GetSection("CosmosDb")).GetAwaiter().GetResult());
            services.AddSingleton<IMortalitySlopeDBService>(InitializeCosmosClientInstanceAsyncMortalitySlope(Configuration.GetSection("CosmosDb")).GetAwaiter().GetResult());
            services.AddSingleton<IGraphParamsDBService>(InitializeCosmosClientInstanceAsyncGraphParams(Configuration.GetSection("CosmosDb")).GetAwaiter().GetResult());


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

            //Use this Middleware prior to app.UseEndpoints....
            app.UseMiddleware(typeof(ExceptionHandlingMiddleware));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            
            
        }
        private static async Task<LogDBService> InitializeCosmosClientInstanceAsyncLogs(IConfigurationSection configurationSection)
        {
            var databaseName = configurationSection["DatabaseName"];
            var containerName = configurationSection["ContainerName"];
            var account = configurationSection["Account"];
            var key = configurationSection["Key"];

            var client = new Microsoft.Azure.Cosmos.CosmosClient(account, key);
            var database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
            await database.Database.CreateContainerIfNotExistsAsync(containerName, "/matchId");

            var cosmosDbService = new LogDBService(client, databaseName, containerName);
            return cosmosDbService;
        }
        private static async Task<TimeToNextOfferDBService> InitializeCosmosClientInstanceAsyncTimeToNextOffer(IConfigurationSection configurationSection)
        {
            var databaseName = configurationSection["DatabaseName"];
            var containerName = "TimeToNextOfferData";
            var account = configurationSection["Account"];
            var key = configurationSection["Key"];

            var client = new Microsoft.Azure.Cosmos.CosmosClient(account, key);
            var database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
            await database.Database.CreateContainerIfNotExistsAsync(containerName, "/matchId");

            var cosmosDbService = new TimeToNextOfferDBService(client, databaseName, containerName);
            return cosmosDbService;
        }
        private static async Task<IMatchRunDBService> InitializeCosmosClientInstanceAsyncMatchRun(IConfigurationSection configurationSection)
        {
            var databaseName = configurationSection["DatabaseName"];
            var containerName = "PassThroughData";
            var account = configurationSection["Account"];
            var key = configurationSection["Key"];

            var client = new Microsoft.Azure.Cosmos.CosmosClient(account, key);
            var database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
            await database.Database.CreateContainerIfNotExistsAsync(containerName, "/matchId");
            

            var cosmosDBService = new MatchRunDBService(client, databaseName, containerName);

            return cosmosDBService;
        }
        private static async Task<IMortalitySlopeDBService> InitializeCosmosClientInstanceAsyncMortalitySlope(IConfigurationSection configurationSection)
        {
            var databaseName = configurationSection["DatabaseName"];
            var containerName = "MortalitySlopeData";
            var account = configurationSection["Account"];
            var key = configurationSection["Key"];

            var client = new Microsoft.Azure.Cosmos.CosmosClient(account, key);
            var database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
            await database.Database.CreateContainerIfNotExistsAsync(containerName, "/matchId");

            var cosmosDbService = new MortalitySlopeDBService(client, databaseName, containerName);
            return cosmosDbService;
        }
        private static async Task<IGraphParamsDBService> InitializeCosmosClientInstanceAsyncGraphParams(IConfigurationSection configurationSection)
        {
            var databaseName = configurationSection["DatabaseName"];
            var containerName = "GraphParamsData";
            var account = configurationSection["Account"];
            var key = configurationSection["Key"];

            var client = new Microsoft.Azure.Cosmos.CosmosClient(account, key);
            var database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
            await database.Database.CreateContainerIfNotExistsAsync(containerName, "/id");

            var cosmosDbService = new GraphParamsDBService(client, databaseName, containerName);
            return cosmosDbService;
        }
    }
}
