namespace Domain
{
    #region << Using >>

    using System;
    using System.Diagnostics.CodeAnalysis;
    using Incoding.Data;
    using Incoding.Quality;
    using JetBrains.Annotations;

    #endregion

    public class Medication : IncEntityBase
    {
        #region Properties

        public new virtual string Id { get; set; }

        public virtual string Name { get; set; }

        public virtual ActionPlan Plan { get; set; }

        public virtual bool Checked { get; set; }

        #endregion

        #region Nested classes

        [UsedImplicitly, Obsolete(ObsoleteMessage.ClassNotForDirectUsage, true), ExcludeFromCodeCoverage]
        public class Map : NHibernateEntityMap<Medication>
        {
            ////ncrunch: no coverage start

            #region Constructors

            protected Map()
            {
                IdGenerateByGuid(r => r.Id);
                MapEscaping(r => r.Name);
                MapEscaping(r => r.Checked);
                DefaultReference(r => r.Plan);
            }

            #endregion

            ////ncrunch: no coverage end        
        }

        #endregion
    }
}