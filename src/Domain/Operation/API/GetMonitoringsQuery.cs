namespace Domain
{
    #region << Using >>

    using System.Collections.Generic;
    using System.Linq;
    using Incoding.CQRS;
    using Incoding.Extensions;

    #endregion

    public class GetMonitoringsQuery : QueryBase<List<GetMonitoringsQuery.Response>>
    {
        #region Properties

        public string ActionPlanId { get; set; }

        #endregion

        #region Nested classes

        public class Response
        {
            #region Properties

            public string Status { get; set; }

            public string Type { get; set; }

            public string Max { get; set; }

            public string Min { get; set; }

            #endregion
        }

        #endregion

        protected override List<Response> ExecuteResult()
        {
            return Repository.GetById<ActionPlan>(ActionPlanId)
                             .Monitorings
                             .Select(monitoring => new Response
                                                       {
                                                               Type = monitoring.Type.ToLocalization(),
                                                               Status = Repository.Query<Notification>()
                                                                                  .Any(r => !r.IsMark && r.Type == monitoring.Type) ? "red" : "green",
                                                               Max = monitoring.Max.ToString(),
                                                               Min = monitoring.Min.ToString()
                                                       })
                             .ToList();
        }
    }
}