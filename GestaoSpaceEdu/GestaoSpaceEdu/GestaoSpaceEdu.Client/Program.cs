using GestaoSpaceEdu.Client.Services;
using GestaoSpaceEdu.Domain.Repositories.Interfaces;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthenticationStateDeserialization();

builder.Services.AddScoped<HttpClient>(options => {
    var httpClient = new HttpClient();
    httpClient.BaseAddress = new Uri("https://localhost:7295");

    return httpClient;
});

builder.Services.AddScoped<IAccountRepository, AccountService>();
builder.Services.AddScoped<ICategoryRepository, CategoryService>();
builder.Services.AddScoped<ICompanyRepository, CompanyService>();
builder.Services.AddScoped<IFinancialTransactionRepository, FinancialTransactionService>();

await builder.Build().RunAsync();
