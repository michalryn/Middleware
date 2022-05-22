using UAParser;

namespace Middleware
{
    public class CheckBrowserMiddleware
    {
        private RequestDelegate _next;
        
        public CheckBrowserMiddleware(RequestDelegate next)
        {
            _next = next;

        }

        public async Task Invoke(HttpContext context)
        {
            var userAgent = context.Request.Headers.UserAgent;
            var uaParser = Parser.GetDefault();
            ClientInfo c = uaParser.Parse(userAgent);

            if(c.UA.Family == "Edge" || c.UA.Family == "IE")
            {
                context.Response.Redirect("https://www.mozilla.org/pl/firefox/new/");
            }
            await _next(context);
        }
    }
}
