using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASP_RESTful_Web_API.Models;

namespace ASP_RESTful_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FairValueController : ControllerBase
    {
        private readonly Database_Context _context;
        private readonly String _DataType = "FairValue";

        public FairValueController(Database_Context context)
        {
            _context = context;
        }

        // POST: api/FairValue        
        [HttpPost]
        public async Task PostFairValue(FairValueParam FairValueParam)
        {
            if (_context.BuffImportData == null)
            {
                throw new Exception("Entity set 'Database_Context.BuffImportData' is null.");
            }

            // Считываем данные по курсам
            string _url;                        
            _url = "https://bank.gov.ua/files/Fair_value/" + FairValueParam.Date[..6] + "/" + FairValueParam.Date + "_fv.txt";

            var rezult = await HttpRead.DownloadFileAsyncString(_url, "windows-1251");

            BuffImportData buffImportData = new();
            buffImportData.DataType = _DataType;
            buffImportData.DataValue = rezult;

            _context.BuffImportData.Add(buffImportData);
            await _context.SaveChangesAsync();

            // Разворачиваем данные            
            if (FairValueParam.IsExecute != null)
            {
                if ((bool)FairValueParam.IsExecute)
                {
                    int p_id = buffImportData.Id;
                    await _context.Database.ExecuteSqlInterpolatedAsync($"EXECUTE [dbo].[exec_add_fair_value] @p_id = {p_id}");
                }
            }
        }
    }
}
