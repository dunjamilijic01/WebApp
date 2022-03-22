import { Porudzbina } from "./Porudzbina.js";
import { Radnik } from "./Radnik.js";
import { Meni } from "./Meni.js";

export class Kantina{

    constructor(naziv){

    this.naziv=naziv;
    this.radnici=null;
    this.container=null;
    this.containerTabela=null;
    this.containerUnos=null;
    this.containerForma=null;
    this.containerRanik=null;
    this.containerCrtanje=null;
    this.containerPorudzbine=null;
    this.containerDivZaRbt=null;
    this.containerGornji=null;
}


crtajKantinu(host){

    let glforma=document.createElement("div");
    glforma.className="glFormaDiv"
    host.appendChild(glforma);
    
    this.container=glforma;

    let naziv=document.createElement("h1");
    naziv.innerHTML=this.naziv;
    glforma.appendChild(naziv);

    let gornjiDiv=document.createElement("div");
    gornjiDiv.className="gornjiDiv";
    this.containerGornji=gornjiDiv;
    glforma.appendChild(gornjiDiv);


    let unosDiv=document.createElement("div");
    unosDiv.className="unosDiv";
    this.containerUnos=unosDiv;
    gornjiDiv.appendChild(unosDiv);
    this.crtajUnos(unosDiv);

    let formaDiv=document.createElement("div");
    formaDiv.className="formaDiv";
    formaDiv.style.visibility="hidden";
    this.containerForma=formaDiv;
    glforma.appendChild(formaDiv);

    let radnikDiv=document.createElement("div");
    radnikDiv.className="radnikDiv";
    this.containerRanik=radnikDiv;
    radnikDiv.style.visibility="hidden";
    gornjiDiv.appendChild(radnikDiv);

    let crtanjeDiv=document.createElement("div");
    crtanjeDiv.className="crtanjeDiv";
    this.containerCrtanje=crtanjeDiv;
    formaDiv.appendChild(crtanjeDiv);

    
}
crtajUnos(host){
    let labela=document.createElement("label");
    labela.innerHTML="Unesite svoj JMBG";
    labela.className="JMGBLabela";
    host.appendChild(labela);

    
    let div=document.createElement("div");
    div.className="jmbgDugmeDiv";
    host.appendChild(div);

    let unos=document.createElement("input");
    unos.type="number";
    unos.className="unosPolje"
    div.appendChild(unos);

    let dugme=document.createElement("button");
    dugme.innerHTML="OK";
    div.appendChild(dugme);
    dugme.onclick=(ev)=>{
    this.jmbgFunkcija();
    }
}

jmbgFunkcija(){

    let jmbg=this.containerUnos.querySelector(".unosPolje").value;

    if(jmbg.length==13)
    {
        fetch("https://localhost:5001/Radnik/VratiRadnika/"+jmbg+"/"+this.naziv,
        {
            method:"GET"
        })
        .then(p=>{
            if(p.ok){
                p.json().then(data=>{
                        let r=new Radnik(data.jmbg,data.ime,data.prezime);
                        console.log(r);
                       this.ukloniFormu();
                       let host=this.container.querySelector(".radnikDiv");
                        r.crtajRadnika(host);
                    }
                );
                this.crtajFormu();
               
            }
            else
            {
                this.ukloniFormu();
                this.containerRanik.style.visibility="hidden";
                alert("Nema radnika za unetim jmbg-om");
            }
            });
    }
    else
    {
        alert("JMBG MORA IMATI 13 CIFARA");
    }
   
    }

    crtajTabelu(host){


        let osnovniDiv=document.createElement("div");
        osnovniDiv.className="tabelaDiv";
        host.appendChild(osnovniDiv);
        this.containerPorudzbine=osnovniDiv;

        let kontrole=document.createElement("div");
        osnovniDiv.appendChild(kontrole);
        kontrole.className="kontroleDiv";

        

        let meni=document.createElement("select");
        meni.className="filterPreuzeta";
        kontrole.appendChild(meni);

        let op=document.createElement("option");
        op.innerHTML="sve";
        op.value=0;
        meni.appendChild(op);
        
        op=document.createElement("option");
        op.innerHTML="nepreuzeta porudzbina";
        op.velue=1;
        meni.appendChild(op);

        op=document.createElement("option");
        op.innerHTML="preuzeta porudzbina";
        op.value=2;
        meni.appendChild(op);

        let btn=document.createElement("button");
        btn.innerHTML="Filtriraj";
        btn.className="btnFiltriraj"
        btn.onclick=(ev)=>{
            this.dugmeFiltriraj();
        }
        kontrole.appendChild(btn);


        var tabela=document.createElement("table");
        tabela.className="tabela";
        osnovniDiv.appendChild(tabela);

        var tabelaHead=document.createElement("thead");
        tabela.appendChild(tabelaHead);

        var tr=document.createElement("tr");
        tabelaHead.appendChild(tr);

        var tabelaBody=document.createElement("tbody");
        tabela.appendChild(tabelaBody);
        tabelaBody.className="podaciTabele";
        this.containerTabela=tabela;
        console.log(this.containerTabela);

        let th;

        let zaglavlje=["Naziv jela","Restoran","Cena","Datum","Preuzeta"];
        zaglavlje.forEach(el=>{
            th=document.createElement("th");
            th.innerHTML=el;
            tr.appendChild(th);
        })

    }

