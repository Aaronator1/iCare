namespace Domain
{
    #region << Using >>

    using System;
    using System.Diagnostics.CodeAnalysis;
    using Incoding.Data;
    using Incoding.Quality;
    using JetBrains.Annotations;

    #endregion

    public class Notification : IncEntityBase
    {
        #region Properties

        public new virtual string Id { get; set; }

        public virtual ActionPlan ActionPlan { get; set; }

        public virtual Monitoring.MonitorOfType Type { get; set; }

        public virtual bool IsMark { get; set; }

        public virtual string Message { get; set; }

        #endregion

        #region Nested classes

        [UsedImplicitly, Obsolete(ObsoleteMessage.ClassNotForDirectUsage, true), ExcludeFromCodeCoverage]
        public class Map : NHibernateEntityMap<Notification>
        {
            ////ncrunch: no coverage start

            #region Constructors

            protected Map()
            {
                IdGenerateByGuid(r => r.Id);
                MapEscaping(r => r.IsMark);
                MapEscaping(r => r.Type).CustomType<Monitoring.MonitorOfType>();
                MapEscaping(r => r.Message);
                DefaultReference(r => r.ActionPlan);
            }

            #endregion

            ////ncrunch: no coverage end        
        }

        #endregion
    }
}