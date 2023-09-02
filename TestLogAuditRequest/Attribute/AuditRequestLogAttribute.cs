using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using System.Text.RegularExpressions;
using TestLogAuditRequest.Models.TestAuditControllerRequest;

namespace TestLogAuditRequest.Attribute
{
    public class AuditRequestLogAttribute : ActionFilterAttribute
    {
        public TestAuditControllerRequestContext _context { get; set; }

        public AuditRequestLogAttribute(TestAuditControllerRequestContext context)
        {
            _context = context;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Additional Auditing-based Logic Here  
            base.OnActionExecuting(filterContext);

            var request = filterContext.HttpContext.Request;

            IPAddress? remoteIpAddress = null;
            var forwardedFor = request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (!string.IsNullOrEmpty(forwardedFor))
            {
                var ips = forwardedFor.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                      .Select(s => s.Trim());
                foreach (var ip in ips)
                {
                    if (IPAddress.TryParse(ip, out var address) &&
                        (address.AddressFamily is AddressFamily.InterNetwork
                         or AddressFamily.InterNetworkV6))
                    {
                        remoteIpAddress = address;
                        break;
                    }
                }
            }

            List<REQUEST_QUERTY_CATEGORY> categories = _context.REQUEST_QUERTY_CATEGORYs
                .Where(s => s.INUSE == true)
                .ToList();

            long? categoryId = null;
            foreach(var c in categories)
            {
                Regex rx = new Regex($@"{c.CATEGORY_PATH_REGEX}");

                if (rx.IsMatch(request.Path))
                {
                    categoryId = c.ID;
                    break;
                }
            }


            REQUEST_LOG log = new REQUEST_LOG()
            {
                REQUEST_IP_ADDRESS = remoteIpAddress?.ToString() ?? "localhost",
                REQUEST_METHOD = request.Method,
                REQUEST_PATH = request.Path,
                REQUEST_QUERY_STRING = request.QueryString.ToString(),
                REQUEST_BODY = request.Body.ToString(),
                REQUEST_TRACE_IDENTIFIER = request.HttpContext.TraceIdentifier,
                REQUEST_USER_AGENT = request.Headers["User-Agent"].FirstOrDefault(),
                USER_ID = 0,
                USER_CLAIMS = JsonSerializer.Serialize(request.HttpContext.User.Claims),
                CASES_ID = 0,
                CASE_NO = "",
                QUERY_TARGET_ID = 999,
                QUERY_CATEGORY_ID = categoryId,
            };

            _context.REQUEST_LOGs.Add(log);
            _context.SaveChanges();
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            // Additional Auditing-based Logic Here  
            base.OnActionExecuted(filterContext);


            var request = filterContext.HttpContext.Request;

            int? statusCode = null;
            if (filterContext.Result?.GetType() == typeof(ForbidResult))
            {
                statusCode = 403;
            }
            else if (filterContext.Result?.GetType() == typeof(BadRequestResult))
            {
                statusCode = 403;
            }
            else if (filterContext.Result?.GetType() == typeof(NotFoundResult) || filterContext.Result?.GetType() == typeof(NotFoundObjectResult))
            {
                statusCode = 404;
            }
            else
            {
                statusCode = (filterContext.Result as StatusCodeResult)?.StatusCode;
            }

            REQUEST_RESULT_LOG log = new REQUEST_RESULT_LOG()
            {
                REQUEST_TRACE_IDENTIFIER = request.HttpContext.TraceIdentifier,
                REQUEST_METHOD = request.Method,
                REQUEST_PATH = request.Path,
                RESPONSE_STATUS_CODE = (filterContext.Result as StatusCodeResult)?.StatusCode,
                EXCEPTION_MESSAGE = filterContext.Exception?.Message ?? filterContext.Exception?.InnerException?.Message ?? filterContext.Exception?.StackTrace?.Substring(0, 4000),
                USER_ID = 0,
            };

            _context.REQUEST_RESULT_LOGs.Add(log);
            _context.SaveChanges();
        }

        //public override void OnResultExecuting(ResultExecutingContext filterContext)
        //{
        //    // Additional Auditing-based Logic Here  
        //    base.OnResultExecuting(filterContext);

        //    var a = filterContext.Result;
        //}

        //public override void OnResultExecuted(ResultExecutedContext filterContext)
        //{
        //    // Additional Auditing-based Logic Here  
        //    base.OnResultExecuted(filterContext);

        //    var a = filterContext.Result;
        //}
    }
}
