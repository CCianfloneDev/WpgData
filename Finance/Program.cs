using Finance.Components;
using Finance.Components.Services;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMudServices();
builder.Services.AddSingleton<WardExpenseService>();
builder.Services.AddSingleton<BidOpportunityService>();
builder.Services.AddHttpClient();

var app = builder.Build();

// Preload data
using (var scope = app.Services.CreateScope())
{
    var wardExpenseService = scope.ServiceProvider.GetRequiredService<WardExpenseService>();
    var bidOpportunityService = scope.ServiceProvider.GetRequiredService<BidOpportunityService>();
    await wardExpenseService.GetExpensesAsync();
    await bidOpportunityService.GetBidOpportunitiesAsync();
}

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
