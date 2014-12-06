namespace Domain
{
    #region << Using >>

    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel;
    using Incoding.CQRS;

    #endregion

    [ServiceContract(Namespace = "just_hacking_array_android")]
    public class GetActionPlansQuery : QueryBase<List<GetActionPlansQuery.Response>>
    {
        #region Nested classes

        public class Response
        {
            #region Properties

            public string Date { get; set; }

            public bool IsFirst { get; set; }

            public string Id { get; set; }

            #endregion
        }

        #endregion

        protected override List<Response> ExecuteResult()
        {
            string patientId = Dispatcher.Query(new GetCurrentUserIdQuery());
            return Repository.Query<ActionPlan>()
                             .ToList()
                             .Select((plan, i) => new Response
                                                      {
                                                              Id = plan.Id,
                                                              Date = plan.CreateDt.ToString("D"),
                                                              IsFirst = i == 0
                                                      })
                             .ToList();
        }
    }
}