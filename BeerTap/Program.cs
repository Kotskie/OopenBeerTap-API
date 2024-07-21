using Microsoft.EntityFrameworkCore;
using BeerTap.Data;
using BeerTap.Data.Repositories.Beverage;
using BeerTap.Data.Repositories.OrderItem;
using BeerTap.Data.Repositories.Tab;
using BeerTap.Services;
using BeerTap.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure DbContext to use SQLite
builder.Services.AddDbContext<BarDbContext>(options =>
    options.UseSqlite("Data Source=beer_tap.db"));

// Register repositories and services
builder.Services.AddScoped<IBeverageRepository, BeverageRepository>();
builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
builder.Services.AddScoped<ITabRepository, TabRepository>();
builder.Services.AddScoped<TabService>();

var app = builder.Build();

// Initialize the database
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<BarDbContext>();
    dbContext.Database.EnsureCreated();

    // Optional: Seed initial data if necessary
    SeedData(dbContext);
}

void SeedData(BarDbContext context)
{
    if (!context.Beverages.Any())
    {
        context.Beverages.AddRange(
            new Beverage { BeverageId = Guid.NewGuid(), Name = "Beer", Price = 45.00m },
            new Beverage { BeverageId = Guid.NewGuid(), Name = "Cider", Price = 52.00m },
            new Beverage { BeverageId = Guid.NewGuid(), Name = "Premix", Price = 59.00m }
        );
        context.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
