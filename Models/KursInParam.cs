using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP_RESTful_Web_API.Models
{    
    public partial class KursInParam
    {
        ///<summary>Дата курса (формат поля - yyyyMMdd)</summary>
        [StringLength(8)]
        public string Date { get; set; } = null!;
        [StringLength(4)]
        ///<summary>Формат полученных данных (xml, json)</summary>
        public string Format { get; set; } = null!;
        [StringLength(3)]
        ///<summary>Код валюты (UAH, USD, EUR)</summary>
        public string? Currency { get; set; }
        ///<summary>Развернуть данные после загрузки</summary>
        public bool? IsExecute { get; set; }
        
        
    }
}
