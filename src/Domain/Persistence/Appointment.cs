namespace Domain
{
    #region << Using >>

    using System;
    using System.Diagnostics.CodeAnalysis;
    using Incoding.Data;
    using Incoding.Quality;
    using JetBrains.Annotations;

    #endregion

    public class Appointment : IncEntityBase
    {
        #region Properties

        public virtual string Staff { get; set; }

        public new virtual string Id { get; set; }

        public virtual DateTime Date { get; set; }

        public virtual ActionPlan ActionPlan { get; set; }

        public virtual string Address { get; set; }

        public virtual string Address1 { get; set; }

        public virtual string Address2 { get; set; }

        public virtual string State { get; set; }

        public virtual string Phone { get; set; }

        public virtual string Name { get; set; }

        public virtual bool Checked { get; set; }

        public virtual bool IsCarePlan { get; set; }

        #endregion

        #region Nested classes

        [UsedImplicitly, Obsolete(ObsoleteMessage.ClassNotForDirectUsage, true), ExcludeFromCodeCoverage]
        public class Map : NHibernateEntityMap<Appointment>
        {
            ////ncrunch: no coverage start

            #region Constructors

            protected Map()
            {
                IdGenerateByGuid(r => r.Id);
                MapEscaping(r => r.Date);
                MapEscaping(r => r.IsCarePlan);
                MapEscaping(r => r.Checked);
                MapEscaping(r => r.Address);
                MapEscaping(r => r.Address1);
                MapEscaping(r => r.Address2);
                MapEscaping(r => r.State);
                MapEscaping(r => r.Phone);
                MapEscaping(r => r.Name);
                MapEscaping(r => r.Staff);
                DefaultReference(r => r.ActionPlan);
            }

            #endregion

            ////ncrunch: no coverage end        
        }

        #endregion
    }
}