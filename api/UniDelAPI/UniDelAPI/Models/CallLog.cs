using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UniDelAPI.Models
{
    [Table("Calllog")]
    public class CallLog
    {
        [Column("CallID")]
        [Key]
        [DatabaseGenerated
            (DatabaseGeneratedOption.Identity)]
        [Required]
        public int CallLogID { get; set; }

        [Column("CallDateTime")]
        [StringLength(255)]
        public string CallDateTime { get; set; }

        [Column("CallReason")]
        [StringLength(255)]
        public string CallReason { get; set; }

        [Column("CallNotes")]
        [StringLength(255)]
        public string CallNotes { get; set; }

    }
}
