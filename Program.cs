using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using OrderManagement.Handlers.CommandHandlers;
using OrderManagement.Handlers.QueryHandlers;
using OrderManagement.Handlers;
using OrderManagementSystem.Handlers;

var builder = WebApplication.CreateBuilder(args);

// Fetch MongoDB connection string from the configuration
var mongoConnectionString = builder.Configuration["MongoDB:ConnectionString"];
if (string.IsNullOrEmpty(mongoConnectionString))
{
    throw new ArgumentNullException("MongoDB connection string is not configured properly.");
}

// Add logging configuration
builder.Logging.ClearProviders(); // Remove default providers
builder.Logging.AddConsole();     // Add console logging
builder.Logging.AddDebug();       // Add debug logging if necessary

// Register MongoDB client
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var client = new MongoClient(mongoConnectionString);
    return client;
});

// Register MongoDB database
builder.Services.AddSingleton<IMongoDatabase>(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    var databaseName = builder.Configuration["MongoDB:DatabaseName"];
    return client.GetDatabase(databaseName);
});

// Register services
builder.Services.AddSingleton<OrderCommandService>();
builder.Services.AddSingleton<OrderQueryService>();

// Register event handlers
builder.Services.AddTransient<OrderCreatedEventHandler>();  // Register the OrderCreated event handler
builder.Services.AddTransient<OrderUpdatedEventHandler>();  // Register the OrderUpdated event handler
builder.Services.AddTransient<OrderDeletedEventHandler>();  // Register the OrderDeleted event handler


// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Order Management API", Version = "v1" });
});

// Add controllers
builder.Services.AddControllers();

var app = builder.Build();

// Ensure logging to console is enabled
app.UseDeveloperExceptionPage(); // Optional, for development purposes
app.MapControllers();

// Enable middleware to serve generated Swagger as a JSON endpoint
app.UseSwagger();

// Enable middleware to serve Swagger UI
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Order Management API v1");
    c.RoutePrefix = "swagger"; // Sets the Swagger UI page to be accessible at /swagger
});

app.UseHttpsRedirection();

// Map controllers
app.Run();
