namespace Domain
{
    #region << Using >>

    using Incoding;
    using Incoding.CQRS;

    #endregion

    public class AddOrEditMonitoringCommand : CommandBase
    {
        #region Properties

        public string Id { get; set; }

        public string ActionPlanId { get; set; }

        public Monitoring.MonitorOfType Type { get; set; }

        public int Max { get; set; }

        public int Min { get; set; }

        #endregion

        public override void Execute()
        {
            if (Min > Max)
                throw IncWebException.For<AddOrEditMonitoringCommand>(r => r.Max, "Please enter a valid value range.");
            var monitoring = Repository.GetById<Monitoring>(Id) ?? new Monitoring();
            monitoring.Type = Type;
            monitoring.Max = Max;
            monitoring.Min = Min;
            monitoring.ActionPlan = Repository.GetById<ActionPlan>(ActionPlanId);
            Repository.SaveOrUpdate(monitoring);
        }
    }
}