using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WpfAppNeeeeeee.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }

        [Required]
        [StringLength(20)]
        public string Role { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string ContactInfo { get; set; }

        [StringLength(100)]
        public string Specialization { get; set; }

        public virtual ICollection<Schedule> Schedules { get; set; }
        [InverseProperty("Sender")]
        public virtual ICollection<Message> SentMessages { get; set; }
        [InverseProperty("Receiver")]
        public virtual ICollection<Message> ReceivedMessages { get; set; }
    }
} 