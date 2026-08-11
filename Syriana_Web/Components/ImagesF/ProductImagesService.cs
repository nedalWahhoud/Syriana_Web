using Microsoft.Extensions.Options;
using Syriana_Web.Components.Models;

namespace Syriana_Web.Components.ImagesF
{
    public class ProductImagesService(IOptions<AppConfig> appConfig, IWebAssemblyHostEnvironment env)
    {
        private readonly IOptions<AppConfig> _appConfig = appConfig;
        private readonly IWebAssemblyHostEnvironment _env = env;

        public string GetProductImageUrl(ProductImages productImages)
        {
            string dbImageUrl = productImages?.ImageUrl!;
            if (dbImageUrl != null)
            {
                // ✅ Füge eine Zufallszahl hinzu, um Cash zu vermeiden.
                string unique = $"?v={productImages?.LastModified}";

                dbImageUrl = dbImageUrl.TrimStart('/');
                if (_env.IsDevelopment())
                {
                    string baseUri = AppConfig.ApiUri.ToString().TrimEnd('/');
                    string path = AppConfig.WebRequestProductImagePath.Trim('/');

                    string completteUrl = $"{baseUri}/{path}/{dbImageUrl}{unique}";
                    return completteUrl;
                }
                else
                {

                    if (dbImageUrl.StartsWith("ProductsImages/", StringComparison.OrdinalIgnoreCase))
                    {
                        dbImageUrl = dbImageUrl["ProductsImages/".Length..];
                    }
                    string domin = AppConfig.Domin.TrimEnd('/');

                    string completteUrl = $"{domin}/{AppConfig.ProductImagesproxy}/{dbImageUrl}{unique}";
                    return completteUrl;
                }
            }
            else
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "DPImage.png");
                var relativePath = path.Split("wwwroot")[1].Replace("\\", "/");

                return relativePath;
            }
        }
    }
}
