using Microsoft.EntityFrameworkCore;
using MinimalAPI.Data;
using MinimalAPI.Models;

var builder = WebApplication.CreateBuilder(args);

/// Defining the Services needed
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/// Configuring the database
builder.Services.AddDbContext<AppDbContext>(options => {
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

/// Configuring the App
var app = builder.Build();

/// Defining exception page to check informations
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

/// Start the use of Swagger
app.UseSwagger();


/// The Request configurations
var items = app.MapGroup("api/items/");

items.MapGet("products/{id}", async (int id, AppDbContext db) =>
{
    return await db.products.FindAsync(id) is 
    Product product ? Results.Ok(product) : Results.NotFound();
});

items.MapPost("products", async(Product product, AppDbContext db) => 
{ 
    db.products.Add(product);
    await db.SaveChangesAsync();
    return Results.Created($"/products/{product.Id}",product);
});

items.MapPut("products/{id}", async (int id, Product inputProduct, AppDbContext db) =>
{
    var product = await db.products.FindAsync(id);
    if (product is null) return Results.NotFound();

    product.Name = inputProduct.Name;
    product.Price = inputProduct.Price;

    await db.SaveChangesAsync();
    return Results.NoContent();
});

items.MapDelete("products/{id}", async (int id, AppDbContext db) =>
{
    if (await db.products.FindAsync(id) is Product product)
    {
        db.products.Remove(product);
        await db.SaveChangesAsync();
        return Results.Ok(product);
    }
    return Results.NotFound();
});

/// Defining the UI from Swagger
app.UseSwaggerUI();


app.Run();
