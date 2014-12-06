namespace Domain
{
    #region << Using >>

    using System.Linq;
    using Incoding.Block.IoC;
    using Incoding.CQRS;
    using Incoding.Extensions;

    #endregion

    public class InitAccount : ISetUp
    {
        #region ISetUp Members

        public int GetOrder()
        {
            return 1;
        }

        public void Execute()
        {
            var dispatcher = IoCFactory.Instance.TryResolve<IDispatcher>();
            if (dispatcher.Query(new GetEntitiesQuery<Account>()).Any())
                return;

            dispatcher.Push(new AddOrEditAccountCommand
                                {
                                        Email = "admin@mail.com",
                                        Password = "Diaweb1$",
                                        First = "Aaron",
                                        Last = "Smith",
                                        Type = Account.AccountOfType.Staff,
                                        Staff = "PCP"
                                });

            dispatcher.Push(new AddOrEditAccountCommand
                                {
                                        Email = "admin2@mail.com",
                                        Password = "Diaweb1$",
                                        First = "Mark",
                                        Last = "Robertson",
                                        Type = Account.AccountOfType.Staff,
                                        Staff = "Pediatrician"
                                });

            foreach (var email in new[]
                                      {
                                              "vlad.kopachinsky@gmail.com",
                                              "aarons@chirondata.com",
                                              "igor.ivanov.taganrog@gmail.com"
                                      })
            {
                dispatcher.Push(new AddOrEditAccountCommand
                                    {
                                            Email = email,
                                            Password = "Diaweb1$",
                                            First = email.Split('@')[0],
                                            Last = email.Split('@')[1],
                                            Type = Account.AccountOfType.Patient
                                    });
            }


        }

        #endregion

        #region Disposable

        public void Dispose() { }

        #endregion
    }
}