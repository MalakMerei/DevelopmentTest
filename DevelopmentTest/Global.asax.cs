using System;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using DevelopmentTest.App_Start;

namespace DevelopmentTest
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
