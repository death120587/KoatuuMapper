namespace KoatuuMapper.Models
{
    /**
     *  Level1 - Region 
     *  Level2 - District
     *  Level3 - Rada
     */
    public class AdmLevelNames
    {
        public AdmLevel Level1 { get; set; }
        public AdmLevel Level2 { get; set; }
        public AdmLevel Level3 { get; set; }
    }

    public class AdmLevel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
