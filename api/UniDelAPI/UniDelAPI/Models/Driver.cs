using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UniDelAPI.Models
{
    [Table("Driver")]
    public class Driver
    {
        [Column("DriverID")]
        [Key]
        [DatabaseGenerated
            (DatabaseGeneratedOption.Identity)]
        [Required]
        public int DriverID { get; set; }

        [Column("DriverName")]
        [StringLength(255)]
        public string DriverName { get; set; }

        [Column("DriverSurname")]
        [StringLength(255)]
        public string DriverSurname { get; set; }

        [Column("DriverRating")]
        public float DriverRating { get; set; }

        [Column("UserID")]
        public int UserID { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }

        [Column("DriverCellphone")]
        [StringLength(20)]
        public string DriverCellphone { get; set; }
    }
}
