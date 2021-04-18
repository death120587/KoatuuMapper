using System;
using FluentNHibernate.Mapping;

namespace ProcuctDatabase.Mapping
{
    public class CurrentArchivedAdm
    {
        public virtual long Id { get; set; }
        public virtual string AdmId { get; set; }
        public virtual string Type { get; set; }
        public virtual string Name { get; set; }
        public virtual string FullName { get; set; }
        public virtual string District { get; set; }
        public virtual string Area { get; set; }
        public virtual DateTime DateTo { get; set; }
        public virtual DateTime? UpdatedAt { get; set; }
        public virtual long? UpdatedBy { get; set; }
        public virtual string Rada { get; set; }
    }

    public class CurrentArchivedAdmMapping : ClassMap<CurrentArchivedAdm>
    {
        CurrentArchivedAdmMapping()
        {
            Table("ref_adm_h");

            Id(x => x.Id).Column("id").GeneratedBy.Increment();

            Map(x => x.AdmId).Column("adm_id");
            Map(x => x.Type).Column("type");
            Map(x => x.Name).Column("name");
            Map(x => x.FullName).Column("full_name");
            Map(x => x.District).Column("district");
            Map(x => x.Area).Column("oblast");
            Map(x => x.DateTo).Column("date_to");
            Map(x => x.UpdatedAt).Column("updated_at");
            Map(x => x.UpdatedBy).Column("updated_by");
            Map(x => x.Rada).Column("rada");
        }
    }
}
