namespace Domain
{
    using System;
    using System.Linq.Expressions;
    using Incoding;

    public class MedicationByActionPlanWhereSpec : Specification<Medication>
    {
        #region Fields

        readonly string actionPlanId;

        #endregion

        #region Constructors

        public MedicationByActionPlanWhereSpec(string actionPlanId)
        {
            this.actionPlanId = actionPlanId;
        }

        #endregion

        public override Expression<Func<Medication, bool>> IsSatisfiedBy()
        {
            return medication => medication.Plan.Id == this.actionPlanId;
        }
    }
}