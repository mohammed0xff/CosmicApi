using Microsoft.Extensions.FileProviders;

namespace CosmicApi.Configurations
{
    public static class StaticFilesConfig
    {
        public static void  UseStaticFiles(this IApplicationBuilder app, IWebHostEnvironment environment)
        {
            var path = Path.Combine(environment.ContentRootPath, "StaticFiles");
            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(path),
                RequestPath = ""
            });
        }
    }
}
