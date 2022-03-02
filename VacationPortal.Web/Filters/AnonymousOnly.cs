using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace VacationPortal.Web.Filters
{
    public class AnonymousOnly : Attribute, IAuthorizationFilter
    {
        public string Role { get; set; } = "Admin";
        public string RedirectUrl1 { get; set; } = "/Admin/Employee";
        public string RedirectUrl2 { get; set; } = "/Client/Home";
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if (user.Identity.IsAuthenticated)
            {
                var isIn = user.IsInRole(Role);

                string url = isIn ? RedirectUrl1 : RedirectUrl2;

                context.Result = new RedirectResult(url);
            }
        }
    }
}
