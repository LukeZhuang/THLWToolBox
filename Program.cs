using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Numpy;
using Python.Runtime;
using THLWToolBox.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<THLWToolBoxContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING") ?? throw new InvalidOperationException("Connection string 'AZURE_SQL_CONNECTIONSTRING' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

np.arange(1);
PythonEngine.BeginAllowThreads();

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
