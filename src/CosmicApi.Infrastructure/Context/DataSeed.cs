using CosmicApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json.Converters;

namespace CosmicApi.Infrastructure.Context
{
    public static class DataSeed
    {
        private static readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings();

        private static readonly string SeedFolderName = "Seeds";
        private static readonly string CurrentDirectory = Directory.GetCurrentDirectory();
        private static readonly string FolderPath = Path.Combine(CurrentDirectory, SeedFolderName);
        private static void ConfigureJsonSettings(JsonSerializerSettings jsonSerializer)
        {
            jsonSerializer.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            jsonSerializer.PreserveReferencesHandling = PreserveReferencesHandling.None;
            jsonSerializer.NullValueHandling = NullValueHandling.Ignore;
            jsonSerializer.Formatting = Formatting.None;
            jsonSerializer.Converters.Add(new StringEnumConverter());
        }
        public static async Task EnsureSeedDataAsync(this ApplicationDbContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(ApplicationDbContext));

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
                // await AddEntitiesIfNotExistsAsync<Picture>(context, "Pictures.json");
               
            await context.SaveChangesAsync();
            
            ///
            context.ChangeTracker.AutoDetectChangesEnabled = true;
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
            ///
        }

        private static async Task AddEntitiesIfNotExistsAsync<TEntity>(DbContext context, string fileName)
            where TEntity : class
        {
            if (!await context.Set<TEntity>().AsNoTracking().AnyAsync())
            {
                var filePath = Path.Combine(FolderPath, fileName);

                if (!File.Exists(filePath))
                    throw new FileNotFoundException($"File '{fileName}': not found at path: '{filePath}'.");

                string res = await File.ReadAllTextAsync(filePath, Encoding.UTF8);
                IEnumerable<TEntity>? entities = JsonConvert.DeserializeObject( res, 
                    typeof(IEnumerable<TEntity>), JsonSettings) 
                    as IEnumerable<TEntity>;
                    
                if (entities != null && entities.Any())
                    context.AddRange(entities);
            }
        }

    }
}
