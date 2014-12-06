namespace Domain
{
    #region << Using >>

    using System;
    using System.Diagnostics.CodeAnalysis;
    using Incoding.Data;
    using Incoding.Quality;
    using JetBrains.Annotations;

    #endregion

    public class AnswerOnReminder : IncEntityBase
    {
        #region Properties

        public new virtual string Id { get; set; }

        public virtual ActionPlan ActionPlan { get; set; }

        public virtual bool Value { get; set; }

        public virtual string Title { get; set; }

        #endregion

        #region Nested classes

        [UsedImplicitly, Obsolete(ObsoleteMessage.ClassNotForDirectUsage, true), ExcludeFromCodeCoverage]
        public class Map : NHibernateEntityMap<AnswerOnReminder>
        {
            ////ncrunch: no coverage start

            #region Constructors

            protected Map()
            {
                IdGenerateByGuid(r => r.Id);
                MapEscaping(r => r.Value);
                MapEscaping(r => r.Title);
                DefaultReference(r => r.ActionPlan);
            }

            #endregion

            ////ncrunch: no coverage end        
        }

        #endregion
    }
}