    ukloniFormu(){

        
        this.container.removeChild(this.containerForma);
        this.containerGornji.removeChild(this.containerRanik);
        
        let formaDiv=document.createElement("div");
        formaDiv.className="formaDiv";
        this.containerForma=formaDiv;

        let radnikDiv=document.createElement("div");
        radnikDiv.className="radnikDiv";
        this.containerRanik=radnikDiv;
        
        let crtanjeDiv=document.createElement("div");
        crtanjeDiv.className="crtanjeDiv";
        formaDiv.appendChild(crtanjeDiv);
        this.containerCrtanje=crtanjeDiv;
        this.containerGornji.appendChild(radnikDiv);
        this.container.appendChild(formaDiv);
    
    }
    ukloniCrtanjeDiv(){

        this.containerForma.removeChild(this.containerCrtanje);

        let crtanjeDiv=document.createElement("div");
        crtanjeDiv.className="crtanjeDiv";
        this.containerCrtanje=crtanjeDiv;
        console.log(this.containerForma);
        this.containerForma.appendChild(crtanjeDiv);

    }

    crtajFormu()
    {
        this.vratiSvePorudzbine();
    }

    vratiMenije()
    {
        let Listamenija=[];
        let d=new Date;
        let dan=d.getDay();
        dan=(dan+1)%7;
        
        fetch("https://localhost:5001/Meni/VratiMeni/"+dan+"/"+this.naziv,
        {
            method:"GET"
        })
        .then(p=>{
            if(p.ok){
                p.json().then(data=>{
                    let host=this.container.querySelector(".crtanjeDiv");
                        let div= document.createElement("div");
                        div.className="divZaRbt";
                        this.containerDivZaRbt=div;
                        let lab=document.createElement("label");
                        lab.innerHTML="Napravite porudzbinu za sutra:"
                        lab.className="labMeniji";
                        div.appendChild(lab);

                        host.appendChild(div);
                            data.forEach(d=>{
                                Listamenija.push(d);
                                let meni=new Meni(d.id,d.naziv,d.nazivJela,d.dan,d.restoran,d.cena);
                                meni.crtajMeni(div);
                       
                    })
                
                    let dugme=document.createElement("button");
                    dugme.innerHTML="Poruci";
                    dugme.className="btnPoruci";
                    this.containerDivZaRbt.appendChild(dugme);
                    dugme.onclick=(ev)=>{
                        this.poruci();
                    }

                    this.porucenoZaDanas();
                })
            }
        })
    }

    vratiSvePorudzbine()
    {
        let jmbg=this.container.querySelector(".unosPolje").value;
        if(jmbg.length==13)
        {
            fetch("https://localhost:5001/Radnik/VratiSvePorudzbineRadnika/"+jmbg,
            {
                method:"GET"
            })
            .then(p=>{
                if(p.ok){
                    p.json().then(data=>{
                        data.forEach(d=>{
                            console.log(d);
                            this.ukloniCrtanjeDiv();
                            let host=this.containerForma.querySelector(".crtanjeDiv");
                            this.crtajTabelu(host);
                            (d.porudzbineRadnika).forEach(porudzbina => {
                                console.log(porudzbina);
                                var p = new Porudzbina(porudzbina.id,porudzbina.preuzeta,porudzbina.datum,porudzbina.nazivJela,porudzbina.restoran,porudzbina.cena);
                                p.crtajPorudzbine(this.containerTabela);
                               
                            });
                        })
                        
                    })
                    this.vratiMenije();
                }
            }); 
        }
        else{
            alert("JMGB MORA IMATI 13 CIFARA");
        }
        
    }

    vratiFiltriranePorudzbine(i)
    {
        let jmbg=this.container.querySelector(".unosPolje").value;
        if(jmbg.length==13){
            fetch("https://localhost:5001/Radnik/VratiPorudzbineRadnika/"+jmbg+"/"+i,
            {
                method:"GET"
            })
            .then(p=>{
                if(p.ok){
                    p.json().then(data=>{
                        data.forEach(d=>{
                            this.ukloniCrtanjeDiv();
                            let host=this.container.querySelector(".crtanjeDiv");
                            this.crtajTabelu(host);
                            (d.porudzbineRadnika).forEach(porudzbina => {
                                console.log(porudzbina);
                                var p = new Porudzbina(porudzbina.id,porudzbina.preuzeta,porudzbina.datum,porudzbina.nazivJela,porudzbina.restoran,porudzbina.cena);
                                p.crtajPorudzbine(this.containerTabela);
                               
                            });
                        })
                        
                    })
                    this.vratiMenije();
                    
                }
    
            }); 
        }
        else{
            alert("JMGB MORA IMATI 13 CIFARA");
        }
        
    }

