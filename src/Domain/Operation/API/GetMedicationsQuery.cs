namespace Domain
{
    #region << Using >>

    using System.Collections.Generic;
    using System.Linq;
    using Incoding.CQRS;

    #endregion

    public class GetMedicationsQuery : QueryBase<List<GetMedicationsQuery.Response>>
    {
        #region Properties

        public string ActionPlanId { get; set; }

        #endregion

        #region Nested classes

        public class Response
        {
            #region Properties

            public string Name { get; set; }

            public bool Checked { get; set; }

            public string Id { get; set; }

            #endregion
        }

        #endregion

        protected override List<Response> ExecuteResult()
        {
            return Repository.Query(whereSpecification: new MedicationByActionPlanWhereSpec(ActionPlanId))
                             .Select(r => new Response
                                              {
                                                      Id = r.Id,
                                                      Checked = r.Checked,
                                                      Name = r.Name
                                              })
                             .ToList();
        }
    }
}