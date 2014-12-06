namespace Domain
{
    #region << Using >>

    using System;
    using System.Diagnostics.CodeAnalysis;
    using Incoding.Data;
    using Incoding.Quality;
    using JetBrains.Annotations;

    #endregion

    public class ValueOnMonitoring : IncEntityBase
    {
        #region Properties

        public new virtual string Id { get; set; }

        public virtual Monitoring.MonitorOfType Monitoring { get; set; }

        public virtual ActionPlan ActionPlan { get; set; }

        public virtual int Value { get; set; }

        #endregion

        #region Nested classes

        [UsedImplicitly, Obsolete(ObsoleteMessage.ClassNotForDirectUsage, true), ExcludeFromCodeCoverage]
        public class Map : NHibernateEntityMap<ValueOnMonitoring>
        {
            ////ncrunch: no coverage start

            #region Constructors

            protected Map()
            {
                IdGenerateByGuid(r => r.Id);
                MapEscaping(r => r.Monitoring).CustomType<Monitoring.MonitorOfType>();
                DefaultReference(r => r.ActionPlan);
                MapEscaping(r => r.Value);
            }

            #endregion

            ////ncrunch: no coverage end        
        }

        #endregion
    }
}