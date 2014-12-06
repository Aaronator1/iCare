namespace UI.Controllers
{
    using System.Web.Mvc;
    using Domain;
    using Incoding.MvcContrib;
    using UI.Core;

    [Authentication]
    public class HomeController : IncControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Index2()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SetValuesOnMOnitoring(SetValueOnMonitoringCommand[] commands)
        {
            foreach (var command in commands)
            {
                TryPush(command);
            }
            return RedirectToAction("Index");
        }
    }
}