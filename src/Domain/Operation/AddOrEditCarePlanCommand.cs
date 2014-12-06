namespace Domain
{
    using System;
    using Incoding.CQRS;

    public class AddOrEditCarePlanCommand : CommandBase
    {
        #region Properties

        public string Id { get; set; }

        public string PatientId { get; set; }

        public DateTime CreateDt { get; set; }
        
        #endregion

        public override void Execute()
        {
            var carePlan = this.Repository.GetById<ActionPlan>(this.Id) ?? new ActionPlan();
            carePlan.Patient = this.Repository.GetById<Account>(this.PatientId);
            carePlan.CreateDt = this.CreateDt;
            this.Repository.SaveOrUpdate(carePlan);
        }
    }
}