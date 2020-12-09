using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using T1907A.Context;
using T1907A.Models;

namespace T1907A.Middleware
{
    public class ApiGetToken
    {
        private readonly RequestDelegate _next;

        public ApiGetToken(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            // truoc khi cho di phai lay duoc token
            IHeaderDictionary headers = httpContext.Request.Headers;
            string path = httpContext.Request.Path;
            if (path.StartsWith("/api/"))
            {
                string token = headers["token"];
                DataContext _context = new DataContext();
                Token t = _context.Tokens.Where(e => e.StrToken.Equals(token)).FirstOrDefault();
                if (t != null)
                {
                    User u = _context.Users.Find(t.UserId);
                    if (u != null)
                    {
                        await _next.Invoke(httpContext);
                    }
                }
            }
            else
            {
                await _next.Invoke(httpContext);
            }
           
            
        }
    }
}
