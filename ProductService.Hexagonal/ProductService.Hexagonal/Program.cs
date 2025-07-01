using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductService.Application.Handlers;
using ProductService.Domain.Repositories;
using ProductService.Infrastructure.Data;
using ProductService.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddUserSecrets<Program>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddMediatR(typeof(AddProductCommandHandler).Assembly);
builder.Services.AddMediatR(typeof(DeleteProductCommandHandler).Assembly);
builder.Services.AddMediatR(typeof(UpdateProductCommandHandler).Assembly);
builder.Services.AddMediatR(typeof(GetAllProductsQueryHandler).Assembly);
builder.Services.AddMediatR(typeof(GetProductByIdQueryHandler).Assembly);
builder.Services.AddMediatR(typeof(GetProductDetailsQueryHandler).Assembly);


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// Handle errors
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();
app.Run();


public partial class Program { }