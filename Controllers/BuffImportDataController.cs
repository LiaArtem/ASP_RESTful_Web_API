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
    public class BuffImportDataController : ControllerBase
    {
        private readonly Database_Context _context;

        public BuffImportDataController(Database_Context context)
        {
            _context = context;
        }

        private bool BuffImportDataExists(int id)
        {
            return (_context.BuffImportData?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        // GET: api/BuffImportData
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BuffImportData>>> GetBuffImportData()
        {
          if (_context.BuffImportData == null)
          {
              return NotFound();
          }
            return await _context.BuffImportData.ToListAsync();
        }

        // GET: api/BuffImportData/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BuffImportData>> GetBuffImportData(int id)
        {
          if (_context.BuffImportData == null)
          {
              return NotFound();
          }
            var buffImportData = await _context.BuffImportData.FindAsync(id);

            if (buffImportData == null)
            {
                return NotFound();
            }

            return buffImportData;
        }

        // PUT: api/BuffImportData/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBuffImportData(int id, BuffImportData buffImportData)
        {
            if (id != buffImportData.Id)
            {
                return BadRequest();
            }

            _context.Entry(buffImportData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BuffImportDataExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/BuffImportData        
        [HttpPost]
        public async Task<ActionResult<BuffImportData>> PostBuffImportData(BuffImportData buffImportData)
        {
          if (_context.BuffImportData == null)
          {
              return Problem("Entity set 'Database_Context.BuffImportData' is null.");
          }
            _context.BuffImportData.Add(buffImportData);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBuffImportData", new { id = buffImportData.Id }, buffImportData);
        }

        //// DELETE: api/BuffImportData/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteBuffImportData(int id)
        //{
        //    if (_context.BuffImportData == null)
        //    {
        //        return NotFound();
        //    }
        //    var buffImportData = await _context.BuffImportData.FindAsync(id);
        //    if (buffImportData == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.BuffImportData.Remove(buffImportData);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}
    }
}
