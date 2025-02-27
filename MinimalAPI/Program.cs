var builder = WebApplication.CreateBuilder(args);

/// Defining the Services needed
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
