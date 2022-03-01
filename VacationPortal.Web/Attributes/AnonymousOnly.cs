using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace VacationPortal.Web.Attributes
{
    public class AnonymousOnly : Attribute, IAuthorizationFilter
    {
        public string Role { get; set; }
        public string Url1 { get; set; }
        public string Url2 { get; set; }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if (user.Identity.IsAuthenticated)
            {
                var isAdmin = user.IsInRole(Role);

                string url = isAdmin ? Url1 : Url2;

                context.Result = new RedirectResult(url);
            }
        }
    }
}
