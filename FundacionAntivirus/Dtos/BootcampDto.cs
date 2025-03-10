namespace FundacionAntivirus.Dtos
{
    public class BootcampDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Information { get; set; }
        public string Costs { get; set; }
        public int InstitutionId { get; set; }
    }
    public class BootcampCreateDto{
        public string Name { get; set; }
        public string Description { get; set; }
        public string Information { get; set; }
        public string Costs { get; set; }
        public int InstitutionId { get; set; }
    }
}