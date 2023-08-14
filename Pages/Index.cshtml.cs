using System;
using System.Collections.Generic;
using System.Linq;
//using System.Security.Claims;
using System.Threading.Tasks;
//using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
#region ForAuth
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
#endregion

namespace testauth.Pages
{
    public class IndexModel : PageModel
    {
        #region ForAuth
        public string UserName = "";
        #endregion
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            #region ForAuth
            UserName = Request.Headers["X-MS-CLIENT-PRINCIPAL-NAME"].ToString();
            //set user name in Authentication.Cookies
            var claims = new List<Claim>
                {
                    //use email or LINE user ID as login name
                    new Claim(ClaimTypes.Name,UserName),
                    //other data
                    new Claim("FullName",UserName),
                    new Claim(ClaimTypes.Role, "nobody"),
                };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
            };
           
            HttpContext.SignInAsync(
               CookieAuthenticationDefaults.AuthenticationScheme,
               new ClaimsPrincipal(claimsIdentity),
               authProperties);
            #endregion
        }
    }
}
