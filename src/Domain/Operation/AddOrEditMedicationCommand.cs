namespace Domain
{
    using Incoding.CQRS;

    public class AddOrEditMedicationCommand : CommandBase
    {
        #region Properties

        public string ActionPlanId { get; set; }

        public string Name { get; set; }

        #endregion

        public override void Execute()
        {
            this.Repository.Save(new Medication
                                {
                                        Name = this.Name,
                                        Plan = this.Repository.GetById<ActionPlan>(this.ActionPlanId)
                                });
        }
    }
}