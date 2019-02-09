namespace Server.Side.Events.Middlewares
{
    using Microsoft.AspNetCore.Builder;
    using Server.Side.Events.Middlewares.Server_Side_Stream;
    using Server.Side.Events.Middlewares.Static_Pages;

    public static class MiddlewareHandlers
    {
        public static IApplicationBuilder AddServerSideEventMiddleware(this IApplicationBuilder app)
        {
            var result = app.UseMiddleware<ServerSideStreamMiddleware>();
            return result;
        }

        // i know it is supported by i just wanted to add this to compare it with expressjs
        public static IApplicationBuilder AddStaticPagesMiddleware(this IApplicationBuilder app)
        {
            var result = app.UseMiddleware<StaticPagesMiddleware>();
            return result;
        }
    }
}