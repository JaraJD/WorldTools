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
using WorldTools.Domain.Ports;
using WorldTools.WebSocketAdapter.Service;
using WorldTools.Rabbit.SubscribeSocketAdapter;
using WorldTools.Rabbit.SubscribeSocketAdapter.Factory;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using WorldTools.SqlAdapter.Common.Secrets;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddScoped<IWebSocketPort, WebSocketHandler>();

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
builder.Services.AddHostedService<SubscribeEventSocket>();

builder.Services.AddSingleton<IBranchUseCaseQueryFactory, BranchUseCaseQueryFactory>();
builder.Services.AddScoped<IBranchUseCaseQuery, RegisterBranchUseCaseQuery>();

builder.Services.AddSingleton<IProductRepositoryFactory, ProductRepositoryFactory>();
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


builder.Services.AddSwaggerGen(setup =>
{
    // Include 'SecurityScheme' to use JWT Authentication
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

var appSettingsSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingsSection);

//JWT
var appSettings = appSettingsSection.Get<AppSettings>();
var llave = Encoding.ASCII.GetBytes(appSettings.Secreto);

builder.Services.AddAuthentication(d =>
{
    d.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    d.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(d =>
    {
        d.RequireHttpsMetadata = false;
        d.SaveToken = true;
        d.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(llave),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });


builder.Services.AddDbContext<ContextSql>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(config => config.AddDataReaderMapping(), typeof(MappingProfileSql));

builder.Services.AddAutoMapper(config => {
    config.AddProfile<MappingProfileSql>();
});

builder.Services.AddSignalR();

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

app.UseRouting();

app.UseCors("AllowSpecificOrigin");

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<WebSocketService>("/WebSocked");
    endpoints.MapControllers();
});

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
