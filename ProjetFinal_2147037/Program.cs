using Microsoft.EntityFrameworkCore;
using ProjetFinal_2147037.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<ProjetFinal_2147037Context>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("ProjetFinal_2147037")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name:"default",
        pattern: "{controller=EmissionTelevisions}/{action=IndexAvecViewSQL}"
        );
});

app.MapRazorPages();

app.Run();
