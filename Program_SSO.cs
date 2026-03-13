using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using SSO_Blazor_Completo.Components;

DotNetEnv.Env.Load();
var builder = WebApplication.CreateBuilder(args);


string[]? scopes = builder.Configuration
    .GetSection("GraphApi:Scopes")
    .Get<string[]>();


builder.Services
    .AddMicrosoftIdentityWebAppAuthentication(builder.Configuration, "AzureAd")
    .EnableTokenAcquisitionToCallDownstreamApi(scopes)
    .AddDownstreamApi("GraphApi", builder.Configuration.GetSection("GraphApi"))
    .AddInMemoryTokenCaches();

builder.Services
    .AddControllersWithViews()
    .AddMicrosoftIdentityUI();


builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = options.DefaultPolicy;
});

builder.Services
    .AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddRazorPages();
builder.Services
    .AddServerSideBlazor()
    .AddMicrosoftIdentityConsentHandler();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();


app.MapControllers();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();