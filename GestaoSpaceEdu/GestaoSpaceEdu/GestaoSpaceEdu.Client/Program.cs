using Blazored.LocalStorage;
using GestaoSpaceEdu.Client;
using GestaoSpaceEdu.Client.Libraries.Notifications;
using GestaoSpaceEdu.Client.Services;
using GestaoSpaceEdu.Domain.Repositories.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

/*
builder.Services.AddScoped<IConfiguration>(options =>
{
    return builder.Configuration;
}); 
 */

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
//builder.Services.AddAuthenticationStateDeserialization();
builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();

builder.Services.AddScoped<HttpClient>(options => {
    var httpClient = new HttpClient();
    httpClient.BaseAddress = new Uri("https://localhost:7295");

    return httpClient;
});
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped<CompanyOnSelectedNotification>();

builder.Services.AddScoped<IAccountRepository, AccountService>();
builder.Services.AddScoped<ICategoryRepository, CategoryService>();
builder.Services.AddScoped<ICompanyRepository, CompanyService>();
builder.Services.AddScoped<IFinancialTransactionRepository, FinancialTransactionService>();

await builder.Build().RunAsync();
