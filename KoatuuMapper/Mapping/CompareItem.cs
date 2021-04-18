using FluentNHibernate.Mapping;

namespace KoatuuMapper.Mapping
{
    public class CompareItem
    {
        public virtual long Id { get; set; }
        public virtual string Сlassifier { get; set; }
        public virtual string Koatuu { get; set; }
        public virtual char Type { get; set; }
        public virtual string Name { get; set; }
    }

    public class CompareItemMapping : ClassMap<CompareItem>
    {
        public CompareItemMapping()
        {
            Table("compare_adc");

            Id(x => x.Id).Column("id").GeneratedBy.Increment();

            Map(x => x.Сlassifier).Column("сlassifier");
            Map(x => x.Koatuu).Column("koatuu");
            Map(x => x.Type).Column("type").CustomSqlType("varchar(1)");
            Map(x => x.Name).Column("name");
        }
    }
}
