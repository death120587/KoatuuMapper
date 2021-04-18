using System;
using System.Collections.Generic;
using System.Text;

namespace KoatuuMapper.Models
{
    public class AdmItem
    {
        public long Uid { get; set; }
        public string Koatuu { get; set; }
        public string Classifier { get; set; }
        public string Lavel1 { get; set; }
        public string Lavel1Name { get; set; }
        public string Lavel1Prefix { get; set; }
        public string Lavel1Suffix { get; set; }
        public bool Lavel1Included { get; set; }
        public string Lavel2 { get; set; }
        public string Lavel2Name { get; set; }
        public string Lavel2Prefix { get; set; }
        public string Lavel2Suffix { get; set; }
        public bool Lavel2Included { get; set; }
        public string Lavel3 { get; set; }
        public string Lavel3Name { get; set; }
        public string Lavel3Prefix { get; set; }
        public string Lavel3Suffix { get; set; }
        public bool Lavel3Included { get; set; }
        public string Lavel4 { get; set; }
        public string Lavel4Name { get; set; }
        public string Lavel4Prefix { get; set; }
        public string Lavel4Suffix { get; set; }
        public bool Lavel4Included { get; set; }
        public string Lavel5 { get; set; }
        public string Lavel5Name { get; set; }
        public string Lavel5Prefix { get; set; }
        public string Lavel5Suffix { get; set; }
        public bool Lavel5Included { get; set; }
        public bool Selectable { get; set; }
        public string FullName { get; set; }
        public string Name { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public bool IsMy { get; set; }
        public DateTime? Created_At { get; set; }
        public long? Created_By { get; set; }
        public DateTime? Updated_At { get; set; }
        public long? Updated_By { get; set; }
        public string District { get; set; }
        public string Area { get; set; }
        public string Type { get; set; }
        public bool IsArchived { get; set; }
    }
}
