using System.ComponentModel.DataAnnotations;

namespace SignUpMeeting.Models
{
    public class Attendees
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        [Required]
        public bool IsSpeaker { get; set; }
    }
}
