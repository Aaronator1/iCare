namespace UI.Core
{
    using System.Web.Mvc;

    public static class HtmlEx
    {
        

        #region Factory constructors

        public static HackHelper<TModel> Hack<TModel>(this HtmlHelper<TModel> helper) where TModel : new()
        {
            return new HackHelper<TModel>(helper);
        }

        #endregion
    }
}