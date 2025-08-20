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
builder.Services.AddScoped<ILogin, LoginService>();

builder.Services.AddHttpClient<PaymobService>();
builder.Services.Configure<PaymobSettings>(builder.Configuration.GetSection("PaymobSettings"));
builder.Services.AddDbContext<ReviewDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), b=> b.MigrationsAssembly("Entities")));
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session timeout
    options.Cookie.HttpOnly = true; // More secure
    options.Cookie.IsEssential = true; // Required for GDPR compliance
});

builder.Services.AddHttpContextAccessor();
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();