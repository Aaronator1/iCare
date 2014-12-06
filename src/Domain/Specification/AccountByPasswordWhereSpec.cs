namespace Domain
{
    using System;
    using System.Linq.Expressions;
    using Incoding;

    public class AccountByPasswordWhereSpec : Specification<Account>
    {
        #region Fields

        readonly string password;

        #endregion

        #region Constructors

        public AccountByPasswordWhereSpec(string password)
        {
            this.password = password;
        }

        #endregion

        public override Expression<Func<Account, bool>> IsSatisfiedBy()
        {
            return account => account.Password == this.password;
        }
    }
}