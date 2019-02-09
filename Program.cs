namespace Server.Side.Events
{
    using System;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore;

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting server");
            CreateWebHostBuilder(args).Start("http://localhost:3000/");
            Console.Read();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
           WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();
    }
}
