using SS.BusinessLogic;
using SS.DataAccess;
using SS.Parser;
using SS.Parser.Base;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddControllersWithViews()
    .AddNewtonsoftJson();

builder.Services
    .RegisterDataAccessServices(builder.Configuration)
    .RegisterParserServices()
    .RegisterBusinessServices();

var app = builder.Build();

#region UPDATE DB

// var dbUpdater = builder.Services.BuildServiceProvider().GetService<IDbUpdater>();
//
// if (dbUpdater is not null)
// {
//     await dbUpdater.UpdateAsync();
// }
// else
// {
//     throw new Exception("Builder not registered.");
// }

#endregion UPDATE DB

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();