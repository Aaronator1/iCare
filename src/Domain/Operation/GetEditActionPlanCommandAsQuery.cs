namespace Domain
{
    #region << Using >>

    using System.Linq;
    using Incoding.CQRS;

    #endregion

    public class GetEditActionPlanCommandAsQuery : QueryBase<AddOrEditCarePlanCommand>
    {
        #region Properties

        public string PatientId { get; set; }

        #endregion

        protected override AddOrEditCarePlanCommand ExecuteResult()
        {
            var plan = Repository.Query(whereSpecification: new ActionPlanByPatientWhereSpec(PatientId))
                                 .FirstOrDefault() ?? new ActionPlan();

            return new AddOrEditCarePlanCommand
                       {
                               Id = plan.Id,
                               PatientId = plan.Patient.Id,
                       };
        }
    }
}