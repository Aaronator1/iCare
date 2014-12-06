namespace Domain
{
    #region << Using >>

    using System;
    using Incoding.CQRS;

    #endregion

    public class AddOrEditAppointmentCommand : CommandBase
    {
        #region Properties

        public string Id { get; set; }

        public string ActionPlanId { get; set; }

        public string Address { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public DateTime? Date { get; set; }

        public bool IsCareTeam { get; set; }

        public string Staff { get; set; }

        public string State { get; set; }

        #endregion

        public override void Execute()
        {
            var appointment = Repository.GetById<Appointment>(Id) ?? new Appointment();

            appointment.ActionPlan = Repository.GetById<ActionPlan>(ActionPlanId);
            appointment.Address = Address;
            appointment.Address1 = Address1;
            appointment.Address2 = Address2;
            appointment.State = State;
            appointment.Name = Name;
            appointment.IsCarePlan = IsCareTeam;
            appointment.Staff = Staff;
            appointment.Phone = Phone;
            appointment.Date = Date.GetValueOrDefault(Date.GetValueOrDefault(DateTime.Now));
            Repository.Save(appointment);
        }
    }
}