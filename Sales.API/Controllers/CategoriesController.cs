using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoresG1.API.Data;
using StoresG1.Shared.Entities;

namespace StoresG1.API.Controllers
{
        [ApiController]
        [Route("/api/categories")]
        public class CategoriesController : ControllerBase
        {
            private readonly DataContext _context;

            public CategoriesController(DataContext context)
            {


                _context = context;
            }


            [HttpPost]
        public async Task<ActionResult> Post(Category category)
            {

                try
                {

                    _context.Add(category);
                    await _context.SaveChangesAsync();


                    return Ok();
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                    {
                        return BadRequest("Ya existe una categoría con el mismo nombre.");
                    }
                    else
                    {
                        return BadRequest(dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    return BadRequest(exception.Message);
                }
            }




            // Get Lista

            [HttpGet]
            public async Task<ActionResult> Get()
            {

                return Ok(await _context.Categories.ToListAsync());

            }




            //Búsqueda por parámetro
            [HttpGet("{id:int}")]  ///api/countries/1
            public async Task<ActionResult> Get(int id)
            {
                var country = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
                if (country is null)
                {
                    return NotFound();
                }

                return Ok(country);
            }

            // Actualización

            [HttpPut]
            public async Task<ActionResult> Put(Category category)
            {

                try
                {

                    _context.Update(category);
                    await _context.SaveChangesAsync();


                    return Ok(category);

                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                    {
                        return BadRequest("Ya existe un registro con el mismo nombre.");
                    }
                    else
                    {
                        return BadRequest(dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    return BadRequest(exception.Message);
                }
            }



            [HttpDelete("{id:int}")]
            public async Task<ActionResult> Delete(int id)
            {
                var afectedRows = await _context.Categories
                    .Where(x => x.Id == id)

                    .ExecuteDeleteAsync();

                if (afectedRows == 0)
                {
                    return NotFound();
                }

                return NoContent();
            }




        }
    }
