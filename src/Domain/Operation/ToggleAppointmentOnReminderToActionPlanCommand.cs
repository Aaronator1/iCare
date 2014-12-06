namespace Domain
{
    using Incoding.CQRS;

    public class ToggleAppointmentOnReminderToActionPlanCommand : CommandBase
    {
        #region Properties

        public string Id { get; set; }

        #endregion

        public override void Execute()
        {
            var appointment = this.Repository.GetById<Appointment>(this.Id);
            appointment.Checked = !appointment.Checked;
        }
    }
}