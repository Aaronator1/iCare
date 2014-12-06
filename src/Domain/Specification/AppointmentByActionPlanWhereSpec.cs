namespace Domain
{
    using System;
    using System.Linq.Expressions;
    using Incoding;

    public class AppointmentByActionPlanWhereSpec : Specification<Appointment>
    {
        #region Fields

        readonly string actionPlandId;

        #endregion

        #region Constructors

        public AppointmentByActionPlanWhereSpec(string actionPlandId)
        {
            this.actionPlandId = actionPlandId;
        }

        #endregion

        public override Expression<Func<Appointment, bool>> IsSatisfiedBy()
        {
            return appointment => appointment.ActionPlan.Id == this.actionPlandId;
        }
    }
}