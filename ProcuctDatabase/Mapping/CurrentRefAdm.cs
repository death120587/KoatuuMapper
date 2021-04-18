using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcuctDatabase.Mapping
{
    public class CurrentRefAdm
    {
        public virtual string Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Type { get; set; }
        public virtual char Flag3 { get; set; }
        public virtual char Flag6 { get; set; }
        public virtual bool Selectable { get; set; }
        public virtual bool My { get; set; }
        public virtual string FullName { get; set; }
        public virtual DateTime? UpdatedAt { get; set; }
        public virtual long? UpdatedBy { get; set; }
        public virtual string District { get; set; }
        public virtual string Area { get; set; }
        public virtual string Rada { get; set; }
    }

    public class CurrentRefAdmMapping : ClassMap<CurrentRefAdm>
    {
        CurrentRefAdmMapping()
        {
            Table("ref_adm");

            Id(x => x.Id).Column("id");

            Map(x => x.Name).Column("name");
            Map(x => x.Type).Column("type");
            Map(x => x.Flag3).Column("flag3");
            Map(x => x.Flag6).Column("flag6");
            Map(x => x.Selectable).Column("selectable");
            Map(x => x.My).Column("my");
            Map(x => x.FullName).Column("full_name");
            Map(x => x.UpdatedAt).Column("updated_at");
            Map(x => x.UpdatedBy).Column("updated_by");
            Map(x => x.District).Column("district");
            Map(x => x.Area).Column("oblast");
            Map(x => x.Rada).Column("rada");
        }
    }
}
