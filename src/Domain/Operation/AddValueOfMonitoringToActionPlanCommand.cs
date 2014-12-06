namespace Domain
{
    using Incoding.CQRS;

    public class AddValueOfMonitoringToActionPlanCommand : CommandBase
    {
        #region Properties

        public string ActionPlanId { get; set; }

        public Monitoring.MonitorOfType Type { get; set; }

        public int Value { get; set; }

        #endregion

        public override void Execute()
        {
            this.Repository.Save(new ValueOnMonitoring
                                {
                                        ActionPlan = this.Repository.GetById<ActionPlan>(this.ActionPlanId),
                                        Monitoring = Type,
                                        Value = this.Value
                                });
        }
    }
}