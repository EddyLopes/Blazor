using Blazored.LocalStorage;
using Coravel;
using GestaoSpaceEdu.Client.Libraries.Notifications;
using GestaoSpaceEdu.Components;
using GestaoSpaceEdu.Components.Account;
using GestaoSpaceEdu.Data;
using GestaoSpaceEdu.Data.Repositories.Implementations;
using GestaoSpaceEdu.Domain.Entities;
using GestaoSpaceEdu.Domain.Enums;
using GestaoSpaceEdu.Domain.Repositories.Interfaces;
using GestaoSpaceEdu.Libraries.Mail;
using GestaoSpaceEdu.Libraries.Queues;
using GestaoSpaceEdu.Libraries.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents()
    .AddAuthenticationStateSerialization();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();


#region Config of Database & Authentication
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySQL(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddSignInManager()
                .AddDefaultTokenProviders();
#endregion

#region Dependency Injection
builder.Services.AddSingleton<SmtpClient>(options => 
{ 
    var smtp = new SmtpClient();
    smtp.Host = builder.Configuration.GetValue<string>("EmailSender:Server")!;
    smtp.Port = builder.Configuration.GetValue<int>("EmailSender:Port")!;
    smtp.EnableSsl = builder.Configuration.GetValue<bool>("EmailSender:SSL")!;
    smtp.Credentials = new NetworkCredential(builder.Configuration.GetValue<string>("EmailSender:User"),
                                             builder.Configuration.GetValue<string>("EmailSender:Password"));
    return smtp;
});
builder.Services.AddSingleton<IEmailSender<ApplicationUser>, EmailSender>();
builder.Services.AddSingleton<ICepService, CepService>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddQueue();

builder.Services.AddScoped<CompanyOnSelectedNotification>();
builder.Services.AddScoped<FinancialTransactionRepeatInvocable>();

builder.Services.AddScoped<IAccountRepository,AccountRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
builder.Services.AddScoped<IFinancialTransactionRepository, FinancialTransactionRepository>();
#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(GestaoSpaceEdu.Client._Imports).Assembly);

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

#region Minimal APIs
int pageSize = builder.Configuration.GetValue<int>("Pagination:PageSize");
                
app.MapGet("/api/categories", async (ICategoryRepository repository, 
                                     [FromQuery] int companyId, 
                                     [FromQuery] int pageIndex) => 
{
    var data = await repository.GetAllAsync(companyId, pageIndex, pageSize);
    return Results.Ok(data);
});

app.MapGet("/api/companies", async (ICompanyRepository repository,
                                    [FromQuery] string applicationUserId,
                                    [FromQuery] int pageIndex,
                                    [FromQuery] string searchWord) =>
{
    var data = await repository.GetAllAsync(applicationUserId, pageIndex, pageSize, searchWord);
    return Results.Ok(data);
});

app.MapGet("/api/accounts", async (IAccountRepository repository,
                                   [FromQuery] int companyId,
                                   [FromQuery] int pageIndex,
                                   [FromQuery] string searchWord) =>
{
    var data = await repository.GetAllAsync(companyId, pageIndex, pageSize, searchWord);
    return Results.Ok(data);
});

app.MapGet("/api/financialtransactions", async (IFinancialTransactionRepository repository,
                                                [FromQuery] int companyId,
                                                [FromQuery] TypeFinancialTransaction type,
                                                [FromQuery] int pageIndex,
                                                [FromQuery] string searchWord) =>
{
    
    var data = await repository.GetAllAsync(companyId, type, pageIndex, pageSize, searchWord);
    return Results.Ok(data);
});
#endregion

app.Run();
