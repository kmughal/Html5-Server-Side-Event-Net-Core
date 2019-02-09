namespace Server.Side.Events.Middlewares.Static_Pages
{
    using Microsoft.AspNetCore.Http;
    using System.IO;
    using System.Threading.Tasks;
    using System;

    public class StaticPagesMiddleware
    {
        private readonly RequestDelegate _next;

        public StaticPagesMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var absoluteFilePath = CreateFileAbsolutePath(context.Request.Path);
            Console.WriteLine(FileExists(absoluteFilePath));
            if (!FileExists(absoluteFilePath))
            {
                await _next(context);
                return;
            }
            context.Response.Headers.Add("Content-Type", "text/html");
            await context.Response.SendFileAsync(absoluteFilePath);
        }

        private bool FileExists(string absoluteFilePath)
        {
            var result = File.Exists(absoluteFilePath);
            return result;
        }

        private string CreateFileAbsolutePath(string filename)
        {
            var fileNameWithExtension = GetFilePath(filename);
            var absoluteFilePath = $"../../../StaticPages/{fileNameWithExtension}";
            //var absoluteFilePath = $"StaticPages/{fileNameWithExtension}";
            return absoluteFilePath;
        }
        private string GetFilePath(string fileName)
        {
            var fileNameWithoutslashes = RemoveSlashes(fileName);
            var result = $"{fileNameWithoutslashes}.html";
            return result;
        }


        private string RemoveSlashes(string path)
        {
            return path?.Replace("/", "").Replace("\\", "");
        }
    }
}