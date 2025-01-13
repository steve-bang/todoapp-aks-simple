using ToDoApi.Services;
using TodoApi.Context;
using Microsoft.EntityFrameworkCore;
using TodoApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<ITodoService, TodoService>();

builder.Services.AddControllers();


builder.Services.AddDbContext<TodoContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddMigration<TodoContext>();

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

// builder.WebHost.ConfigureKestrel(options =>
// {
//     options.ListenAnyIP(80); // Listen on port 80 on all interfaces
// });

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Configure the HTTP request pipeline.
app.UseCors("AllowAllOrigins");  // Enable CORS

app.UseHttpsRedirection();

app.MapGet("", () => "Helloworld");


app.MapControllers();

app.Run();

