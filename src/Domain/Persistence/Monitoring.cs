namespace Domain
{
    #region << Using >>

    using System;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using Incoding.Data;
    using Incoding.Extensions;
    using Incoding.Quality;
    using JetBrains.Annotations;

    #endregion

    public class Monitoring : IncEntityBase
    {
        #region Properties

        public new virtual string Id { get; set; }

        public virtual ActionPlan ActionPlan { get; set; }

        public virtual MonitorOfType Type { get; set; }

        public virtual string TypeName { get { return Type.ToLocalization(); } }

        public virtual int Max { get; set; }

        public virtual int Min { get; set; }

        #endregion

        #region Api Methods

        public virtual bool IsSafety(int value)
        {
            return Max > value && value < Min;
        }

        #endregion

        #region Nested classes

        [UsedImplicitly, Obsolete(ObsoleteMessage.ClassNotForDirectUsage, true), ExcludeFromCodeCoverage]
        public class Map : NHibernateEntityMap<Monitoring>
        {
            ////ncrunch: no coverage start

            #region Constructors

            protected Map()
            {
                IdGenerateByGuid(r => r.Id);
                MapEscaping(r => r.Type).CustomType<MonitorOfType>();
                MapEscaping(r => r.Min);
                MapEscaping(r => r.Max);
                DefaultReference(r => r.ActionPlan);
            }

            #endregion

            ////ncrunch: no coverage end        
        }

        #endregion

        #region Enums

        public enum MonitorOfType
        {
            [Description("Heart Rate")]
            HeartRate,

            Temperature,

            Weight
        }

        #endregion
    }
}