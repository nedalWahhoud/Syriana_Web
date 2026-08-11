using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using Syriana_Web;
using Syriana_Web.Components.AddressesF;
using Syriana_Web.Components.Cart;
using Syriana_Web.Components.CategoriesF;
using Syriana_Web.Components.CookieF;
using Syriana_Web.Components.CustomersF;
using Syriana_Web.Components.DebtF;
using Syriana_Web.Components.DiscountF;
using Syriana_Web.Components.EmailF;
using Syriana_Web.Components.FavoriteF;
using Syriana_Web.Components.ImagesF;
using Syriana_Web.Components.Login;
using Syriana_Web.Components.OrderF;
using Syriana_Web.Components.ProductGroupF;
using Syriana_Web.Components.ProductsF;
using Syriana_Web.Components.SearchF;
using Syriana_Web.Components.TransactionsCustomersF;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// http api
builder.Services.AddScoped(sp =>
{
    return new HttpClient { BaseAddress = AppConfig.ApiUri };
});


// auth
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
// auth AuthService
builder.Services.AddScoped<AuthService>();
// products
builder.Services.AddScoped<ProductService>();
// addresses
builder.Services.AddScoped<AddressService>();
// cart
builder.Services.AddScoped<CartService>();
// order
builder.Services.AddScoped<OrderService>();
// email
builder.Services.AddScoped<EmailService>();
// Categories
builder.Services.AddScoped<CategoryService>();
// GroupProducts
builder.Services.AddScoped<ProductGroupService>();
// GroupProducts
builder.Services.AddScoped<DiscountService>(); 
// Search
builder.Services.AddScoped<SearchService>();
// ProductImages
builder.Services.AddScoped<ProductImagesService>();
// Carousel Images
builder.Services.AddScoped<CarouselImagesService>();
// cookie service
builder.Services.AddScoped<CookieService>();
// Customer service
builder.Services.AddScoped<CustomersService>();
// TransactionsCustomers service
builder.Services.AddScoped<TransactionsCustomersService>();
// DebtCustomers service
builder.Services.AddScoped<DebtService>();
// favorite service
builder.Services.AddScoped<FavoriteService>();

// sprache
builder.Services.AddLocalization(options =>
{
    options.ResourcesPath = "Resources";
});

var app = builder.Build();



var jsRuntime = app.Services.GetRequiredService<IJSRuntime>();
var result = await jsRuntime.InvokeAsync<string>("blazorCulture.get");

string cultureName = !string.IsNullOrEmpty(result) ? result : "de";

var culture = new CultureInfo(cultureName);
CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;

await builder.Build().RunAsync();
