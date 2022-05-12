using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP_RESTful_Web_API.Models
{
    public class IsinSecurParam
    {        
        [StringLength(4)]
        ///<summary>Формат полученных данных (xml, json)</summary>
        public string Format { get; set; } = null!;     
        ///<summary>Развернуть данные после загрузки</summary>
        public bool? IsExecute { get; set; }
    }
}
