using ANDtrSafetyPortal.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

Console.WriteLine("ACTUAL DATABASE PATH: " +
    Path.GetFullPath("ANDtrSafety.db"));

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider
        .GetRequiredService<ANDtrSafetyPortal.Data.ApplicationDbContext>();

    if (!context.SafetyNotices.Any())
    {
        context.SafetyNotices.Add(
            new ANDtrSafetyPortal.Models.SafetyNotice
            {
                RecallNumber = "TEST-001",
                ProductDescription = "Digital Blood Pressure Monitor",
                RecallClass = "Class II",
                FirmName = "Demo Medical Ltd",
                RecallReason = "Device may display incorrect blood pressure readings."
            });

        context.SaveChanges();
    }
}

app.Run();
