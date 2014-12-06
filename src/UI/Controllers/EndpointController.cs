namespace InstaLine.UI.Controllers
{
    #region << Using >>

    using System.Web.Mvc;

    #endregion

    public class EndpointController : Controller
    {
        #region Api Methods

        public ActionResult Index()
        {
            return View();
        }

        #endregion
    }
}