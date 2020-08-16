using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UniDelWebApplication.Models
{
    [Table("CallLog")]
    public class CallLog
    {
        [Column("CallID")]
        [Key]
        [DatabaseGenerated
            (DatabaseGeneratedOption.Identity)]
        [Required]
        public int CallID { get; set; }

        [Column("CallDateTime")]
        public DateTime CallDateTime { get; set; }

        [Column("CallNotes")]
        [StringLength(255)]
        public string CallNotes { get; set; }

        [Column("CallReason")]
        [StringLength(255)]
        public string CallReason { get; set; }
    }
}
