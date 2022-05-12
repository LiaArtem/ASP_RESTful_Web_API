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
    public class IsinSecurController : ControllerBase
    {
        private readonly Database_Context _context;
        private readonly String _DataType = "IsinSecur";

        public IsinSecurController(Database_Context context)
        {
            _context = context;
        }

        // POST: api/IsinSecur        
        [HttpPost]
        public async Task PostIsinSecur(IsinSecurParam IsinSecurParam)
        {
            if (_context.BuffImportData == null)
            {
                throw new Exception("Entity set 'Database_Context.BuffImportData' is null.");
            }

            // Считываем данные по курсам
            string _url;
            string _dop_param = "";
            if (IsinSecurParam.Format == "json") { _dop_param = "?json"; }

            _url = "https://bank.gov.ua/depo_securities" + _dop_param;
            
            var rezult = await HttpRead.DownloadFileAsyncString(_url);

            BuffImportData buffImportData = new();
            buffImportData.DataType = _DataType;
            buffImportData.DataValue = rezult;

            _context.BuffImportData.Add(buffImportData);
            await _context.SaveChangesAsync();

            // Разворачиваем данные            
            if (IsinSecurParam.IsExecute != null)
            {
                if ((bool)IsinSecurParam.IsExecute)
                {
                    int p_id = buffImportData.Id;
                    await _context.Database.ExecuteSqlInterpolatedAsync($"EXECUTE [dbo].[exec_add_isin_secur] @p_id = {p_id}");
                }
            }
        }
    }
}
