using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Middlewares
{
    public class Logger
    {
        readonly RequestDelegate next;

        readonly string path;
        readonly long maxSize;
        readonly FileStream fileStream;

        public Logger(RequestDelegate next, string path, long maxSize)
        {
            this.next = next;
            this.path = path;
            this.maxSize = maxSize;

            fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
            fileStream.Seek(0, SeekOrigin.End);
        }

        ~Logger()
        {
            fileStream.Close();
        }

        public async Task Invoke(HttpContext context)
        {
            var request = await FormatRequest(context.Request);
            var originalBodyStream = context.Response.Body;

            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;
                await next(context);

                var response = await FormatResponse(context.Response);

                var timestamp = DateTime.UtcNow.ToString("dd-MM-yyyy HH:mm:ss:ffff");
                if (fileStream.Position > maxSize)
                    fileStream.Seek(0, SeekOrigin.Begin);
                using (StreamWriter sw = new StreamWriter(fileStream, Encoding.UTF8, 1024, true))
                {
                    sw.WriteLine("!!! LOG_ENTRY_BEGIN !!!");
                    sw.WriteLine(timestamp);
                    sw.WriteLine(request);
                    sw.WriteLine(response);
                    sw.WriteLine("!!! LOG_ENTRY_END !!!");
                }

                await responseBody.CopyToAsync(originalBodyStream);
            }
        }

        private async Task<string> FormatRequest(HttpRequest request)
        {
            var body = request.Body;
            request.EnableRewind();
            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            var bodyAsText = Encoding.UTF8.GetString(buffer);
            request.Body = body;
            return $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {bodyAsText}";
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            string text = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);
            return $"{response.StatusCode} : {text}";
        }
    }
}
