using Microsoft.EntityFrameworkCore;
using PalamigStore.DataAccess.Data;
using PalamigStore.DataAccess.Repository;
using PalamigStore.DataAccess.Repository.IRepository;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("PalamigStoreConnection")));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
var app = builder.Build();

if (!app.Environment.IsDevelopment()) 
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    );


app.Run();
