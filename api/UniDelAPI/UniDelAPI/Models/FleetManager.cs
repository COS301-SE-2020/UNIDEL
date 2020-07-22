using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UniDelAPI.Models
{
    [Table("FleetManager")]
    public class FleetManager
    {
        [Column("FleetManagerID")]
        [Key]
        [DatabaseGenerated
            (DatabaseGeneratedOption.Identity)]
        [Required]
        public int FleetManagerID { get; set; }

        [Column("FleetManagerName")]
        [StringLength(255)]
        public string FleetManagerName { get; set; }

        [Column("FleetManagerCellphone")]
        [StringLength(20)]
        public string FleetManagerCellphone { get; set; }

        [Column("UserID")]
        public int UserID { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }
    }
}
