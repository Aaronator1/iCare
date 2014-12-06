#region << Using >>

using UI.App_Start;
using WebActivator;

#endregion

[assembly: PreApplicationStartMethod(
        typeof(IncodingStart), "PreStart")]

namespace UI.App_Start
{
    #region << Using >>

    using Domain;
    using UI.Controllers;

    #endregion

    public static class IncodingStart
    {
        #region Factory constructors

        public static void PreStart()
        {
            Bootstrapper.Start();
            new DispatcherController(); // init routes
        }

        #endregion
    }
}