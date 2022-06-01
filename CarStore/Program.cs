using CarStore.Database;
using CarStore.Repository.CategoryRepository;
using CarStore.Repository.CustomerRepository;
using CarStore.Repository.OrderItemRepository;
using CarStore.Repository.OrderRepository;
using CarStore.Repository.ProductRepository;
using CarStore.Repository.UserRepository;
using CarStore.Services.CategoryService;
using CarStore.Services.CustomerService;
using CarStore.Services.OrderItemService;
using CarStore.Services.OrderService;
using CarStore.Services.ProductService;
using CarStore.Services.UserService;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("kage",
    builder =>
    {
        builder.AllowAnyOrigin() // kan skrive port i stedet for
               .AllowAnyHeader()
               .AllowAnyMethod(); // kun get eller put mm.
    });
});

// Add services to the container.
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddScoped<IOrderItemService, OrderItemService>();
builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
builder.Services.AddDbContext<AbContext>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CarStore", Version = "v1" });
});

var app = builder.Build();

// x½x½Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("kage");
app.MapControllers();

app.Run();
