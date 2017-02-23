using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading;
using System.Web;
using System.Web.Http;
using Project.Common.InternalContracts.Authentication;
using Project.Framework.CrossCuttingConcerns;

namespace Project.Endpoint.Web
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (Request.Url.AbsolutePath == "/")
            {
                Response.Write(@"<!DOCTYPE html>
<html>
<head>
    <title>Example</title>
    <meta charset=""utf-8"" />
</head>
<body>
    <h1>Header</h1>
</body>
</html>");
                CompleteRequest();

            }
        }

        protected void Application_PostAuthorizeRequest()
        {
            SetUserCulture();
        }

        #region User culture

        private void SetUserCulture()
        {
            var user = AC.Inst.GetPrincipal<IProjectPrincipal>();

            var lang = Context.Request.Cookies.Get("lang")?.Value;

            if (string.IsNullOrWhiteSpace(lang))
                lang = user.Lang;

            if (string.IsNullOrWhiteSpace(lang))
            {
                lang = Context.Request.Headers.Get("Accept-Language")?
                    .Split(',')
                    .Select(StringWithQualityHeaderValue.Parse)
                    .OrderByDescending(s => s.Quality.GetValueOrDefault(1))
                    .FirstOrDefault()?.Value;
            }

            var culture = GetCulture(lang).FirstOrDefault();

            if (culture != null)
            {
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
            }
        }

        private static IEnumerable<CultureInfo> GetCulture(string lang)
        {
            if (string.IsNullOrWhiteSpace(lang))
                yield break;

            CultureInfo culture = null;
            try
            {
                culture = CultureInfo.GetCultureInfo(lang);
            }
            catch (Exception ex)
            {
                AC.Inst.Log.Error("", ex);
            }

            if (culture != null)
                yield return culture;

            try
            {
                culture = CultureInfo.CreateSpecificCulture(lang);
            }
            catch (Exception ex)
            {
                AC.Inst.Log.Error("", ex);
            }

            if (culture != null)
                yield return culture;

            try
            {
                culture = CultureInfo.GetCultureInfo(1049);
            }
            catch (Exception ex)
            {
                AC.Inst.Log.Error("", ex);
            }

            if (culture != null)
                yield return culture;

            try
            {
                culture = CultureInfo.CreateSpecificCulture("ru");
            }
            catch (Exception ex)
            {
                AC.Inst.Log.Error("", ex);
            }

            if (culture != null)
                yield return culture;
        }

        #endregion
    }
}
