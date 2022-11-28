using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASP_RESTful_Web_API.Models;

namespace ASP_RESTful_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KursInController : ControllerBase
    {
        private readonly Database_Context _context;
        private readonly String _DataType = "KursIn";

        public KursInController(Database_Context context)
        {
            _context = context;
        }

        // POST: api/KursIn        
        [HttpPost]
        public async Task PostKursIn(KursInParam kursInParam)
        {
            if (_context.BuffImportData == null)
            {
                throw new Exception("Entity set 'Database_Context.BuffImportData' is null.");
            }

            // Считываем данные по курсам
            string _url;
            string _dop_param = "";
            if (kursInParam.Format == "json") { _dop_param = "&json"; }

            if (kursInParam.Currency == null) {
                _url = "https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?date=" + kursInParam.Date + _dop_param;
            }
            else {
                _url = "https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?valcode=" + kursInParam.Currency + "&date=" + kursInParam.Date + _dop_param;
            }           
            var rezult = await HttpRead.DownloadFileAsyncString(_url);

            BuffImportData buffImportData = new()
            {
                DataType = _DataType,
                DataValue = rezult
            };

            _context.BuffImportData.Add(buffImportData);
            await _context.SaveChangesAsync();

            // Разворачиваем данные            
            if (kursInParam.IsExecute != null)
            {
                if ((bool)kursInParam.IsExecute)
                {
                    int p_id = buffImportData.Id;
                    await _context.Database.ExecuteSqlInterpolatedAsync($"EXECUTE [dbo].[exec_add_kurs_nbu] @p_id = {p_id}");                    
                }
            }                                    
        }        
    }
}
