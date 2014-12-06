namespace Domain
{
    #region << Using >>

    using System.Collections.Generic;
    using System.Linq;
    using Incoding.CQRS;
    using Incoding.Extensions;

    #endregion

    public class GetValueOnMonitorQuery : QueryBase<List<GetValueOnMonitorQuery.Response>>
    {
        #region Nested classes

        public class Response
        {
            #region Properties

            public string Id { get; set; }

            public string Patient { get; set; }

            public string Type { get; set; }

            public string Value { get; set; }

            #endregion
        }

        #endregion

        protected override List<Response> ExecuteResult()
        {
            return Repository.Query<ValueOnMonitoring>().ToList()
                             .Select(monitoring => new Response
                                                       {
                                                               Id = monitoring.Id,
                                                               Patient = monitoring.ActionPlan.Patient.Email,
                                                               Type = monitoring.Monitoring.ToLocalization(),
                                                               Value = monitoring.Value.ToString()
                                                       })
                             .ToList();
        }
    }
}