namespace Domain
{
    #region << Using >>

    using Incoding.CQRS;

    #endregion

    public class AddAnswerOnReminderToActionPlanCommand : CommandBase
    {
        #region Properties

        public string ActionPlanId { get; set; }

        public string Title { get; set; }

        public bool Value { get; set; }

        #endregion

        public override void Execute()
        {
            Repository.Save(new AnswerOnReminder
                                {
                                        ActionPlan = Repository.GetById<ActionPlan>(this.ActionPlanId),
                                        Title = Title,
                                        Value = Value
                                });
        }
    }
}