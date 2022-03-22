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
    public class PorudzbinaController : ControllerBase
    {     
        public KantinaContext Context {get; set;} 
         public PorudzbinaController(KantinaContext context)
        {
            Context=context;
        } 

        [Route("DodajPorudzbinu/{preuzeta}/{datum}/{idRadnika}/{idJela}")]
        [HttpPost]
        public async Task<ActionResult> DodajPorudzbinu(bool preuzeta,DateTime datum,int idRadnika,int idJela)
        {
            
            if(idRadnika<0 || await Context.Radnici.FindAsync(idRadnika)==null)
            {
                return BadRequest("POgresan id radnika!!");
            }

            if(idJela<0 || await Context.Jela.FindAsync(idJela)==null)
            {
                return BadRequest("Pogresan id jela");
            }
            try
            {
                var pom=await Context.Porudzbine.Where(p=>p.Datum==datum && p.PorudzbinaZa.ID==idRadnika).FirstOrDefaultAsync();
                if(pom!=null)
                {
                    return BadRequest("Porudzbina za ovaj dan je vec napravljena!");

                }
                else
                {
                    var r=await Context.Radnici.FindAsync(idRadnika);
                    var j=await Context.Jela.FindAsync(idJela);
                    Porudzbina p=new Porudzbina();
                    p.PreuzetaPorudzbina=preuzeta;
                    p.Datum=datum;
                    p.PorudzbinaZa=r;
                    p.JeloPorudzbine=j;
                    Context.Porudzbine.Add(p);
                    await Context.SaveChangesAsync();
                    return Ok($"Uspesno dodata porudzbina ciji je id:{p.ID}");
                }
                
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [Route("IzbrisiPorudzbinu/{id}")]
        [HttpDelete]
        public async Task<ActionResult> IzbrisiPorudzbinu(int id)
        {
            if(id<0)
            {
                return BadRequest("Pogresan id!!");
            }

            try
            {
                var p=await Context.Porudzbine.FindAsync(id);
                if(p!=null)
                {
                    Context.Porudzbine.Remove(p);
                    await Context.SaveChangesAsync();
                    return Ok($"Uspesno izbrisana porudzbina ciji je id: {id}");
                }
                else
                {
                    return BadRequest($"Ne postoji porudzbina ciji je id: {id}");
                }
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
                
        }

        [Route("IzmeniPorudzbinu/{id}/{preuzeta}/{datum}/{idRadnika}/{idJela}")]
        [HttpPut]
        public async Task<ActionResult> IzmeniPorudzbinu(int id, bool preuzeta,DateTime datum, int idRadnika,int idJela)
        {
            if(id<0)
            {
                return BadRequest("Pogresan id porudzbine!!!");
            }

            if(idRadnika<0 || await Context.Radnici.FindAsync(idRadnika)==null)
            {
                return BadRequest("POgresan id radnika!!");
            }

            if(idJela<0 || await Context.Jela.FindAsync(idJela)==null)
            {
                return BadRequest("Pogresan id jela");
            }

            try
            {
                var r=await Context.Porudzbine.FindAsync(id);

                if(r!=null)
                {
                    if(Context.Porudzbine.Where(m=>m.Datum==datum && m.PreuzetaPorudzbina==preuzeta && m.PorudzbinaZa.ID==idRadnika && m.JeloPorudzbine.ID==idJela).FirstOrDefault()==null)
                    {
                        r.PreuzetaPorudzbina=preuzeta;
                        r.Datum=datum;
                        r.PorudzbinaZa=await Context.Radnici.FindAsync(idRadnika);
                        r.JeloPorudzbine=await Context.Jela.FindAsync(idJela);
                        await Context.SaveChangesAsync();
                        return Ok($"Uspesno promenjena porudzbina ciji je id: {id}");
                    }
                    else
                    {
                        return BadRequest("Ovakva porudzbina vec postoji!!");
                    }
                        
                }
                else
                {
                    return BadRequest($"Ne postoji meni sa id-em {id}");
                }
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [Route("VratiPorudzbinu/{id}")]
        [HttpGet]
        public async Task<ActionResult> VratiPorudzbinu(int id)
        {
            if(id<0)
            {
                return BadRequest("Pogresan id porudzbine!!!");
            }

            try
            {
                var p=await Context.Porudzbine.Where(p=>p.ID==id)
                                            .Include(r=>r.PorudzbinaZa)
                                            .Include(j=>j.JeloPorudzbine)
                                            .ThenInclude(l=>l.ListaMenija).FirstOrDefaultAsync();
                if(p!=null)
                {
                    return Ok(p);
                }
                else
                {
                    return BadRequest("Porudzbina sa zadataim id-em NE POSTOJI");
                }
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
         [Route("VratiDanasnjuPorudzbinu/{jmbg}")]
        [HttpGet]
        public async Task<ActionResult> VratiDanasnjuPorudzbinu(string jmbg)
        {
            if(jmbg.Length!=13 || string.IsNullOrWhiteSpace(jmbg))
            {
                return BadRequest("Pogresan jmbg");
            } 

            try{
                 
                var porudzbina=await Context.Porudzbine.Where(p=>p.Datum==DateTime.Today && p.PreuzetaPorudzbina==false)
                                                .Include(p=>p.PorudzbinaZa)
                                                .Where(p=>p.PorudzbinaZa.JMBG==jmbg)
                                                .Include(p=>p.JeloPorudzbine)
                                                .ToListAsync();

                if(porudzbina!=null && porudzbina.Count!=0)
                {
                    return Ok(porudzbina.Select(p=>
                    new{
                        preuzetaPorudzbina=p.PreuzetaPorudzbina,
                        datum=p.Datum,
                        nazivJela=p.JeloPorudzbine.NazivJela,
                        restoran=p.JeloPorudzbine.Restoran,
                        cena=p.JeloPorudzbine.Cena
                    }));
                }
                else{
                    return BadRequest("Radnik sa trazenim jbmg-om nema porudzbinu za danas");

                }
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("IzmeniPorudzbinu/{jmbg}")]
        [HttpPut]
        public async Task<ActionResult> IzmeniStatusPorudzbine(string jmbg)
        {
            if(jmbg.Length!=13 || string.IsNullOrWhiteSpace(jmbg))
            {
                return BadRequest("Pogresan jmbg");
            } 

            try{
                var porudzbina= await Context.Porudzbine.Where(p=>p.PorudzbinaZa.JMBG==jmbg && p.Datum==DateTime.Today).FirstOrDefaultAsync();

                if(porudzbina!=null)
                {
                    porudzbina.PreuzetaPorudzbina=true;
                     await Context.SaveChangesAsync();
                    return Ok("Uspesno promenjena porudzbina");
                }
                else{
                    return BadRequest("Radnik nema porudzbinu za danas!");
                }
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
