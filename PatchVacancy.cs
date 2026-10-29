namespace pract6.Models
{
    public partial class PatchVacancy
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Company { get; set; } = null!;

        public decimal Salary { get; set; }

        public string City { get; set; } = null!;

        public bool? IsActive { get; set; }
    }
}
