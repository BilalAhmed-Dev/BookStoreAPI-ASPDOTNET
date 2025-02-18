namespace BookStoreApp.API.Extensions;

public static class MiddlewareExtensions
{
    public static void UseSwaggerMiddleware(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    public static void UseCorsMiddleware(this IApplicationBuilder app)
    {
        app.UseCors("AllowAll");
    }
}