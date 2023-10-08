using Microsoft.EntityFrameworkCore;
using WorldTools.Application.Queries.Factory;
using WorldTools.Application.Queries.UseCases.BranchUseCases;
using WorldTools.Domain.Factory;
using WorldTools.Infrastructure;
using WorldTools.Rabbit.SubscribeAdapter;
using WorldTools.SqlAdapter.Adapters;
using AutoMapper.Data;
using WorldTools.SqlAdapter.Common.Mapping;
using WorldTools.Application.UseCases.UserUseCases;
using WorldTools.Application.Queries.UseCases.ProductUseCases;
using WorldTools.Domain.Ports.BranchPorts;
using WorldTools.Domain.Ports.ProductPorts;
using WorldTools.Application.UseCases.ProductUseCases;
using WorldTools.Application.Queries.UseCases.UserUserCases;
using WorldTools.Domain.Ports.UserPorts;
using WorldTools.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<GetBrachByIdUseCase>();
builder.Services.AddTransient<GetAllBranchUseCase>();

builder.Services.AddTransient<GetProductByIdUseCaseQuery>();
builder.Services.AddTransient<GetAllProductsUseCaseQuery>();

builder.Services.AddTransient<GetUserByIdUseCase>();
builder.Services.AddTransient<GetAllUsersUseCase>();

builder.Services.AddHostedService<SubscribeEvent>();

builder.Services.AddSingleton<IBranchUseCaseQueryFactory, BranchUseCaseQueryFactory>();
builder.Services.AddScoped<IBranchUseCaseQuery, RegisterBranchUseCaseQuery>();

builder.Services.AddSingleton<IProductUseCaseQueryFactory, ProductUseCaseQueryFactory>();
builder.Services.AddScoped<IProductUseCaseQuery, AddProductUseCaseQuery>();
builder.Services.AddScoped<IProductCustomerSaleUseCaseQuery, RegisterProductFinalCustomerSaleUseCaseQuery>();
builder.Services.AddScoped<IProductResellerSaleUseCaseQuery, RegisterResellerSaleUseCaseQuery>();
builder.Services.AddScoped<IProductRegisterStockUseCaseQuery, RegisterProductInventoryStockUseCaseQuery>();

builder.Services.AddSingleton<IUserUseCaseQueryFactory, UserUseCaseQueryFactory>();
builder.Services.AddScoped<IUserRegisterUseCase, RegisterUserUseCaseQuery>();

builder.Services.AddScoped<IBranchRepository, BranchRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ISaleProductRepository, SaleRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

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
