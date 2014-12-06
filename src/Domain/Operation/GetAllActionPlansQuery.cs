namespace Domain
{
    #region << Using >>

    using System.Collections.Generic;
    using System.Linq;
    using Incoding.CQRS;

    #endregion

    public class GetAllActionPlansQuery : QueryBase<List<GetAllActionPlansQuery.Response>>
    {
        #region Nested classes

        public class Response
        {
            #region Properties

            public string Id { get; set; }

            public string Patient { get; set; }

            public string Date { get; set; }

            #endregion
        }

        #endregion

        protected override List<Response> ExecuteResult()
        {
            return Repository.Query<ActionPlan>()
                             .ToList()
                             .Select(grouping => new Response
                                                     {
                                                             Patient = grouping.Patient.First + "," + grouping.Patient.Last,
                                                             Date = grouping.CreateDt.ToShortDateString(),
                                                             Id = grouping.Id
                                                     })
                             .ToList();
        }
    }
}