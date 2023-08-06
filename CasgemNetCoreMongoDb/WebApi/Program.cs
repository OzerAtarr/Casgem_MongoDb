using DataAccess.Abstract;
using DataAccess.Concrete;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Runtime;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<DataBaseSettings>(builder.Configuration.GetSection("DataBaseSettings"));
builder.Services.AddSingleton<IDataBaseSettings>(x => x.GetRequiredService<IOptions<DataBaseSettings>>().Value);


builder.Services.AddSingleton<IMongoClient>(m => new MongoClient(builder.Configuration.GetValue<string>("DataBaseSettings:ConnectionString")));


builder.Services.AddScoped<IEstateService, EstateService>();
builder.Services.AddScoped<IUserService, UserService>();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
