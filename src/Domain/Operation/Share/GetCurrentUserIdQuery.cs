namespace Domain
{
    using System.Web;
    using Incoding.CQRS;

    public class GetCurrentUserIdQuery : QueryBase<string>
    {
        protected override string ExecuteResult()
        {
            return HttpContext.Current.Request.Cookies["UserId"].Value;
        }
    }
}