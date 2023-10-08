using Microsoft.EntityFrameworkCore;
using WorldTools.Application.Queries.Factory;
using WorldTools.Application.Queries.UseCases.BranchUseCases;
using WorldTools.Domain.Factory;
using WorldTools.Domain.Ports;
using WorldTools.Infrastructure;
using WorldTools.Rabbit.SubscribeAdapter;
using WorldTools.SqlAdapter.Adapters;
using AutoMapper.Data;
using WorldTools.SqlAdapter.Common.Mapping;
using WorldTools.Application.UseCases.UserUseCases;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<GetBrachByIdUseCase>();

builder.Services.AddHostedService<SubscribeEvent>();

builder.Services.AddSingleton<IBranchUseCaseQueryFactory, BranchUseCaseQueryFactory>();
builder.Services.AddScoped<IBranchUseCaseQuery, RegisterBranchUseCaseQuery>();

builder.Services.AddScoped<IBranchRepository, BranchRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(config => config.AddDataReaderMapping(), typeof(MappingProfileSql));

builder.Services.AddAutoMapper(config => {
    config.AddProfile<MappingProfileSql>();
});

var app = builder.Build();

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
