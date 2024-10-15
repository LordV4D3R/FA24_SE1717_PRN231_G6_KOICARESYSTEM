using Autofac.Core;
using KoiCareSys.Data;
using KoiCareSys.Data.Repository;
using KoiCareSys.Data.Repository.Interface;
using KoiCareSys.Service.Mappings;
using KoiCareSys.Service.Service;
using KoiCareSys.Service.Service.Interface;
using KoiCareSys.WebAPI.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IUnitService, UnitService>();
builder.Services.AddScoped<IMeasurementService, MeasurementService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<UnitOfWork>();
builder.Services.AddScoped<IFeedingScheduleRepository, FeedingScheduleRepository>();
builder.Services.AddScoped<IMeasurementRepository, MeasurementRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IKoiRepository, KoiReposiory>();

// Add Service
builder.Services.AddScoped<IKoiService, KoiService>();

//Add Configuration
//builder.Services.ConfigAddDbContext();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policy =>
        {
            policy.WithOrigins("https://localhost:7250")
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
});

// AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowSpecificOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
