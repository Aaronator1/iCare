namespace UI.Core
{
    #region << Using >>

    using System.Collections.Generic;
    using System.Web;
    using System.Web.Mvc;
    using Domain;
    using Incoding.Block.IoC;
    using Incoding.CQRS;
    using Incoding.MvcContrib;

    #endregion

    public class AuthenticationAttribute : AuthorizeAttribute
    {
        #region Fields

        readonly List<string> whiteList;

        #endregion

        #region Constructors

        public AuthenticationAttribute()
        {
            var url = new UrlHelper(HttpContext.Current.Request.RequestContext);
            this.whiteList = new List<string>
                                 {
                                         url.Dispatcher().Push<SignInAccountCommand>(),
                                         url.Dispatcher().Model(new SignInAccountCommand()).AsView("~/Views/Account/SignIn.cshtml"),
                                 };
        }

        #endregion

        public override void OnAuthorization(AuthorizationContext filterContext)
        {                        
            var request = filterContext.RequestContext.HttpContext.Request;
            if (this.whiteList.Contains(request.Url.PathAndQuery))
                return;

            if (request.Form.Get("incType") == typeof(SignInAccountCommand).Name)
                return;

            if (HttpContext.Current.User.Identity.IsAuthenticated && IoCFactory.Instance.TryResolve<IDispatcher>().Query(new IsActualUserQuery()))
                return;

            var url = new UrlHelper(HttpContext.Current.Request.RequestContext);
            if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
                filterContext.Result = IncodingResult.RedirectTo(url.Dispatcher().Model(new SignInAccountCommand()).AsView("~/Views/Account/SignIn.cshtml"));
        }
    }
}