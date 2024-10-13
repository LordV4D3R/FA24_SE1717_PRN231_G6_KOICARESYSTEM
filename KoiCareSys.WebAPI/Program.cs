using KoiCareSys.Data;
using KoiCareSys.Data.Repository;
using KoiCareSys.Data.Repository.Interface;
using KoiCareSys.Service.Service;
using KoiCareSys.Service.Service.Interface;
using KoiCareSys.WebAPI.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Repository
builder.Services.AddScoped<UnitOfWork>();
builder.Services.AddScoped<IFeedingScheduleRepository, FeedingScheduleRepository>();
builder.Services.AddScoped<IKoiRepository, KoiReposiory>();

// Add Service
builder.Services.AddScoped<IKoiService, KoiService>();

//Add Configuration
builder.Services.ConfigAddDbContext();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
