using ODSApiCore.Auth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ODSApi.BusinessServices;
using ODSDatabase.DBServices;
using ODSApi.Extensions;
using ODSApi.Middleware;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Unos.Foundation;
using System;

namespace ODSApi
{
    public class Startup
    {
        private IConfiguration Configuration; 
        
        public Startup(IConfiguration configuration)
        {
            Configuration = Requires.NotNull(configuration,nameof(configuration));
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddControllers();           
           
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ODSApi", Version = "v1" });
            });

            //Add unos auth services
            services.AddApigeeJwtBearerAuthentication(Configuration).AddAuthorization(options =>
            {

                options.AddPolicy(PredictiveAnalyticsAuthorizationPolicy.Name, PredictiveAnalyticsAuthorizationPolicy.Policy);

            });
                      
           
                services.AddSingleton<ILogDBService>(InitializeCosmosClientInstanceAsyncLogs(Configuration.GetSection("CosmosDb")).GetAwaiter().GetResult());
                services.AddSingleton<ITimeToNextOfferDBService>(InitializeCosmosClientInstanceAsyncTimeToNextOffer(Configuration.GetSection("CosmosDb")).GetAwaiter().GetResult());
                services.AddSingleton<IMatchRunDBService>(InitializeCosmosClientInstanceAsyncMatchRun(Configuration.GetSection("CosmosDb")).GetAwaiter().GetResult());
                services.AddSingleton<IMortalitySlopeDBService>(InitializeCosmosClientInstanceAsyncMortalitySlope(Configuration.GetSection("CosmosDb")).GetAwaiter().GetResult());
                services.AddSingleton<IGraphParamsDBService>(InitializeCosmosClientInstanceAsyncGraphParams(Configuration.GetSection("CosmosDb")).GetAwaiter().GetResult());
                services.AddScoped<IMatchRunBusinessService, MatchRunBusinessService>();
                services.AddApplicationInsightsTelemetry();
                services.ConfigureCors();     


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
            if (env.IsStaging())
            {

            }
            else
            {
                app.UseHsts();
            }
            app.UseStaticFiles();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ODSApi v1"));
            app.UseCors("CorsPolicy");
           

            //Use this Middleware prior to app.UseEndpoints....

            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseMiddleware(typeof(ExceptionHandlingMiddleware));
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


        }
        /*********************************
         * These are the database services
         * that are injected into the ioc
         * container.  There are 5 Services
         * Based on the 5 collections in ODS
         * The partition key in Cosmos is /matchId
         * except for the graph params table.  The
         * partition key in ODS must be a field in
         * database.
         * ******************************/
        
        private static async Task<LogDBService> InitializeCosmosClientInstanceAsyncLogs(IConfigurationSection configurationSection) //used to be IConfigurationSection
        {
            
          
            var databaseName = configurationSection["DatabaseName"];
            var containerName = configurationSection["logContainerName"];
            var account = "";
            var key = "";
            if(Environment.GetEnvironmentVariable("ENVIRONMENT") == "DEV")
            {
                 account = Environment.GetEnvironmentVariable("cosmos_uri");
                 key = Environment.GetEnvironmentVariable("cosmos_key");
            }          
            else
            {
                account = configurationSection["Account"];
                key = configurationSection["Key"];

            }
            //var account = Environment.GetEnvironmentVariable("cosmos_uri");
            //var key = configurationSection["Key"];  
            //var key = Environment.GetEnvironmentVariable("cosmos_key");
            var client = new Microsoft.Azure.Cosmos.CosmosClient(account, key);
            var database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
            await database.Database.CreateContainerIfNotExistsAsync(containerName, "/matchId");
            var cosmosDbService = new LogDBService(client, databaseName, containerName);            
            return cosmosDbService;
        }
        private static async Task<TimeToNextOfferDBService> InitializeCosmosClientInstanceAsyncTimeToNextOffer(IConfigurationSection configurationSection)
        {
            var databaseName = configurationSection["DatabaseName"];
            var containerName = configurationSection["ttnoContainerName"];
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
            var containerName = configurationSection["passThroughContainerName"];
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
            var containerName = configurationSection["mortalitySlopeContainerName"];
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
            var containerName = configurationSection["graphParamsContainerName"];
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
