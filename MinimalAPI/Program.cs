using Microsoft.EntityFrameworkCore;
using MinimalAPI.Data;

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
app.MapGet("/", () => "Hello World!");

/// Defining the UI from Swagger
app.UseSwaggerUI();


app.Run();
