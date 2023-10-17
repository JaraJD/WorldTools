using Microsoft.EntityFrameworkCore;
using WorldTools.Application.Queries.Factory;
using WorldTools.Application.Queries.UseCases.BranchUseCases;
using WorldTools.Domain.Factory;
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
using WorldTools.Application.Queries.UseCases.SaleUseCases;
using WorldTools.SqlAdapter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<GetBrachByIdUseCase>();
builder.Services.AddScoped<GetAllBranchUseCase>();

builder.Services.AddScoped<GetProductByIdUseCaseQuery>();
builder.Services.AddScoped<GetProductsByBranchIdUseCaseQuery>();
builder.Services.AddScoped<GetAllProductsUseCaseQuery>();

builder.Services.AddScoped<GetUserByIdUseCase>();
builder.Services.AddScoped<GetAllUsersUseCase>();
builder.Services.AddScoped<LoginUserUseCase>();

builder.Services.AddTransient<GetAllSalesByBranchIdUseCase>();

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

builder.Services.AddDbContext<ContextSql>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(config => config.AddDataReaderMapping(), typeof(MappingProfileSql));

builder.Services.AddAutoMapper(config => {
    config.AddProfile<MappingProfileSql>();
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200")
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials();
        });
});

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowSpecificOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
