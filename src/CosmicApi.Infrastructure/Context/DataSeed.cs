using CosmicApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using JsonNet.ContractResolvers;
using System.Text;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace CosmicApi.Infrastructure.Context
{
    public static class DataSeed
    {
        private static readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings();

        private static readonly string SeedFolderName = "Seeds";
        private static readonly string CurrentDirectory = Directory.GetCurrentDirectory();
        private static readonly string FolderPath = Path.Combine(CurrentDirectory, SeedFolderName);

        public static async Task EnsureSeedDataAsync(this ApplicationDbContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(ApplicationDbContext));

            ConfigureJsonSettings(JsonSettings);
            
            ///
            context.ChangeTracker.AutoDetectChangesEnabled = false;
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ///

            if (!context.Galaxies.Any())
                await AddEntitiesIfNotExistsAsync<Galaxy>(context, "Galaxies.json");
            if (!context.Stars.Any())
                await AddEntitiesIfNotExistsAsync<Star>(context, "Stars.json");
            if (!context.Planets.Any())
                await AddEntitiesIfNotExistsAsync<Planet>(context, "Planets.json");
            if (!context.Users.Any())
                await AddEntitiesIfNotExistsAsync<User>(context, "Users.json");
            if (!context.Pictures.Any())
                await AddEntitiesIfNotExistsAsync<Picture>(context, "Pictures.json");
               
            await context.SaveChangesAsync();
            
            ///
            context.ChangeTracker.AutoDetectChangesEnabled = true;
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
            ///
        }

        private static async Task AddEntitiesIfNotExistsAsync<TEntity>(DbContext context, string fileName)
            where TEntity : class
        {
            var filePath = Path.Combine(FolderPath, fileName);

            if (!File.Exists(filePath))
                throw new FileNotFoundException($"File '{fileName}': not found at path: '{filePath}'.");

            string res = await File.ReadAllTextAsync(filePath, Encoding.UTF8);
            IEnumerable<TEntity>? entities = JsonConvert.DeserializeObject(res,
                typeof(IEnumerable<TEntity>), JsonSettings)
                as IEnumerable<TEntity>;

            if (entities != null && entities.Any())
                context.AddRange(entities);
        }

        private static void ConfigureJsonSettings(JsonSerializerSettings jsonSerializer)
        {
            jsonSerializer.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            jsonSerializer.PreserveReferencesHandling = PreserveReferencesHandling.None;
            jsonSerializer.NullValueHandling = NullValueHandling.Ignore;
            jsonSerializer.Formatting = Formatting.None;

            var contractResolver = new PrivateSetterContractResolver();
            var namingStrategy = new CamelCaseNamingStrategy();
            contractResolver.NamingStrategy = namingStrategy;
            jsonSerializer.ContractResolver = contractResolver;

            jsonSerializer.Converters.Add(new StringEnumConverter(namingStrategy));
        }
    }
}
