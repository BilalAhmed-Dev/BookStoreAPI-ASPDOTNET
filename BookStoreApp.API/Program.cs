using BookStoreApp.API.Extensions;
using BookStoreApp.API.Service;
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
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddHttpContextAccessor();
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