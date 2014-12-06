namespace Domain
{
    #region << Using >>

    using System.Linq;
    using System.ServiceModel;
    using System.Web;
    using System.Web.Security;
    using Incoding;
    using Incoding.CQRS;
    using Incoding.Extensions;

    #endregion

    [ServiceContract(Namespace = "just_hacking_array_android")]
    public class SignInAccountCommand : CommandBase
    {
        #region Properties

        public string Email { get; set; }

        public string Password { get; set; }

        #endregion

        public override void Execute()
        {
            var user = this.Repository.Query(whereSpecification: new AccountByEmailWhereSpec(this.Email)
                                                .And(new AccountByPasswordWhereSpec(this.Password))).SingleOrDefault();
            if (user == null)
                throw IncWebException.For<SignInAccountCommand>(r => r.Email, "User does not exist");

            HttpContext.Current.Response.Cookies.Add(new HttpCookie("UserId", user.Id));
            FormsAuthentication.SetAuthCookie(user.First, true);
        }
    }
}