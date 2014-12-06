namespace Domain
{
    #region << Using >>

    using System.Collections.Generic;
    using System.Linq;
    using Incoding.CQRS;

    #endregion

    public class GetAppointmentsQuery : QueryBase<List<GetAppointmentsQuery.Response>>
    {
        #region Properties

        public string ActionPlanId { get; set; }

        public bool? IsCarePlan { get; set; }

        #endregion

        #region Nested classes

        public class Response
        {
            #region Properties

            public string Id { get; set; }

            public string Name { get; set; }

            public string Address { get; set; }

            public string Phone { get; set; }

            public bool Checked { get; set; }

            public string Map { get; set; }

            public string Date { get; set; }

            public string Staff { get; set; }

            #endregion
        }

        #endregion

        protected override List<Response> ExecuteResult()
        {
            return Repository.Query(whereSpecification: new AppointmentByActionPlanWhereSpec(ActionPlanId))
                             .Where(r => r.IsCarePlan == IsCarePlan.GetValueOrDefault())
                             .ToList()
                             .Select(r => new Response
                                              {
                                                      Id = r.Id,
                                                      Date = r.Date.ToShortDateString(),
                                                      Checked = r.Checked,
                                                      Name = r.Name,
                                                      Address = string.Join(" ", new [] { r.Address, r.Address1, !string.IsNullOrEmpty(r.State) 
                                                                                                                                ? string.Format("( {0} )", r.State) : "" , r.Address2 }),
                                                      Phone = r.Phone,
                                                      Staff = r.Staff,
                                                      Map = "https://www.google.com/maps/place/" + r.Address
                                              })
                             .ToList();
        }
    }
}