    dugmeFiltriraj()
    {
        let host=this.container.querySelector(".kontroleDiv");
        let se=host.querySelector("select");
        let i=se.options[se.selectedIndex];
        console.log(i.index);

        if(i.index==0)
        {
            this.vratiSvePorudzbine();
        }
        else 
        {
            this.vratiFiltriranePorudzbine(i.index);
        }
    }

    poruci(){
        //let div=this.containerDivZaRbt.querySelector(".divZaRbt");
        //console.log(div);
        let rbt=this.containerDivZaRbt.querySelector("[name='meniji']:checked");

        if(rbt==null)
        {
            alert("Morate izabrati meni prilikom porucivanja!");
        }

        let jeloId;


        fetch("https://localhost:5001/Meni/VratiJelo/"+rbt.value,
        {
            method:"GET"
        }).then(p=>{
            if(p.ok){
                p.json().then(data=>
                    {
                         var jeloId=data;
                         console.log(jeloId); 

                         this.vratiRadnika(jeloId);

                    })
                    
            } 
           
        })

    }

    vratiRadnika(jeloId)
    {
        let radnikId;

        let jmbg=this.container.querySelector(".unosPolje").value;
        //console.log(jmbg);
    
        fetch("https://localhost:5001/Radnik/VratiRadnika/"+jmbg+"/"+this.naziv,
        {
        method:"GET"
        })
            .then(p=>{
                if(p.ok){
                    p.json().then(data=>{
                        radnikId=data.id;
                        console.log(radnikId);
                        this.dodajPorudzbinu(jeloId,radnikId);
                    });
                }
            });
    }

    dodajPorudzbinu(jeloId,radnikId)
    {
        console.log(jeloId+" "+radnikId);
        let datum=new Date;
        datum.setDate(new Date().getDate()+1);
        console.log(datum);
        fetch("https://localhost:5001/Porudzbina/DodajPorudzbinu/"+false+"/"+datum.toDateString()+"/"+radnikId+"/"+jeloId,
        {
            method:"POST"
        }).then(s=>
            {
                if(s.ok){
                    this.ukloniCrtanjeDiv();
                    this.vratiSvePorudzbine();
                }
                else
                {
                    alert("Porudzbina za taj datum je vec napravljena!!");
                }
            })
    }

    porucenoZaDanas()
    {
        let div=document.createElement("div");
        div.className="danasnjaPorudzbina";
        let host=this.container.querySelector(".crtanjeDiv");
        host.appendChild(div);
        
        let labela=document.createElement("label");
        div.appendChild(labela);
        labela.className="porucenoZaDanasLab";
        labela.innerHTML="ZA DANAS JE PORUCENO:";

        let jmbg=this.containerUnos.querySelector(".unosPolje").value;

        fetch("https://localhost:5001/Porudzbina/VratiDanasnjuPorudzbinu/"+jmbg,
        {
            method:"GET"
        }).then(p=>
            {
                if(p.ok){
                    p.json().then(p=>{
                        p.forEach(data=>{
                            let por=new Porudzbina(data.id,data.preuzetaPorudzbina,data.datum,data.nazivJela,data.restoran,data.cena);
                            console.log(por);
                            por.crtajDanasnjuPorudzbinu(div);  
        
                            let dugmePreuzmi=document.createElement("button");
                            dugmePreuzmi.className="btnPreuzmi";
                            dugmePreuzmi.innerHTML="Preuzmi porudzbinu";
                            div.appendChild(dugmePreuzmi);
                            dugmePreuzmi.onclick=(ev)=>{
                                this.preuzmiPorudzbinu();
                        }
                    })
                        
                    }
                )
                
            }
                else
                {
                    let lab=document.createElement("label");
                    lab.innerHTML="Nemate porudzbinu za danas!";
                    lab.style.backgroundColor="red";
                    div.appendChild(lab);
                }
            })


    }

    preuzmiPorudzbinu()
    {
        let redovi=this.containerTabela.querySelectorAll(".redTabele");
        console.log(redovi);
        redovi.forEach(p=>{
            let polje=p.querySelector(".datumPorudzbine");
           
            var date = new Date(Date.parse(polje.innerHTML));
            console.log(date);
            
           let datum=new Date();
           datum.getDate();
           datum.setHours(0,0,0,0);
           console.log(datum.toString());

           if(date==datum.toString())
           {
                this.preuzimanje();
           }
        })
    }

    preuzimanje(){


        console.log("uso u preuzimanje!");
        let jmbg=this.containerUnos.querySelector(".unosPolje").value;

        fetch("https://localhost:5001/Porudzbina/IzmeniPorudzbinu/"+jmbg,
        {
            method:"PUT"
        }).then(s=>
            {
                if(s.ok){
                    this.ukloniCrtanjeDiv();
                    this.vratiSvePorudzbine();
                }
                else
                {
                    alert("Greska");
                }
            })
        }

}

