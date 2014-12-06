namespace Domain
{
    #region << Using >>

    using System.Linq;
    using Incoding.Block.IoC;
    using Incoding.CQRS;
    using Incoding.Data;

    #endregion

    public class InitDataBase : ISetUp
    {
        #region ISetUp Members

        public int GetOrder()
        {
            return 1;
        }

        public void Execute()
        {
            var manager = IoCFactory.Instance.TryResolve<IManagerDataBase>();
            if (!manager.IsExist())
                manager.Update();
        }

        #endregion

        #region Disposable

        public void Dispose() { }

        #endregion
    }

}