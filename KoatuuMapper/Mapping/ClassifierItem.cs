using System;
using FluentNHibernate.Mapping;

namespace KoatuuMapper.Mapping
{
    public class ClassifierItem
    {
        public virtual long Id { get; set; }
        public virtual string Lavel1 { get; set; }
        public virtual string Lavel2 { get; set; }
        public virtual string Lavel3 { get; set; }
        public virtual string Lavel4 { get; set; }
        public virtual string Lavel5 { get; set; }
        public virtual string Classifier { get; set; }
        public virtual char Type { get; set; }
        public virtual string Name { get; set; }
    }

    public class RefAdmMapping : ClassMap<ClassifierItem>
    {
        public RefAdmMapping()
        {
            Table("classifier_adc");

            Id(x => x.Id).Column("id").GeneratedBy.Increment();

            Map(x => x.Name).Column("name");
            Map(x => x.Lavel1).Column("level1").CustomSqlType("VARCHAR(19)");
            Map(x => x.Lavel2).Column("level2").CustomSqlType("VARCHAR(19)");
            Map(x => x.Lavel3).Column("level3").CustomSqlType("VARCHAR(19)");
            Map(x => x.Lavel4).Column("level4").CustomSqlType("VARCHAR(19)");
            Map(x => x.Lavel5).Column("level5").CustomSqlType("VARCHAR(19)");
            Map(x => x.Type).Column("type").CustomSqlType("VARCHAR(1)");
            Map(x => x.Classifier).Column("classifier").CustomSqlType("VARCHAR(19)");
        }
    }
}
