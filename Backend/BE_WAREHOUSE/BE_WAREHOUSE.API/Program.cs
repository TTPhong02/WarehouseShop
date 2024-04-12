using AutoMapper;
using BE_WAREHOUSE.Core.AutoMapper;
using BE_WAREHOUSE.Core.Interfaces.Base;
using BE_WAREHOUSE.Core.Interfaces.Cart;
using BE_WAREHOUSE.Core.Interfaces.Category;
using BE_WAREHOUSE.Core.Interfaces.Image;
using BE_WAREHOUSE.Core.Interfaces.Products;
using BE_WAREHOUSE.Core.Interfaces.Token;
using BE_WAREHOUSE.Core.Interfaces.User;
using BE_WAREHOUSE.Core.Services.Base;
using BE_WAREHOUSE.Core.Services.Cart;
using BE_WAREHOUSE.Core.Services.Category;
using BE_WAREHOUSE.Core.Services.Image;
using BE_WAREHOUSE.Core.Services.Products;
using BE_WAREHOUSE.Core.Services.Token;
using BE_WAREHOUSE.Core.Services.User;
using BE_WAREHOUSE.Infrastructure.DbContext;
using BE_WAREHOUSE.Infrastructure.Interfaces;
using BE_WAREHOUSE.Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MISA.AMISDemo.Core.Exceptions;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers()
.AddJsonOptions(x =>
{
    x.JsonSerializerOptions.PropertyNamingPolicy = null;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

//Config DI
builder.Services.AddScoped<IDbContext, MariaDbContext>();

builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<ICategoriesRepository, CategoriesRepository>();
builder.Services.AddScoped<ICategoriesService, CategoriesService>();

builder.Services.AddScoped<IImagesService,ImagesService>();
builder.Services.AddScoped<IImagesRepository,ImagesRepository>();

builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IUsersRepository, UserRepository>();

builder.Services.AddScoped<ICartsService, CartsService>();
builder.Services.AddScoped<ICartsRepository, CartsRepository>();

builder.Services.AddScoped<ICartItemsService, CartItemsService>();
builder.Services.AddScoped<ICartItemsRepository, CartItemsRepository>();

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<ITokenRepository, TokenRepository>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// Adding Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,

        ValidateLifetime = true,//kiểm tra token hết hạn
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,

        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        //chỉ ra key mà token sẽ dùng sau này
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<HandleExceptionMiddleware>();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseCors("AllowOrigin");

app.UseStaticFiles();
app.Run();
