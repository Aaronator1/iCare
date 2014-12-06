namespace Domain
{
    #region << Using >>

    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Incoding.Data;
    using Incoding.Quality;
    using JetBrains.Annotations;

    #endregion

    public class ActionPlan : IncEntityBase
    {
        #region Properties

        public new virtual string Id { get; set; }

        public virtual DateTime CreateDt { get; set; }

        public virtual Account Patient { get; set; }

        public virtual IList<AnswerOnReminder> Reminders { get; set; }

        public virtual IList<Monitoring> Monitorings { get; set; }

        
        #endregion

        #region Nested classes

        [UsedImplicitly, Obsolete(ObsoleteMessage.ClassNotForDirectUsage, true), ExcludeFromCodeCoverage]
        public class Map : NHibernateEntityMap<ActionPlan>
        {
            ////ncrunch: no coverage start

            #region Constructors

            protected Map()
            {
                IdGenerateByGuid(r => r.Id);
                MapEscaping(r => r.CreateDt);
                HasMany(r => r.Reminders).Cascade.AllDeleteOrphan().LazyLoad();
                HasMany(r => r.Monitorings).Cascade.AllDeleteOrphan().LazyLoad();                
                DefaultReference(r => r.Patient);
            }

            #endregion

            ////ncrunch: no coverage end        
        }

        #endregion
    }
}