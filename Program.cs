using Employee_Management_System.Common;
using Employee_Management_System.CosmosDB;
using Employee_Management_System.Interface;
using Employee_Management_System.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<ICosmoseDBService, CosmosDBService>();
builder.Services.AddScoped<IEmployeeBasicDetailsService, EmployeeBasicDetailsService>();
builder.Services.AddScoped<IEmployeeAdditionalDetailsService, EmployeeAdditionalDetailsService>();

builder.Services.AddAutoMapper(typeof(Mapper));

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
