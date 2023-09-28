using WorldTools.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using WorldTools.Application.Gateway;
using WorldTools.Application.UseCase;
using WorldTools.Application.Gateway.Repository;
using WorldTools.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IBranchUseCase, BranchUseCase>();
builder.Services.AddScoped<IBranchRepository, BranchRepository>();

builder.Services.AddScoped<IBranchUseCase, BranchUseCase>();
builder.Services.AddScoped<IBranchRepository, BranchRepository>();

builder.Services.AddScoped<IBranchUseCase, BranchUseCase>();
builder.Services.AddScoped<IBranchRepository, BranchRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Connection"))
);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<Context>();
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
