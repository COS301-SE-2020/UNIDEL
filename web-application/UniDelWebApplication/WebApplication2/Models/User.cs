using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UniDelWebApplication.Models
{
    [Table("User")]
    public class User
    {
        [Column("UserID")]
        [Key]
        [DatabaseGenerated
            (DatabaseGeneratedOption.Identity)]
        [Required]
        public int UserID { get; set; }

        [Column("UserEmail")]
        [StringLength(255)]
        public string UserEmail { get; set; }

        [Column("UserPassword")]
        [StringLength(255)]
        public string UserPassword { get; set; }

        [Column("UserProfilePic")]
        public byte[] UserProfilePic { get; set; }

        [Column("UserType")]
        [StringLength(255)]
        public string UserType { get; set; }

        [Column("UserConfirmed")]
        public bool UserConfirmed { get; set; }

        [Column("UserToken")]
        [StringLength(255)]
        public string UserToken { get; set; }
    }
}
