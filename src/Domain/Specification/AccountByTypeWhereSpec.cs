namespace Domain
{
    using System;
    using System.Linq.Expressions;
    using Incoding;

    public class AccountByTypeWhereSpec : Specification<Account>
    {
        #region Fields

        readonly Account.AccountOfType type;

        #endregion

        #region Constructors

        public AccountByTypeWhereSpec(Account.AccountOfType type)
        {
            this.type = type;
        }

        #endregion

        public override Expression<Func<Account, bool>> IsSatisfiedBy()
        {
            return account => account.Type == this.type;
        }
    }
}