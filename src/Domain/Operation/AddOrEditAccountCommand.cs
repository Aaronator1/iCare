namespace Domain
{
    #region << Using >>

    using System;
    using Incoding.CQRS;

    #endregion

    public class AddOrEditAccountCommand : CommandBase
    {
        #region Properties

        public string Id { get; set; }

        public string Staff { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string First { get; set; }

        public string Last { get; set; }

        public Account.AccountOfType Type { get; set; }



        #endregion

        public override void Execute()
        {
            var account = Repository.GetById<Account>(Id) ?? new Account();
            account.Email = Email;
            account.Password = Password ?? "Diaweb1$";
            account.First = First;
            account.Last = Last;
            account.Type = Type;
            account.Phone = Phone;
            account.Staff = this.Staff;
            Repository.SaveOrUpdate(account);
            if (Type == Account.AccountOfType.Patient)
            {
                var carePlan = new ActionPlan()
                                   {
                                           Patient = account,
                                           CreateDt = DateTime.Now
                                   };
                Repository.SaveOrUpdate(carePlan);
            }
        }
    }
}