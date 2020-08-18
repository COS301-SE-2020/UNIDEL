using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UniDelAPI.Models
{
    [Table("Client")]
    public class Client
    {
        [Column("ClientID")]
        [Key]
        [DatabaseGenerated
            (DatabaseGeneratedOption.Identity)]
        [Required]
        public int ClientID { get; set; }

        [Column("ClientName")]
        [StringLength(255)]
        public string ClientName { get; set; }

        [Column("ClientTelephone")]
        [StringLength(255)]
        public string ClientTelephone { get; set; }

        [Column("ClientAddress")]
        [StringLength(255)]
        public string ClientAddress { get; set; }

        [Column("UserID")]
        public int UserID { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }
    }
}
