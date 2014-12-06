namespace Domain
{
    using Incoding.CQRS;

    public class MakrNotifcationCommand : CommandBase
    {
        #region Properties

        public string Id { get; set; }

        #endregion

        public override void Execute()
        {
            var notifiation = this.Repository.GetById<Notification>(this.Id);
            notifiation.IsMark = true;
        }
    }
}