using Microsoft.Extensions.Options;
using Syriana_Web.Components.Models;

namespace Syriana_Web.Components.ImagesF
{
    public class CarouselImagesService(HttpClient http,  IWebAssemblyHostEnvironment env)
    {
        public List<CarouselImage> DownloadedCarouselImage { get; private set; } = [];
        private readonly HttpClient _http = http;
        private readonly IWebAssemblyHostEnvironment _env = env;

        public string GetImageUrl(CarouselImage carouselImage)
        {
            if (carouselImage == null)
                return "/images/sample.jpg";

            if (carouselImage.ImageUrl != null)
            {
                string dbImageUrl = carouselImage.ImageUrl.TrimStart('/');
                // ✅ Füge eine Zufallszahl hinzu, um Cash zu vermeiden.
                string unique = $"?v={carouselImage.LastModified}";
                //
                if (_env.IsDevelopment())
                {
                    string baseUri = AppConfig.ApiUri.ToString().TrimEnd('/');
                    string path = AppConfig.WebRequestProductImagePath.Trim('/');

                    string completteUrl = $"{baseUri}/{path}/{dbImageUrl}{unique}";
                    return completteUrl;
                }
                else
                {
                    if (dbImageUrl.StartsWith("CarouselImages/", StringComparison.OrdinalIgnoreCase))
                    {
                        dbImageUrl = dbImageUrl["CarouselImages/".Length..];
                    }
                    string domin = AppConfig.Domin.TrimEnd('/');

                    string completteUrl = $"{domin}/{AppConfig.CarouselImagesproxy}/{dbImageUrl}{unique}";
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
        // async
        public async Task<List<CarouselImage>> GetActive()
        {
            if (DownloadedCarouselImage.Count > 0)
                return DownloadedCarouselImage;

            try
            {
                var response = await _http.GetAsync("api/Carousel/getActive");
                if (!response.IsSuccessStatusCode)
                {
                    return [];
                }
                var carouselImages = await response.Content.ReadFromJsonAsync<List<CarouselImage>>();
                if (carouselImages == null)
                {
                    return [];
                }

                // add to local list
                AddProductToLocal(carouselImages);

                return carouselImages;
            }
            catch
            {
                return [];
            }
        }

        // local
        public void AddProductToLocal(CarouselImage carouselImage)
        {
            if (!DownloadedCarouselImage.Any(p => p.Id == carouselImage.Id))
            {
                DownloadedCarouselImage.Add(carouselImage);
            }
        }
        public void AddProductToLocal(List<CarouselImage> carouselImage)
        {
            if (carouselImage.Count > 0 && DownloadedCarouselImage.Count == 0)
            {
                DownloadedCarouselImage.AddRange(carouselImage);
                return;
            }
            foreach (var product in carouselImage)
            {
                if (!DownloadedCarouselImage.Any(p => p.Id == product.Id))
                {
                    DownloadedCarouselImage.Add(product);
                }
            }
        }
    }
}
