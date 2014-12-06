namespace Domain
{
    #region << Using >>

    using System.Collections.Generic;
    using System.Linq;
    using Incoding.CQRS;

    #endregion

    public class GetRemindersQuery : QueryBase<List<GetRemindersQuery.Response>>
    {
        #region Properties

        public string ActionPlanId { get; set; }

        #endregion

        #region Nested classes

        public class Response
        {
            #region Properties

            public bool Value { get; set; }

            public string Title { get; set; }

            public string Id { get; set; }

            #endregion
        }

        #endregion

        protected override List<Response> ExecuteResult()
        {
            return Repository.GetById<ActionPlan>(ActionPlanId)
                             .Reminders
                             .Select(r => new Response
                                              {
                                                      Id = r.Id,
                                                      Title = r.Title,
                                                      Value = r.Value
                                              })
                             .ToList();
        }
    }
}