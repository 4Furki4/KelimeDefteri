using FluentValidation.AspNetCore;
using KelimeDefteri.Controllers.Businesses;
using KelimeDefteri.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddRazorPages();

builder.Services.AddDbContext<DefterDB>(
    opt => opt.UseSqlServer
        (
            builder.Configuration["ConnectionStrings:DefterDBConnection"]
        ), 
        ServiceLifetime.Singleton
    );
builder.Services.AddSingleton<Business>();
//builder.Services.AddSingleton<DefterDB>();


var app = builder.Build();

app.UseStaticFiles();

app.MapRazorPages();

//Pagination route.



app.MapControllerRoute("pagination", "AllRecord/Page{recordPage}", new { Controller = "Defter", Action = "AllRecord" });

app.MapControllerRoute("Update", "Word/Update/{id?}", new { Controller = "Defter", Action = "Update" });

app.MapControllerRoute("default", "{controller=Defter}/{action=Homepage}");



app.Run();
