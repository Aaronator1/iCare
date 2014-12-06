namespace UI.Core
{
    using System;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Incoding.MetaLanguageContrib;
    using Incoding.MvcContrib;

    public class HackHelper<TModel> where TModel : new()
    {
        #region Fields

        readonly HtmlHelper<TModel> htmlHelper;

        #endregion

        #region Constructors

        public HackHelper(HtmlHelper<TModel> htmlHelper)
        {
            this.htmlHelper = htmlHelper;
        }

        #endregion


        #region Api Methods

        public BeginTag BeginForm(Action<BeginFormSetting> action)
        {
            var url = new UrlHelper(HttpContext.Current.Request.RequestContext);
            var setting = new BeginFormSetting();
            action(setting);

            var routes = new RouteValueDictionary(new { @class = setting.AsInlineForm ? "form-inline" : "form-horizontal" });
            if (setting.IsMultiPart)
                routes.Add("enctype", "multipart/form-data");

            return htmlHelper.When(JqueryBind.InitIncoding)
                             .Direct()
                             .OnSuccess(dsl => dsl.Self().Core().Form.Validation.Parse())
                             .When(JqueryBind.Submit)
                             .PreventDefault()
                             .Submit()
                             .OnSuccess(dsl =>
                             {
                                 if (setting.CloseDialog)
                                     dsl.WithId("dialog").JqueryUI().Dialog.Close();

                                 if (setting.Back)
                                     dsl.Utilities.Document.Back();
                                 if (setting.RedirectToSelf)
                                     dsl.Utilities.Document.RedirectToSelf();
                                 if (setting.RedirectTo != null)
                                     dsl.Utilities.Document.RedirectTo(setting.RedirectTo);
                                 if (setting.TriggerTo != null)
                                     dsl.With(setting.TriggerTo).Core().Trigger.Incoding();
                                 if (setting.Trigger != null && setting.Trigger.Selector != null)
                                 {
                                     if (setting.Trigger.Event != null)
                                     {
                                         dsl.With(setting.Trigger.Selector)
                                            .Core()
                                            .Trigger.Invoke(setting.Trigger.Event);
                                     }
                                     else
                                     {
                                         dsl.With(setting.Trigger.Selector)
                                            .Core()
                                            .Trigger.Incoding();
                                     }
                                 }
                             })
                             .OnError(dsl => dsl.Self().Core().Form.Validation.Refresh())
                             .AsHtmlAttributes(routes)
                             .ToBeginForm(htmlHelper, url.Dispatcher().Push<TModel>());
        }



        #endregion

        #region Nested classes

        public class BeginFormSetting
        {
            #region Properties

            public Selector RedirectTo { get; set; }

            public bool CloseDialog { get; set; }

            public bool RedirectToSelf { get; set; }

            public bool Back { get; set; }

            public JquerySelectorExtend TriggerTo { get; set; }

            public bool IsMultiPart { get; set; }

            public bool AsInlineForm { get; set; }

            public Trigger Trigger { get; set; }

            #endregion
        }

        public class Trigger
        {
            #region Properties

            public JquerySelectorExtend Selector { get; set; }

            public JqueryBind Event { get; set; }

            #endregion
        }


        #endregion
    }
}