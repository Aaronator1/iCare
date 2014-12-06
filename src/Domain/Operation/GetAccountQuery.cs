using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Operation
{
    using Incoding.CQRS;

    public class GetAccountQuery : QueryBase<AddOrEditAccountCommand>
    {
        public string Id { get; set; }

        public Account.AccountOfType Type { get; set; }

        protected override AddOrEditAccountCommand ExecuteResult()
        {
            var account = Repository.GetById<Account>(Id);
            return new AddOrEditAccountCommand()
                       {
                               Id = account.Id,
                               Email = account.Email,
                               First = account.First,
                               Last = account.Last,
                               Type = account.Type
                       };
        }
    }
}
