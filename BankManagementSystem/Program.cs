using BankManagementSystem.Domain.InterfaceRepository;
using BankManagementSystem.Domain.Repository;
using BankManagementSystem.Domain.Context;
using BankManagementSystem.Dto.Interface;
using BankManagementSystem.Servicess.Mapping;
using BankManagementSystem.Mappings;
using BankManagementSystem.Servicess.InterfaceService;
using BankManagementSystem.Servicess.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using System.Text;
using BankManagementSystem.Ado.Repository.InterfaceRepository;
using BankManagementSystem.Ado.Repository;
using BankManagementSystem.Ado.Services.Services.InterfaceService;
using BankManagementSystem.Ado.Services.Services;
using BankManagementSystem.Ado.Services.Mapping;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();

//public void ConfigureServices(IServiceCollection services)
//{
//    services.AddHttpClient();
//    services.AddControllers();
//}
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.Configure<ConnectionSetting>(builder.Configuration.GetSection("ConnectionSetting"));


builder.Services.AddScoped<ICustomersRepository, CustomersRepository>();
builder.Services.AddScoped<ICustomersService, CustomersServices>();



builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<IMoneyTransactionRepository, MoneyTransactionRepository>();
builder.Services.AddScoped<ICustomerService, CustomerServices>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IMoneyTransactionService, MoneyTransactionService>();




builder.Services.AddControllers()
       .AddNewtonsoftJson(options =>
       {
           options.SerializerSettings.Converters.Add(new StringEnumConverter());
       })
       .AddFluentValidation();




builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MulitpleDb.Sample", Version = "v1" });
}).AddSwaggerGenNewtonsoftSupport();



builder.Services.AddAutoMapper(typeof(ServicesMapping));
builder.Services.AddAutoMapper(typeof(ApiMapping));

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHsts();
app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
