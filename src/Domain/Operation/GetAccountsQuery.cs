namespace Domain
{
    #region << Using >>

    using System.Collections.Generic;
    using System.Linq;
    using Incoding.CQRS;
    using Incoding.Maybe;

    #endregion

    public class GetAccountsQuery : QueryBase<List<GetAccountsQuery.Response>>
    {
        #region Properties

        public Account.AccountOfType Type { get; set; }

        #endregion

        #region Nested classes

        public class Response
        {
            #region Properties

            public string Email { get; set; }

            public string First { get; set; }

            public string Last { get; set; }

            public string Id { get; set; }

            public string ActionPlanId { get; set; }

            public int Value { get; set; }

            public Monitoring.MonitorOfType Monitortype { get; set; }

            #endregion
        }

        #endregion

        protected override List<Response> ExecuteResult()
        {
            return Repository.Query(whereSpecification: new AccountByTypeWhereSpec(Type))
                .ToList()
                             .Select(r => new Response
                                              {
                                                  Id  = r.Id,
                                                      Email = r.Email,
                                                      First = r.First,
                                                      Last = r.Last,   
                                                      ActionPlanId = Repository.Query(whereSpecification:new ActionPlanByPatientWhereSpec(r.Id)).FirstOrDefault().With(plan=>plan.Id)
                                              })
                             .ToList();
        }
    }
}