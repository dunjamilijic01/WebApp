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
    public class RadnikController : ControllerBase
    {     
        public KantinaContext Context {get; set;} 
         public RadnikController(KantinaContext context)
        {
            Context=context;
        } 

        [Route("DodajRadnika")]
        [HttpPost]
        public async Task<ActionResult> DodajRadnika([FromBody] Radnik radnik)
        {
            if(radnik.JMBG.Length!=13 || string.IsNullOrWhiteSpace(radnik.JMBG))
            {
                return BadRequest("Pogresan jmbg");
            }

            if(string.IsNullOrWhiteSpace(radnik.Ime)|| radnik.Ime.Length>20)
            {
                return BadRequest("Nesipravno ime");
            }

             if(string.IsNullOrWhiteSpace(radnik.Prezime)|| radnik.Prezime.Length>50)
            {
                return BadRequest("Nesipravno prezime");
            }

            try
            {
                var r=Context.Radnici.Where(p=>p.JMBG==radnik.JMBG).FirstOrDefault();
                if(r==null)
                {
                    Context.Radnici.Add(radnik);
                    await Context.SaveChangesAsync();
                    return Ok($"Dodat je radnik ciji je id:{radnik.ID}");
                }
                else 
                {
                    return BadRequest("Ovaj radnik vec postoji!");
                }
                
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
       

        }

        [Route("IzmeniRadnika/{jmbg}/{ime}/{prezime}/{idKantine}")]
        [HttpPut]
        public async Task<ActionResult> IzmeniRadnika(string jmbg,string ime, string prezime,int idKantine)
        {
            if(jmbg.Length!=13 || string.IsNullOrWhiteSpace(jmbg))
            {
                return BadRequest("Pogresan jmbg");
            }   

            if(string.IsNullOrWhiteSpace(ime)|| ime.Length>20)
            {
                return BadRequest("Nesipravno ime");
            }

             if(string.IsNullOrWhiteSpace(prezime)|| prezime.Length>50)
            {
                return BadRequest("Nesipravno prezime");
            }

            if(Context.Kantine.FindAsync(idKantine)==null)
            {
                return BadRequest("Ne psotoji kantina sa unetim id-em");
            }

        try
        {
            var r = Context.Radnici.Where(p=>p.JMBG==jmbg).FirstOrDefault();

            if(r!=null)
            {
                r.Ime=ime;
                r.Prezime=prezime;
                r.kantina=await Context.Kantine.FindAsync(idKantine);

                await Context.SaveChangesAsync();
                return Ok($"Uspesno je izmenjen radnik ciji je Id:{r.ID}");

            }
            else
            {
                return BadRequest("Student sa trazenim jmbg-om nije pronadjen!!");
            }
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);

        }
    }

    [Route("IzbrisiRadnika/{jmbg}")]
    [HttpDelete]
    public async Task<ActionResult> IzbrisiRadnika(string jmbg)
    {
        if(jmbg.Length!=13 || string.IsNullOrWhiteSpace(jmbg))
            {
                return BadRequest("Pogresan jmbg");
            }   

        try
        {
            var r=Context.Radnici.Where(p=>p.JMBG==jmbg).FirstOrDefault();
            if(r!=null)
            {
                Context.Radnici.Remove(r);
                await Context.SaveChangesAsync();
                return Ok($"Uspesno obrisan radnik ciji je id:{r.ID}");
            }
            else
            {
                return BadRequest("Nije pronadjen trazeni radnik!");
            }
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Route("VratiRadnika/{jmbg}/{naziv}")]
    [HttpGet]
    public async Task<ActionResult> VratiRadnika(string jmbg,string naziv)
    {
        if(jmbg.Length!=13 || string.IsNullOrWhiteSpace(jmbg))
            {
                return BadRequest("Pogresan jmbg");
            }  
         if(string.IsNullOrWhiteSpace(naziv))
            {
                return BadRequest("Pogresan naziv");
            }  
        try
        {
            var r= await Context.Radnici.Include(p =>p.kantina)/*.Where(p=>p.JMBG==jmbg)*/
                                    /*.Include(p=>p.Porudzbine)/*.FirstOrDefault();*/
                                    /*.ThenInclude(q=>q.ID)
                                    .Include(p=>p.Porudzbine)
                                    .ThenInclude(q=>q.PreuzetaPorudzbina)
                                    .Include(p=>p.Porudzbine)
                                    .ThenInclude(q=>q.Datum)
                                    .Include(p=>p.Porudzbine)*/
                                    /*.ThenInclude(q=>q.JeloPorudzbine)*/
                                    .Where(p=>p.JMBG==jmbg).Where(p=>p.kantina.NazivKantine==naziv).
                                    FirstOrDefaultAsync();

            //var radnik=await r.Where(p=>p.JMBG==jmbg).ToListAsync();

            if(r!=null)
            {

                return Ok(r);
            }
            else
            {
                return BadRequest("Nije pronadjen trazeni radnik!");
            }
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Route("VratiPorudzbineRadnika/{jmbg}/{preuzeta}")]
    [HttpGet]
    public async Task<ActionResult> VratiPorudzbine(string jmbg,int preuzeta)
    {
        if(jmbg.Length!=13 || string.IsNullOrWhiteSpace(jmbg))
            {
                return BadRequest("Pogresan jmbg");
            }  
        if(preuzeta<1 && preuzeta>2)
        {
            return BadRequest("Neispravan podatak preuzeta");
        }
            try
            {
                bool param;
                if(preuzeta==1)
                    param=false;
                else
                    param=true;
                var porudzbine=await Context.Radnici
                                    .Include(p=>p.Porudzbine.Where(q=>q.PreuzetaPorudzbina==param))
                                    .ThenInclude(q=>q.JeloPorudzbine)
                                    .Where(p=>p.JMBG==jmbg).ToListAsync();
                if(porudzbine!=null)
                {

                    return Ok
                    (
                        
                        porudzbine.Select(p=>
                        new
                        {
                            porudzbineRadnika=p.Porudzbine.Select(q=>
                            new{
                                id=q.ID,
                                NazivJela=q.JeloPorudzbine.NazivJela,
                                Restoran=q.JeloPorudzbine.Restoran,
                                Cena=q.JeloPorudzbine.Cena,
                                Datum=q.Datum,
                                Preuzeta=q.PreuzetaPorudzbina
                            })

                    }));
                }
                else
                {
                    return BadRequest("Ne postoji radnik sa trazenim jmbg-om");
                }

            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
    }

    [Route("VratiSvePorudzbineRadnika/{jmbg}")]
    [HttpGet]
    public async Task<ActionResult> VratiSvePorudzbine(string jmbg)
    {
        if(jmbg.Length!=13 || string.IsNullOrWhiteSpace(jmbg))
            {
                return BadRequest("Pogresan jmbg");
            }  
       
            try
            {
                
                var porudzbine=await Context.Radnici
                                    .Include(p=>p.Porudzbine)
                                    .ThenInclude(q=>q.JeloPorudzbine)
                                    .Where(p=>p.JMBG==jmbg).ToListAsync();
                if(porudzbine!=null)
                {

                    return Ok
                    (
                        
                        porudzbine.Select(p=>
                        new
                        {
                            porudzbineRadnika=p.Porudzbine.Select(q=>
                            new{
                                id=q.ID,
                                NazivJela=q.JeloPorudzbine.NazivJela,
                                Restoran=q.JeloPorudzbine.Restoran,
                                Cena=q.JeloPorudzbine.Cena,
                                Datum=q.Datum,
                                Preuzeta=q.PreuzetaPorudzbina
                            })

                    }));
                }
                else
                {
                    return BadRequest("Ne postoji radnik sa trazenim jmbg-om");
                }

            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
    }

    [Route("VratiRadnikeKantine/{kantina}")]
    [HttpGet]
     public async Task<ActionResult> radniciKantine(string kantina)
     {
         if(string.IsNullOrWhiteSpace(kantina))
         return BadRequest("Nesipravno ime kantine!!");

         try
         {
             var k= Context.Kantine.Where(p=>p.NazivKantine==kantina).FirstOrDefault();
             if(k==null)
             {
                 return BadRequest("Ne postoji trazena kantina!!");
             }
             else
             {
                 var r= await Context.Radnici.Where(q=>q.kantina==k).Include(p=>p.Porudzbine)
                                                .ThenInclude(q=>q.JeloPorudzbine)
                                                .ToListAsync();

                return Ok(r);
             }
         }
         catch(Exception e)
         {
             return BadRequest(e.Message);
         }
     }
   

    }
}
