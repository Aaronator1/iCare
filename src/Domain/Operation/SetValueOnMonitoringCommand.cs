namespace Domain
{
    #region << Using >>

    using System.Linq;
    using Incoding.CQRS;

    #endregion

    public class SetValueOnMonitoringCommand : CommandBase
    {
        #region Properties

        public Monitoring.MonitorOfType Monitortype { get; set; }

        public int Value { get; set; }

        public string Id { get; set; }

        #endregion

        public override void Execute()
        {
            var actionPlan = Repository.Query(whereSpecification: new ActionPlanByPatientWhereSpec(Id)).FirstOrDefault();
            var monitoring = actionPlan.Monitorings.FirstOrDefault(r => r.Type == Monitortype);
            if (monitoring != null && (Value < monitoring.Min || Value > monitoring.Max))
            {
                Repository.Save(new Notification
                                    {
                                            ActionPlan = actionPlan,
                                            IsMark = false,
                                            Type = Monitortype,
                                            Message = "You have less or more then you value"
                                    });
            }
            Repository.SaveOrUpdate(new ValueOnMonitoring
                                        {
                                                Value = Value,
                                                Monitoring = Monitortype,
                                                ActionPlan = actionPlan
                                        });
        }
    }
}