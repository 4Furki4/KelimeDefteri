using FluentValidation.AspNetCore;
using KelimeDefteri.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddFluentValidation();

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

//Pagination route.
app.MapControllerRoute("pagination", "AllRecord/Page{recordPage}", new { Controller = "Defter", Action = "AllRecord" }); 

app.MapControllerRoute("default", "{controller=Defter}/{action=Homepage}");



app.Run();
