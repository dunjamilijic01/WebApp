using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;

namespace Projekat_WEB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KantinaController : ControllerBase
    {     
        public KantinaContext Context {get; set;} 
         public KantinaController(KantinaContext context)
        {
            Context=context;
        } 

        [Route("DodajKantinu")]
        [HttpPost]
        public async Task<ActionResult> dodajKantinu([FromBody] Kantina kantina)
        {
             if(string.IsNullOrWhiteSpace(kantina.NazivKantine)|| kantina.NazivKantine.Length>50)
            {
                return BadRequest("Nesipravno unet naziv kantine");
            }

            try
            {
                var k= await Context.Kantine.Where(p=>p.NazivKantine==kantina.NazivKantine).FirstOrDefaultAsync();

                if(k==null)
                
                {
                    Context.Kantine.Add(kantina);
                    await Context.SaveChangesAsync();
                    return Ok(kantina);
                }
                else
                {
                    return BadRequest("Kantina vec postoji");
                }
            }
            catch(Exception e) 
            {
                return BadRequest(e.Message);
            }
        }

        [Route("VratiKantinu/{naziv}")]
        [HttpGet]
        public async Task<ActionResult> vratiKantinu(string naziv)
        {
             if(string.IsNullOrWhiteSpace(naziv)||naziv.Length>50)
            {
                return BadRequest("Nesipravno unet naziv kantine");
            }

            try
            {
                var k=await Context.Kantine.Where(p=>p.NazivKantine==naziv).FirstOrDefaultAsync();

                if(k==null)
                {
                    return BadRequest("Kantina sa trazenim imenom ne postoji!!");
                }
                else
                
                {
                    return Ok(k);
                }
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}