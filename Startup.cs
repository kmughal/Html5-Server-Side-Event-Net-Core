namespace Server.Side.Events
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Server.Side.Events.Middlewares;
    using Server.Side.Events.Services;

    internal class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddTransient<IServerSideService, ServerSideService>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.AddStaticPagesMiddleware();
            app.AddServerSideEventMiddleware();
        }
    }
}