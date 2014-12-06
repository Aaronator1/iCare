namespace Domain
{
    using System;
    using System.Linq.Expressions;
    using Incoding;

    public class ActionPlanByPatientWhereSpec : Specification<ActionPlan>
    {
        #region Fields

        readonly string userId;

        #endregion

        #region Constructors

        public ActionPlanByPatientWhereSpec(string userId)
        {
            this.userId = userId;
        }

        #endregion

        public override Expression<Func<ActionPlan, bool>> IsSatisfiedBy()
        {
            return plan => plan.Patient.Id == this.userId;
        }
    }
}