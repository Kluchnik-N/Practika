using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WpfAppNeeeeeee.Models
{
    [Table("Schedule")]
    public class Schedule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [StringLength(255)]
        public string Description { get; set; }

        [Required]
        public DateTime StartDateTime { get; set; }

        [StringLength(100)]
        public string Location { get; set; }

        [Required]
        public int TeacherID { get; set; }

        [ForeignKey("TeacherID")]
        public virtual User Teacher { get; set; }
    }
} 