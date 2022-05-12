using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ASP_RESTful_Web_API.Models
{
    [Table("BUFF_IMPORT_DATA")]
    public partial class BuffImportData
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("DATA_TYPE")]
        [StringLength(255)]
        public string DataType { get; set; } = null!;
        [Column("DATA_VALUE")]
        public string? DataValue { get; set; }
        [Column("SYS_DATE")]
        public DateTime SysDate { get; set; }
        [Column("IS_ERROR")]
        [StringLength(1)]
        public string? IsError { get; set; }
        [Column("ERROR_MESSAGE")]
        public string? ErrorMessage { get; set; }
    }
}
