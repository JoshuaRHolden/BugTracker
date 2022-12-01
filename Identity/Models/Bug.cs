using System.ComponentModel.DataAnnotations;

namespace AppData.Models
{
    public record Bug
    {
        [Key]
        public int? Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; } = DateTime.Now;
        public string? CreatedBy { get; set; }

        public String AssignedTo { get; set; }

        public BugStatus? BugStatus { get; set; }
        public Priority? PriorityStatus { get; set; }

        public Bug()
        {
            CreatedBy = string.Empty;
        }

    }
}
