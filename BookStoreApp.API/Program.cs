using BookStoreApp.API.Extensions;
var builder = WebApplication.CreateBuilder(args);

// Services Configuration
builder.Services.ConfigureDatabaseContext(builder.Configuration);
builder.Services.ConfigureIdentity();
builder.Services.ConfigureAutoMapper();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();
builder.Host.ConfigureLogging();
builder.Services.ConfigureCors();
builder.Services.AddJwtAuthentication(builder.Configuration);

var app = builder.Build();

// Middleware Configuration
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerMiddleware();
}

app.UseHttpsRedirection();
app.UseCorsMiddleware();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();










































//using BookStoreApp.API.Configurations;
//using BookStoreApp.API.Data;
//using BookStoreApp.API.Static;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.IdentityModel.Tokens;
//using Microsoft.OpenApi.Models;
//using Serilog;
//using System.Security.Claims;
//using System.Text;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//var connString = builder.Configuration.GetConnectionString("BookStoreAppDbConnection");
//builder.Services.AddDbContext<BookstoreContext>(options => options.UseSqlServer(connString));

//builder.Services.AddIdentityCore<ApiUser>()
//.AddRoles<IdentityRole>()
//.AddEntityFrameworkStores<BookstoreContext>();

//builder.Services.AddAutoMapper(typeof(MapperConfig));
//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Book Store App API", Version = "v1" });

//    // Add JWT Bearer authentication support in Swagger
//    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//    {
//        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
//        Name = "Authorization",
//        In = ParameterLocation.Header,
//        Type = SecuritySchemeType.Http,
//        Scheme = "Bearer",
//        BearerFormat = "JWT"
//    });

//    c.AddSecurityRequirement(new OpenApiSecurityRequirement
//    {
//        {
//            new OpenApiSecurityScheme
//            {
//                Reference = new OpenApiReference
//                {
//                    Type = ReferenceType.SecurityScheme,
//                    Id = "Bearer"
//                }
//            },
//            Array.Empty<string>()
//        }
//    });
//});

//builder.Host.UseSerilog((ctx, lc) =>
//    lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));

//builder.Services.AddCors(options => {
//    options.AddPolicy("AllowAll",
//        b => b.AllowAnyMethod()
//        .AllowAnyHeader()
//        .AllowAnyOrigin());
//});

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(options =>
//{
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuerSigningKey = true,
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = true,
//        ClockSkew = TimeSpan.Zero,
//        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
//        ValidAudience = builder.Configuration["JwtSettings:Audience"],
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
//    };

//    options.Events = new JwtBearerEvents
//    {
//        OnTokenValidated = async context =>
//        {
//            var userManager = context.HttpContext.RequestServices
//                .GetRequiredService<UserManager<ApiUser>>();

//            var user = await userManager.FindByIdAsync(
//                context.Principal.FindFirstValue(CustomClaimTypes.Uid));

//            if (user == null ||
//                context.Principal.FindFirstValue("AspNet.Identity.SecurityStamp")
//                != await userManager.GetSecurityStampAsync(user))
//            {
//                context.Fail("Invalid token");
//            }
//        }
//    };
//});

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseCors("AllowAll");

//app.UseAuthentication();
//app.UseAuthorization();

//app.MapControllers();

//app.Run();
