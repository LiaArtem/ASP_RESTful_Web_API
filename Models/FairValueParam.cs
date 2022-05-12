using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP_RESTful_Web_API.Models
{
    public class FairValueParam
    {
        ///<summary>Дата курса (формат поля - yyyyMMdd)</summary>
        [StringLength(8)]
        public string Date { get; set; } = null!;
        ///<summary>Развернуть данные после загрузки</summary>
        public bool? IsExecute { get; set; }
    }
}
