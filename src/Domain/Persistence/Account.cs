namespace Domain
{
    #region << Using >>

    using System;
    using System.Diagnostics.CodeAnalysis;
    using Incoding.Data;
    using Incoding.Quality;
    using JetBrains.Annotations;

    #endregion

    public class Account : IncEntityBase
    {

        #region Properties

        public new virtual string Id { get; set; }

        public virtual string Staff { get; set; }

        public virtual string Email { get; set; }

        public virtual string First { get; set; }

        public virtual string Last { get; set; }

        public virtual string Password { get; set; }

        public virtual AccountOfType Type { get; set; }

        public virtual string Phone { get; set; }

        
        #endregion

        #region Nested classes

        [UsedImplicitly, Obsolete(ObsoleteMessage.ClassNotForDirectUsage, true), ExcludeFromCodeCoverage]
        public class Map : NHibernateEntityMap<Account>
        {
            ////ncrunch: no coverage start

            #region Constructors

            protected Map()
            {
                IdGenerateByGuid(r => r.Id);
                MapEscaping(r => r.Email);
                MapEscaping(r => r.Staff);
                MapEscaping(r => r.Password);
                MapEscaping(r => r.First);
                MapEscaping(r => r.Last);
                MapEscaping(r => r.Phone);
                MapEscaping(r => r.Type).CustomType<AccountOfType>();                
            }

            #endregion

            ////ncrunch: no coverage end        
        }

        #endregion

        #region Enums

        public enum AccountOfType
        {
            Staff,

            Patient
        }

        #endregion
    }
}