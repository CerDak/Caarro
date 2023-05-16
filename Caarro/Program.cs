using Caarro.Data;
using Caarro.Services;

using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;

using MudBlazor.Services;

using Prometheus;

using Quartz;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"));
builder.Services.AddControllersWithViews()
    .AddMicrosoftIdentityUI();
builder.Services.AddAuthorization(options =>
{
    // By default, all incoming requests will be authorized according to the default policy
    options.FallbackPolicy = options.DefaultPolicy;
});

builder.Services.AddHealthChecks()
    .AddSqlite(builder.Configuration.GetConnectionString("Sqlite")!);

builder.Services.AddMetricServer(metrics =>
{
    metrics.Port = 9091;
});

builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory();
});
builder.Services.AddQuartzServer(q =>
{
    q.WaitForJobsToComplete = true;
});

builder.Services.AddDbContext<CaarroDbContext>(db =>
{
    db.UseSqlite(builder.Configuration.GetConnectionString("Sqlite"));
    db.UseTriggers(trigger =>
    {
        trigger.AddTrigger<SendDistanceNotification>();
    });
});

builder.Services.AddMudServices();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor()
    .AddMicrosoftIdentityConsentHandler();

builder.Services.AddScoped<RefuelingService>();
builder.Services.AddScoped<ServicesService>();
builder.Services.AddScoped<VehicleService>();
builder.Services.AddScoped<ReminderService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<CaarroDbContext>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapHealthChecks("/d47ea59a-d4ce-493a-8d67-e429d7d127ad/healthz", new HealthCheckOptions
{
    AllowCachingResponses = false,
}).AllowAnonymous();

app.UseHttpMetrics();
app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
