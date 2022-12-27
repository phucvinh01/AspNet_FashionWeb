using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace MyWebApp.Filters
{
    public class AdminFilters : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if(filterContext.HttpContext.User.IsInRole("Admin") == false)
            {
                filterContext.Result = new HttpUnauthorizedResult();                            
            }




            //throw new NotImplementedException();
        }
    }
}