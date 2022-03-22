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
    public class MeniController : ControllerBase
    {     
        public KantinaContext Context {get; set;} 
         public MeniController(KantinaContext context)
        {
            Context=context;
        } 


        [Route("DodajMeni/{dan}/{idJela}/{naziv}/{idKantine}")]
        [HttpPost]
        public async Task<ActionResult> DodajMeni(DayOfWeek dan, int idJela, string naziv,int idKantine)
        {
            if(string.IsNullOrEmpty(naziv))
            {
                return BadRequest("Morate uneti naziv menija");
            }
            if(idJela<0)
            {
                return BadRequest("Nesipravan id jela!!!!");
            }
            if(idKantine<0)
            {
                return BadRequest("Nesipravan id jela!!!!");
            }

            try
            {
                var j=await Context.Jela.FindAsync(idJela);
                var m=await Context.Kantine.FindAsync(idKantine);
                if(j!=null && m!=null)
                {
                    Meni meni=new Meni();
                    meni.Dan=dan;
                    meni.JeloNaMeniju=j;
                    meni.Naziv=naziv;
                    meni.Kantina=m;
                    Context.Meniji.Add(meni);
                    await Context.SaveChangesAsync();
                    return Ok($"Dodat meni ciji je id:{meni.ID}");
                }
                else
                {
                    return BadRequest("Neispravan unos!");
                }
               

            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("IzbrisiMeni/{id}")]
        [HttpDelete]
        public async Task<ActionResult> IzbrisiMeni(int id)
        {
            if(id<0)
            {
                return BadRequest("Nesipravan id!!!!");
            }

            try
            {
                var m=await Context.Meniji.FindAsync(id);
                if(m!=null)
                {
                    Context.Meniji.Remove(m);
                    await Context.SaveChangesAsync();
                    return Ok($"Uspesno izbrisan meni sa id-em {id}");
                }
                else
                {
                    return BadRequest("Ne postoji meni ciji je id prosledjen!");
                }
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("IzmeniMeni/{id}/{dan}/{idJela}/{idKantine}")]
        [HttpPut]
        public async Task<ActionResult> IzmeniMeni(int id,DayOfWeek dan,int idJela,string naziv,int idKantine)
        {
            if(id<0)
            {
                return BadRequest("Nesipravan id!!!!");
            }

            if(idJela<0)
            {
                return BadRequest("Nesipravan id jela!!!!");
            }

            if(idKantine<0)
            {
                return BadRequest("Nesipravan id jela!!!!");
            }

            if(Context.Jela.Where(j=>j.ID==idJela).FirstOrDefault()==null)
            {
                return BadRequest("Jelo sa zadataim id-em ne postoji!!");
            }

             if(Context.Kantine.Where(k=>k.ID==idKantine).FirstOrDefault()==null)
            {
                return BadRequest("Kantina sa zadataim id-em ne postoji!!");
            }

             if(string.IsNullOrEmpty(naziv))
            {
                return BadRequest("Morate uneti naziv menija");
            }

            try
            {
                var m=await Context.Meniji.FindAsync(id);
                if(m!=null)
                {
                    if(Context.Meniji.Where(p=>p.Dan==dan &&p.JeloNaMeniju.ID==idJela&&p.Naziv==naziv&&p.Kantina.ID==idKantine).FirstOrDefault()==null)
                    {
                        m.Dan=dan;
                        m.JeloNaMeniju=Context.Jela.Where(q=>q.ID==idJela).FirstOrDefault();
                        m.Naziv=naziv;
                        m.Kantina=Context.Kantine.Where(q=>q.ID==idKantine).FirstOrDefault();
                        await Context.SaveChangesAsync();
                        return Ok($"Uspesno izmenjen meni ciji je id:{id}");
                    }
                    else
                    {
                        return BadRequest("Meni sa ovakvim podacima vec postoji!!");
                    }
                }
                else
                {
                    return BadRequest("Trazeni meni za izmenu ne postoji!");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }


        }

        [Route("VratiMeni/{dan}/{nazivKantine}")]
        [HttpGet]
        public async Task<ActionResult> VratiJelo(DayOfWeek dan,string nazivKantine)
        {
            
            try
            {
                var m=await Context.Meniji.Where(m=>m.Dan==dan && m.Kantina.NazivKantine==nazivKantine)
                                            .Include(j=>j.JeloNaMeniju)
                                            .ToListAsync();
                if(m!=null)
                {
                    return Ok(
                        m.Select(p=>
                        new
                        {
                            id=p.ID,
                            dan=p.Dan,
                            naziv=p.Naziv,
                            nazivJela=p.JeloNaMeniju.NazivJela,
                            restoran=p.JeloNaMeniju.Restoran,
                            cena=p.JeloNaMeniju.Cena
                        }));
                }
                else
                {
                    return BadRequest("Ne postoji meni sa trazenim id-em!!");
                }
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [Route("VratiJelo/{id}")]
        [HttpGet]
        public  async Task<ActionResult> VratiJeloMenija(int id)
        {
            if(id<0)
            {
                return BadRequest("Nesipravan id!!!!");
            }

            try
            {
                var meni= await Context.Meniji.Where(p=>p.ID==id).Include(p=>p.JeloNaMeniju).FirstOrDefaultAsync();

                if(meni!=null)
                {
                    return Ok(meni.JeloNaMeniju.ID);
                }
                else
                {
                    return BadRequest("Nema menija sa unetim id-em");
                }
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }

}