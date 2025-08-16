using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using PaymentContracts;
using Services;
var builder = WebApplication.CreateBuilder(args);

// Add Payments to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IReview, ReviewServices>();
builder.Services.AddScoped<IService, ServiceServices>();
builder.Services.AddScoped<IPay, PayService>();
builder.Services.AddHttpClient<PaymobService>();
builder.Services.Configure<PaymobSettings>(builder.Configuration.GetSection("PaymobSettings"));
builder.Services.AddDbContext<ReviewDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), b=> b.MigrationsAssembly("Entities")));
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