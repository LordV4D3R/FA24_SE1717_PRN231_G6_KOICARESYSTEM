using KoiCareSys.Data;
using KoiCareSys.MVCWebApp.ApiService;
using KoiCareSys.MVCWebApp.ApiService.Interface;
using KoiCareSys.Service.Mappings;
using KoiCareSys.Service.Service;
using KoiCareSys.Service.Service.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Register your DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));
builder.Services.AddHttpClient<ApiService>("MyAPI", client =>
{
    client.BaseAddress = new Uri("https://localhost:7050");
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder
     
            .WithOrigins("https://localhost:7022")
            .WithOrigins("https://localhost:7050")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

builder.Services.AddScoped<IApiService, ApiService>();
builder.Services.AddScoped<IPondService, PondService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IKoiRecordSvc, KoiRecordSvc>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IDevelopmenStageSvc, DevelopmentStageSvc>();
builder.Services.AddScoped<IKoiService, KoiService>();
builder.Services.AddScoped<UnitOfWork>();
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
app.UseCors("AllowAllOrigins");
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
