namespace Domain
{
    using System;
    using System.Linq.Expressions;
    using Incoding;

    public class AccountByEmailWhereSpec : Specification<Account>
    {
        #region Fields

        readonly string email;

        #endregion

        #region Constructors

        public AccountByEmailWhereSpec(string email)
        {
            this.email = email;
        }

        #endregion

        public override Expression<Func<Account, bool>> IsSatisfiedBy()
        {
            return account => account.Email == this.email;
        }
    }
}