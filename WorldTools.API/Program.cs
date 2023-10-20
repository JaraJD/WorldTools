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
using WorldTools.SqlAdapter;
using WorldTools.Rabbit.SubscribeAdapter;
using System.Text;
using WorldTools.SqlAdapter.Common.Secrets;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ContextSql>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Connection"))
);

builder.Services.AddControllers();

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
app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("MyCorsPolicy");

app.MapControllers();

app.Run();