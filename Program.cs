using IronDomeAPI.Data;
using IronDomeAPI.MiddleWares.Global;
using IronDomeAPI.MiddlEWares.Attack;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddDbContext<IronDomeAPIDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'IronDomeAPIDbContext' not found.")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.UseMiddleware<GlobalLoginMiddleWare>();

app.UseWhen(
    context =>
        context.Request.Path.StartsWithSegments("/api/attacks"),
    appBuilder =>
    {
        appBuilder.UseMiddleware<AttckLoginMiddleWare>();
    });


app.MapControllers();

app.Run();
