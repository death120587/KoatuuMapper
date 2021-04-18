using System;
using FluentNHibernate.Mapping;
using Newtonsoft.Json;

namespace SqlLiteDatabase.Mapping
{
    public class RefAdc
    {
        [JsonIgnore]
        public virtual long Id { get; set; }
        
        [JsonProperty("uid")]
        public virtual long Uid { get; set; }
        
        [JsonProperty("classifier")]
        public virtual string Classifier { get; set; }
        
        [JsonProperty("level1")]
        public virtual string Lavel1 { get; set; }
        
        [JsonProperty("level1name")]
        public virtual string Lavel1Name { get; set; }
        
        [JsonProperty("level1prefix")]
        public virtual string Lavel1Prefix { get; set; }
        
        [JsonProperty("level1suffix")]
        public virtual string Lavel1Suffix { get; set; }
        
        [JsonProperty("level1included")]
        public virtual bool Lavel1Included { get; set; }
        
        [JsonProperty("level2")]
        public virtual string Lavel2 { get; set; }
        
        [JsonProperty("level2name")]
        public virtual string Lavel2Name { get; set; }
        
        [JsonProperty("level2prefix")]
        public virtual string Lavel2Prefix { get; set; }
        
        [JsonProperty("level2suffix")]
        public virtual string Lavel2Suffix { get; set; }
        
        [JsonProperty("level2included")]
        public virtual bool Lavel2Included { get; set; }
        
        [JsonProperty("level3")]
        public virtual string Lavel3 { get; set; }
        
        [JsonProperty("level3name")]
        public virtual string Lavel3Name { get; set; }
        
        [JsonProperty("level3prefix")]
        public virtual string Lavel3Prefix { get; set; }
        
        [JsonProperty("level3suffix")]
        public virtual string Lavel3Suffix { get; set; }
        
        [JsonProperty("level3included")]
        public virtual bool Lavel3Included { get; set; }
        
        [JsonProperty("level4")]
        public virtual string Lavel4 { get; set; }
        
        [JsonProperty("level4name")]
        public virtual string Lavel4Name { get; set; }
        
        [JsonProperty("level4prefix")]
        public virtual string Lavel4Prefix { get; set; }
        
        [JsonProperty("level4suffix")]
        public virtual string Lavel4Suffix { get; set; }

        [JsonProperty("level4included")]
        public virtual bool Lavel4Included { get; set; }

        [JsonProperty("level5")]
        public virtual string Lavel5 { get; set; }

        [JsonProperty("level5name")]
        public virtual string Lavel5Name { get; set; }

        [JsonProperty("level5prefix")]
        public virtual string Lavel5Prefix { get; set; }

        [JsonProperty("level5suffix")]
        public virtual string Lavel5Suffix { get; set; }

        [JsonProperty("level5included")]
        public virtual bool Lavel5Included { get; set; }

        [JsonProperty("selectable")]
        public virtual bool Selectable { get; set; }

        [JsonProperty("full_name")]
        public virtual string FullName { get; set; }

        [JsonProperty("name")]
        public virtual string Name { get; set; }

        [JsonProperty("date_start")]
        public virtual DateTime DateStart { get; set; }

        [JsonProperty("date_end")]
        public virtual DateTime? DateEnd { get; set; }

        [JsonProperty("my")]
        public virtual bool IsMy { get; set; }

        [JsonProperty("created_at")]
        public virtual DateTime Created_At { get; set; }

        [JsonProperty("created_by")]
        public virtual long Created_By { get; set; }

        [JsonProperty("updated_at")]
        public virtual DateTime? Updated_At { get; set; }

        [JsonProperty("updated_by")]
        public virtual long? Updated_By { get; set; }

        [JsonProperty("type")]
        public virtual string Type { get; set; }
    }

    public class RefAdcMapping : ClassMap<RefAdc>
    {
        public RefAdcMapping()
        {
            Table("ref_adc");

            Id(x => x.Id).Column("id").GeneratedBy.Increment();

            Map(x => x.Uid).Column("uid");
            Map(x => x.Name).Column("name");
            Map(x => x.FullName).Column("fullname");
            Map(x => x.Classifier).Column("classifier").CustomSqlType("VARCHAR(29)");
            Map(x => x.Lavel1).Column("level1").CustomSqlType("VARCHAR(19)");
            Map(x => x.Lavel1Name).Column("level1name");
            Map(x => x.Lavel1Prefix).Column("level1prefix");
            Map(x => x.Lavel1Suffix).Column("level1suffix");
            Map(x => x.Lavel1Included).Column("level1included").CustomSqlType("BOOLEAN"); ;
            Map(x => x.Lavel2).Column("level2").CustomSqlType("VARCHAR(19)");
            Map(x => x.Lavel2Name).Column("level2name");
            Map(x => x.Lavel2Prefix).Column("level2prefix");
            Map(x => x.Lavel2Suffix).Column("level2suffix");
            Map(x => x.Lavel2Included).Column("level2included").CustomSqlType("BOOLEAN"); ;
            Map(x => x.Lavel3).Column("level3").CustomSqlType("VARCHAR(19)");
            Map(x => x.Lavel3Name).Column("level3name");
            Map(x => x.Lavel3Prefix).Column("level3prefix");
            Map(x => x.Lavel3Suffix).Column("level3suffix");
            Map(x => x.Lavel3Included).Column("level3included").CustomSqlType("BOOLEAN"); ;
            Map(x => x.Lavel4).Column("level4").CustomSqlType("VARCHAR(19)");
            Map(x => x.Lavel4Name).Column("level4name");
            Map(x => x.Lavel4Prefix).Column("level4prefix");
            Map(x => x.Lavel4Suffix).Column("level4suffix");
            Map(x => x.Lavel4Included).Column("level4included").CustomSqlType("BOOLEAN"); ;
            Map(x => x.Lavel5).Column("level5").CustomSqlType("VARCHAR(19)");
            Map(x => x.Lavel5Name).Column("level5name");
            Map(x => x.Lavel5Prefix).Column("level5prefix");
            Map(x => x.Lavel5Suffix).Column("level5suffix");
            Map(x => x.Lavel5Included).Column("level5included").CustomSqlType("BOOLEAN"); ;
            Map(x => x.Selectable).Column("selectable").CustomSqlType("BOOLEAN");
            Map(x => x.DateStart).Column("date_start").CustomSqlType("DATE");
            Map(x => x.DateEnd).Column("date_end").CustomSqlType("DATE");
            Map(x => x.IsMy).Column("my").CustomSqlType("BOOLEAN");
            Map(x => x.Created_At).Column("created_at").CustomSqlType("DATETIME");
            Map(x => x.Created_By).Column("created_by");
            Map(x => x.Updated_At).Column("updated_at").CustomSqlType("DATETIME");
            Map(x => x.Updated_By).Column("updated_by");
            Map(x => x.Type).Column("type");
        }
    }
}
