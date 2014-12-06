namespace UI.Controllers
{
    #region << Using >>

    using Domain;
    using Incoding.MvcContrib.MVD;
    using UI.Core;

    #endregion

    [Authentication]
    public class DispatcherController : DispatcherControllerBase
    {
        #region Constructors

        public DispatcherController()
                : base(new[]
                           {
                                   typeof(Bootstrapper).Assembly,
                                   typeof(MvdEndPoint.Domain.Bootstrapper).Assembly
                           }) { }

        #endregion
    }
}