using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RestFlow.BL.Factory;
using RestFlow.BL.Observer.Consumers;
using RestFlow.BL.Services;
using RestFlow.DAL.Data;
using RestFlow.DAL.Entities;
using RestFlow.DAL.Repositories;
using RestFlow.Common.Utilities;

var builder = WebApplication.CreateBuilder(args);

var redisConnectionString = builder.Configuration.GetValue<string>("REDIS_CONNECTION_STRING") ?? "localhost:6379";
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = redisConnectionString;
    options.InstanceName = "SampleInstance";
});

builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
                options.JsonSerializerOptions.MaxDepth = 32;
            });


builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("AuthRestFlowDb")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<AuthDbContext>();

builder.Services.AddDbContext<DataDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("RestFlowDb")));


builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IDishRepository, DishRepository>();
builder.Services.AddScoped<IIngredientRepository, IngredientRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IRestaurantRepository, RestaurantRepository>();
builder.Services.AddScoped<ITableRepository, TableRepository>();
builder.Services.AddScoped<IWaiterRepository, WaiterRepository>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IDishService, DishService>();
builder.Services.AddScoped<IIngredientService, IngredientService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IRestaurantService, RestaurantService>();
builder.Services.AddScoped<ITableService, TableService>();
builder.Services.AddScoped<IWaiterService, WaiterService>();

builder.Services.AddScoped<KitchenObserver>();
builder.Services.AddSingleton<IModelFactory, ModelFactory>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddLogging(logging =>
{
    logging.ClearProviders();
    logging.AddProvider(new CustomLoggerProvider());
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder
            .WithOrigins("http://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var ctx1 = scope.ServiceProvider.GetRequiredService<DataDbContext>();
    var ctx2 = scope.ServiceProvider.GetRequiredService<AuthDbContext>();
    //ctx1.Database.EnsureDeleted();
    ctx1.Database.EnsureCreated();
}


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
