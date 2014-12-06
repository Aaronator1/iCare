namespace Domain
{
    #region << Using >>

    using Incoding.CQRS;

    #endregion

    public class GetActionPlanDetailQuery : QueryBase<GetActionPlanDetailQuery.Response>
    {
        #region Properties

        public string Id { get; set; }

        #endregion

        #region Nested classes

        public class Response
        {
            #region Properties

            public string Date { get; set; }

            public string Id { get; set; }

            public string Patient { get; set; }

            #endregion
        }

        #endregion

        protected override Response ExecuteResult()
        {
            var plan = Repository.GetById<ActionPlan>(Id);
            return new Response
                       {
                               Id = plan.Id,
                               Date = plan.CreateDt.ToString("D"),          
                               Patient = plan.Patient.First + "," + plan.Patient.Last
                       };
        }
    }
}