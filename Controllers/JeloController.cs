using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;

namespace Projekat_WEB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JeloController : ControllerBase
    {     
        public KantinaContext Context {get; set;} 
         public JeloController(KantinaContext context)
        {
            Context=context;
        } 

        [Route("DodajJelo")]
        [HttpPost]
        public async Task<ActionResult> DodajJelo([FromBody] Jelo jelo)
        {
            if(jelo.NazivJela.Length>50 || string.IsNullOrEmpty(jelo.NazivJela))
            {
                return BadRequest("Nespravni naziv jela");
            }

            if(jelo.Restoran.Length>50 || string.IsNullOrEmpty(jelo.Restoran))
            {
                return BadRequest("Nespravni naziv restorana");
            }

            if(jelo.Cena<100||jelo.Cena>500)
            {
                return BadRequest("Nedozvoljena cena jela je uneta!");
            }

            try
            {

                var j=Context.Jela.Where(p=>p.NazivJela==jelo.NazivJela && p.Restoran==jelo.Restoran && p.Cena==jelo.Cena).FirstOrDefault();
                if(j==null)
                {
                    Context.Jela.Add(jelo);
                    await Context.SaveChangesAsync();
                    return Ok($"Dodato je jelo ciji je id:{jelo.ID}");
                }
                else
                {
                    return BadRequest("Ovo jelo vec postoji");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [Route("VratiJelo/{id}")]
        [HttpGet]
        public async Task<ActionResult> VratiJelo(int id)
        {
            if(id<0)
            {
                return BadRequest("Unet pogresan id za pretragu");
            }

            try
            {
                var j=await Context.Jela.FindAsync(id);
                if(j!=null)
                {
                    return Ok(j);
                }
                else
                {
                    return BadRequest("Jelo sa trazenim id-em ne postoji!");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("IzmeniJelo/{id}/{naziv}/{restoran}/{cena}")]
        [HttpPut]
        public async Task<ActionResult> IzmeniJelo(int id,string naziv,string restoran,int cena)
        {
            if(id<0)
            {
                return BadRequest("Unet pogresan id za pretragu");
            }

            if(naziv.Length>50 || string.IsNullOrEmpty(naziv))
            {
                return BadRequest("Nespravni naziv jela");
            }

            if(restoran.Length>50 || string.IsNullOrEmpty(restoran))
            {
                return BadRequest("Nespravni naziv restorana");
            }

            if(cena<100||cena>500)
            {
                return BadRequest("Nedozvoljena cena jela je uneta!");
            }
            try
            {
                var j= await Context.Jela.FindAsync(id);
                if(j!=null)
                {
                    j.NazivJela=naziv;
                    j.Restoran=restoran;
                    j.Cena=cena;
                    if(Context.Jela.Where(p=>p.NazivJela==naziv && p.Restoran==restoran &&p.Cena==cena).FirstOrDefault()==null)
                    {
                        await Context.SaveChangesAsync();
                        return Ok($"Uspesno promenjeno jelo sa id-em{j.ID}");
                    }
                    else
                    {
                        return BadRequest("Ovakvo jelo vec postoji!");
                    }

                }
                else
                {
                    return BadRequest("Jelo sa trazenim id-em ne postoji!");
                }
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("IzbrisiJelo/{id}")]
        [HttpDelete]
        public async Task<ActionResult> ObrisiJelo(int id)
        {
            if(id<0)
            {
                return BadRequest("Unet pogresan id za pretragu");
            }

            try
            {
                var j= await Context.Jela.FindAsync(id);
                if(j!=null)
                {
                    Context.Remove(j);
                    await Context.SaveChangesAsync();
                    return Ok($"Jelo ciji je id: {j.ID} je uspesno obrisano!");
                }
                else
                {
                    return BadRequest("Jelo sa zadatim id-em ne postoji");
                }
                    
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}