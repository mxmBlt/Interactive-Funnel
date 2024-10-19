using api.Data;
using api.Repository;
using Microsoft.EntityFrameworkCore;

// Creates a builder object which prepares the application to run
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add optional extra features to the application. 
builder.Services.AddEndpointsApiExplorer(); // helps explore API endpoints
builder.Services.AddSwaggerGen(); // adds swagger 

// Specify to the application which databse it's going to use. 
builder.Services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options => // this parameter is a lambda expression or anonymous function
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

});

builder.Services.AddScoped<IStockRepository, StockRepository>();

// Build the application and get it ready to run
var app = builder.Build();

// Check if the application is running, if so set Swagger for API documentation
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Make sure http requests are redirected to Https for security
app.UseHttpsRedirection();

app.MapControllers(); // in order for swagger to work

// Start the application and keep it running
app.Run();
