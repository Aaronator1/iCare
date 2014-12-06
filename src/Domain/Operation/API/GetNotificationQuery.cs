namespace Domain
{
    #region << Using >>

    using System.Collections.Generic;
    using System.Linq;
    using Incoding.CQRS;

    #endregion

    public class GetNotificationQuery : QueryBase<GetNotificationQuery.Response>
    {
        #region Nested classes

        public class Response
        {
            #region Properties

            public string Count { get { return Items.Count.ToString(); } }

            public List<Item> Items { get; set; }

            #endregion

            #region Nested classes

            public class Item
            {
                #region Properties

                public string Message { get; set; }

                public string Id { get; set; }

                #endregion
            }

            #endregion
        }

        #endregion

        protected override Response ExecuteResult()
        {
            return new Response
                       {
                               Items = Repository.Query<Notification>()
                                                 .Where(r => !r.IsMark)
                                                 .Select(r => new Response.Item
                                                                  {
                                                                          Message = r.Message,
                                                                          Id = r.Id
                                                                  })
                                                 .ToList()
                       };
        }
    }
}