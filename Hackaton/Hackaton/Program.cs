using Hackaton.Data;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using Hackaton.Services;
using Hackaton.Models.User;
using Hackaton.Validation;
using Hackaton.Validation.User;


var server = Environment.GetEnvironmentVariable("SERVER");
var port = Environment.GetEnvironmentVariable("PORT");
var user = Environment.GetEnvironmentVariable("USER");
var password = Environment.GetEnvironmentVariable("PASSWORD");
var database = Environment.GetEnvironmentVariable("DATABASE");

var conString = $"Server={server},{port};user={user};password={password};database={database}; CharSet=utf8;Persist Security Info=True";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddTransient<UserSignUpValidator>();
builder.Services.AddTransient<UserLoginValidator>();

builder.Services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
{
    try
    {
        options.UseMySql(conString, ServerVersion.AutoDetect(conString));
    }
    catch
    {
        Console.WriteLine("Can`t connect to db!");
        Console.WriteLine("Connnection string is not valid: \n" + conString);
        throw new NotImplementedException("Can`t connect to db!");
    }
});
Console.WriteLine("Connected to db successfully!");

builder.Services.AddRazorPages();

var app = builder.Build();

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
