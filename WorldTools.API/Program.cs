using WorldTools.Infrastructure;
using Microsoft.EntityFrameworkCore;
using AutoMapper.Data;
using WorldTools.Application.Gateway;
using WorldTools.Application.UseCase;
using WorldTools.Infrastructure.Repositories;
using WorldTools.MongoAdapter;
using WorldTools.MongoAdapter.Interfaces;
using WorldTools.MongoAdapter.Common.Mapping;
using WorldTools.Domain.Ports;
using WorldTools.SqlAdapter.Adapters;
using WorldTools.MongoAdapter.Adapters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IBranchUseCase, BranchUseCase>();
builder.Services.AddScoped<IBranchRepository, BranchRepository>();

builder.Services.AddScoped<IProductUseCase, ProductUseCase>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddScoped<IUserUseCase, UserUseCase>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IStoredEventRepository, StoredEventRepository>();

builder.Services.AddControllers();


builder.Services.AddAutoMapper(config => config.AddDataReaderMapping(), typeof(MappingProfileMongo));
builder.Services.AddSingleton<IContextMongo>(provider => new ContextMongo(builder.Configuration.GetConnectionString("MongoConnection"), "Events"));

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
