using Auth;
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

            /******************
             * Inject db services
             * as singleton 
             * ****************/

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

              /*app.UseAuthentication();
                app.UseRouting();
                app.UseAuthorization();
                app.UseMiddleware(typeof(ExceptionHandlingMiddleware));*/
               /* app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });*/
                //Use this Middleware prior to app.UseEndpoints....
                /* app.UseMiddleware(typeof(ExceptionHandlingMiddleware));
                 app.UseRouting();
                 app.UseAuthorization();
                 app.UseEndpoints(endpoints =>
                 {
                     endpoints.MapControllers();
                 });*/
            }
            else
            {
                app.UseHsts();
            }
            app.UseStaticFiles();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ODSApi v1"));
            app.UseCors("CorsPolicy");
            /*app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.All
            });
            app.UseHttpsRedirection();*/

            /*      app

          .UseAuthentication()

          .UseRouting()

          .UseAuthorization()

          .UseMiddleware(typeof(ExceptionHandlingMiddleware))

          .UseEndpoints(endpoints => endpoints.MapControllers());*/

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
