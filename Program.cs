using AlphaWorkout.Data;
using AlphaWorkout.Models;
using AlphaWorkout.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// determine which connection string to use
string connectionString;
if (builder.Environment.IsDevelopment())
{
    connectionString = builder.Configuration.GetConnectionString("LocalConnection") ?? throw new InvalidOperationException("Connection string 'LocalConnection' not found.");
}
else
{
    connectionString = builder.Configuration.GetConnectionString("Alphaworkoutconnection") ?? throw new InvalidOperationException("Connection string 'Alphaworkoutconnection' not found.");
}

// add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<ChatGPTService>(new ChatGPTService("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")); // for chatgpt

var app = builder.Build();

// seed the database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        ExerciseSeed.Seed(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the DB.");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // errors in development
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
