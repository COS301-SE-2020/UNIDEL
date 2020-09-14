using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UniDelWebApplication.Models
{
    [Table("CompanyCall")]
    public class CompanyCall
    {
        [Column("CompanyCallID")]
        [Key]
        [DatabaseGenerated
            (DatabaseGeneratedOption.Identity)]
        [Required]
        public int CompanyCallID { get; set; }

        [ForeignKey("CourierCompanyID")]
        public int CourierCompanyID { get; set; }

        [Column("CallID")]
        public int CallID { get; set; }

        [ForeignKey("CallID")]
        public CallLog Call { get; set; }
    }
}
