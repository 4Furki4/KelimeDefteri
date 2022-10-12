using KelimeDefteri.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

builder.Services.AddDbContext<DefterDB>(
    opt => opt.UseSqlServer
        (
            builder.Configuration["ConnectionStrings:DefterDBConnection"]
        ), ServiceLifetime.Singleton
    );

//builder.Services.AddSingleton<DefterDB>();


var app = builder.Build();

app.UseStaticFiles();

app.MapRazorPages();

app.MapControllerRoute("defter", "{controller=Defter}/{action=anasayfa}");

app.Run();
