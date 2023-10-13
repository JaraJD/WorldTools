using WorldTools.Infrastructure;
using Microsoft.EntityFrameworkCore;
using AutoMapper.Data;
using WorldTools.Infrastructure.Repositories;
using WorldTools.MongoAdapter;
using WorldTools.MongoAdapter.Interfaces;
using WorldTools.MongoAdapter.Common.Mapping;
using WorldTools.Domain.Ports;
using WorldTools.SqlAdapter.Adapters;
using WorldTools.MongoAdapter.Adapters;
using WorldTools.SqlAdapter.Common.Mapping;
using WorldTools.Application.UseCases.BranchUseCases;
using WorldTools.Application.UseCases.ProductUseCases;
using WorldTools.Application.UseCases.UserUseCases;
using WorldTools.Rabbit.PublishAdapter;
using WorldTools.Domain.Ports.BranchPorts;
using WorldTools.Domain.Ports.ProductPorts;
using WorldTools.Domain.Ports.UserPorts;
using WorldTools.WebSocketAdapter.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<WebSocketService>();

builder.Services.AddTransient<RegisterBranchUseCase>();
builder.Services.AddScoped<IBranchRepository, BranchRepository>();

builder.Services.AddTransient<AddProductUseCase>();
builder.Services.AddTransient<RegisterProductFinalCustomerSaleUseCase>();
builder.Services.AddTransient<RegisterProductInventoryStockUseCase>();
builder.Services.AddTransient<RegisterResellerSaleUseCase>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddTransient<RegisterUserUseCase>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ISaleProductRepository, SaleRepository>();

builder.Services.AddScoped<IStoredEventRepository, StoredEventRepository>();
builder.Services.AddScoped<IPublishEventRepository, PublishEvent>();


builder.Services.AddAutoMapper(config => config.AddDataReaderMapping(), typeof(MappingProfileMongo));
builder.Services.AddAutoMapper(config => {
    config.AddProfile<MappingProfileSql>();
    config.AddProfile<MappingProfileMongo>();
});

builder.Services.AddSingleton<IContextMongo>(provider => new ContextMongo(builder.Configuration.GetConnectionString("MongoConnection"), "Events"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Connection"))
);

builder.Services.AddControllers();

builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCorsPolicy", builder =>
    {
        builder.SetIsOriginAllowed(origen => true)
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseCors("MyCorsPolicy");

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<WebSocketService>("/WebSocked");
    endpoints.MapControllers();
});

app.MapControllers();

app.Run();
