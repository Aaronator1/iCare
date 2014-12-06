namespace Domain
{
    #region << Using >>

    using Incoding.CQRS;

    #endregion

    public class ToggleAnswerOnReminderToActionPlanCommand : CommandBase
    {
        #region Properties

        public string Id { get; set; }

        #endregion

        public override void Execute()
        {
            var answer = Repository.GetById<AnswerOnReminder>(Id);
            answer.Value = !answer.Value;
        }
    }
}