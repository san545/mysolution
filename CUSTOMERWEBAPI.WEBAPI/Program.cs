using CUSTOMERWEBAPI.DataAccess.Data;
using CUSTOMERWEBAPI.DataAccess.Interfaces;
using CUSTOMERWEBAPI.DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<sqlDBConnectionFactory>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// ✅ CORS for Blazor WebAssembly
builder.Services.AddCors(options =>
{
    options.AddPolicy("BlazorDev", policy =>
    {
        policy.WithOrigins("https://localhost:7279")  // Blazor UI URL
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ✅ Use CORS before Authorization
app.UseCors("BlazorDev");

app.UseAuthorization();

app.MapControllers();

app.Run();
