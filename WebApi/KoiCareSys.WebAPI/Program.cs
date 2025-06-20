using KoiCareSys.Data;
using KoiCareSys.Data.Repository;
using KoiCareSys.Data.Repository.Interface;
using KoiCareSys.Service.Mappings;
using KoiCareSys.Service.Service;
using KoiCareSys.Service.Service.Interface;
using KoiCareSys.Service.SignalR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IUnitService, UnitService>();
builder.Services.AddScoped<IMeasurementService, MeasurementService>();
builder.Services.AddScoped<IFeedingScheduleService, FeedingScheduleService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPondService, PondService>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Unit of Work
builder.Services.AddScoped<UnitOfWork>();

// Add Repository
builder.Services.AddScoped<IFeedingScheduleRepository, FeedingScheduleRepository>();
builder.Services.AddScoped<IMeasurementRepository, MeasurementRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPondRepository, PondRepository>();

// Add Service
builder.Services.AddScoped<IKoiService, KoiService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IUnitService, UnitService>();
builder.Services.AddScoped<IMeasurementService, MeasurementService>();
builder.Services.AddScoped<IFeedingScheduleService, FeedingScheduleService>();
builder.Services.AddScoped<IPondService, PondService>();
builder.Services.AddScoped<IKoiRecordSvc, KoiRecordSvc>();
builder.Services.AddScoped<IDevelopmenStageSvc, DevelopmentStageSvc>();

//builder.Services.ConfigAddDbContext();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder
           // .AllowAnyOrigin()
            .WithOrigins("https://localhost:7022")
            .WithOrigins("https://localhost:7249")
                .WithOrigins("https://localhost:7050")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

// AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

// Add SignalR
builder.Services.AddSignalR();
builder.Services.AddScoped<SignalRHub>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<SignalRHub>("/signalRHub");

app.Run();
