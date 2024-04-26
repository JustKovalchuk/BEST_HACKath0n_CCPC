using Hackaton.Models.Validation;
using Hackaton.Data;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using Hackaton.Services;
using Hackaton.Models.User;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
{
    var conString = builder.Configuration.GetConnectionString("Default");
    options.UseMySql(conString, ServerVersion.AutoDetect(conString));
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetService<IApplicationDbContext>();

    if (!dbContext.Database.CanConnect())
    {
        throw new NotImplementedException("Can`t connect to db!");
    }
    else
    {
        Console.WriteLine("Connected to db successfully!");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
