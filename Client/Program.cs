using Client.Components;
using Data;
using Data.Services;
using Microsoft.FluentUI.AspNetCore.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<PresidentService>();

SharedValues.ApiColombiaBaseURL = builder.Configuration["Colombia_API"] ??
                                  throw new ApplicationException("No se encontró la URL del API");

builder.Services.AddScoped(s => new HttpClient
{
    BaseAddress = new Uri(SharedValues.ApiColombiaBaseURL)
});

builder.Services.AddFluentUIComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();