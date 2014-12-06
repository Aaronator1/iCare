namespace Domain
{
    #region << Using >>

    using Incoding.CQRS;

    #endregion

    public class IsActualUserQuery : QueryBase<IncBoolResponse>
    {
        protected override IncBoolResponse ExecuteResult()
        {
            string userId = Dispatcher.Query(new GetCurrentUserIdQuery());
            return Repository.GetById<Account>(userId) != null;
        }
    }
}