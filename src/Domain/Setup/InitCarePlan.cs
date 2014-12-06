namespace Domain
{
    #region << Using >>

    using System;
    using System.Linq;
    using Incoding.Block.IoC;
    using Incoding.CQRS;

    #endregion

    public class InitCarePlan : ISetUp
    {
        #region ISetUp Members

        public int GetOrder()
        {
            return 2;
        }

        public void Execute()
        {
            var dispatcher = IoCFactory.Instance.TryResolve<IDispatcher>();
            if (dispatcher.Query(new GetEntitiesQuery<ActionPlan>()).Any())
                return;

            var allPatients = dispatcher.Query(new GetAccountsQuery { Type = Account.AccountOfType.Patient })
                                        .Select(r => r.Id)
                                        .ToArray();
            foreach (var t in allPatients)
            {
                dispatcher.Push(new AddOrEditCarePlanCommand
                                    {
                                            CreateDt = DateTime.Now,
                                            PatientId = t
                                    });
            }

            var array = dispatcher.Query(new GetEntitiesQuery<ActionPlan>()).Select(r => r.Id).ToArray();
            for (int index = 0; index < array.Length; index++)
            {
                string planId = array[index];

                dispatcher.Push(new AddValueOfMonitoringToActionPlanCommand
                                    {
                                            ActionPlanId = planId,
                                            Type = Monitoring.MonitorOfType.Temperature,
                                            Value = 30
                                    });
                dispatcher.Push(new AddAnswerOnReminderToActionPlanCommand
                                    {
                                            ActionPlanId = planId,
                                            Title = "Set remind",
                                            Value = true
                                    });

                dispatcher.Push(new AddOrEditAppointmentCommand
                                    {
                                            Date = DateTime.Now,
                                            ActionPlanId = planId,
                                            Name = "Appointment " + index,
                                            Phone = "Phone " + index,
                                            Address = "3600 Fm-2181, Ste 100, Hickory Creek, TX 75065",
                                    });
                dispatcher.Push(new AddOrEditAppointmentCommand
                                    {
                                            Date = DateTime.Now,
                                            ActionPlanId = planId,
                                            Name = "Care Team " + index,
                                            Phone = "Phone " + index,
                                            Address = "3600 Fm-2181, Ste 100, Hickory Creek, TX 75065",
                                            IsCareTeam = true,
                                            Staff = "PCP"
                                    });
                dispatcher.Push(new AddOrEditMedicationCommand
                                    {
                                            ActionPlanId = planId,
                                            Name = "Medication on " + index
                                    });
            }
        }

        #endregion

        #region Disposable

        public void Dispose() { }

        #endregion
    }
}