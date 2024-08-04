using static Microsoft.AspNetCore.Http.StatusCodes;
using Microsoft.EntityFrameworkCore;
using NorthwindOrdersAPI.Data;
using NorthwindOrdersAPI.BL;
using NorthwindOrdersAPI.Services.Interfaces;
using NorthwindOrdersAPI.Repositories;
using NorthwindOrdersAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<DocumentService>();
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<ShipperService>();

builder.Services.AddScoped<OrderRepository>();
builder.Services.AddScoped<TempOrderRepository>();
builder.Services.AddScoped<CustomerRepository>();
builder.Services.AddScoped<DocumentRepository>();
builder.Services.AddScoped<EmployeeRepository>();
builder.Services.AddScoped<ProductRepository>();
builder.Services.AddScoped<ShipperRepository>();
builder.Services.AddScoped<OrderDetailsRepository>();

builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => { builder.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader(); });
});

builder.Services.AddHttpsRedirection(options =>
{
    options.RedirectStatusCode = Status307TemporaryRedirect;
    options.HttpsPort = 7022;
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<CentralErrorHandler>();
app.UseStaticFiles();
app.UseRouting();
app.UseCors("AllowAllOrigins");
app.UseAuthorization();
app.MapControllers();

app.Run();

