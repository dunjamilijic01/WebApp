export class Porudzbina{

    constructor(id,preuzeta,datum,nazivJela,restoran,cena){
        
        this.id=id;
        this.container=null;
        this.preuzeta=preuzeta;
        this.datum=datum;
        this.nazivJela=nazivJela;
        this.restoran=restoran;
        this.cena=cena;
    }

    crtajPorudzbine(host){

        let porudzbina=document.createElement("tr");
        this.container=porudzbina;
        porudzbina.className="redTabele";
        host.appendChild(porudzbina);

        let polje=document.createElement("td");
        polje.className="nazivJela";
        polje.innerHTML=this.nazivJela;
        porudzbina.appendChild(polje);

        polje=document.createElement("td");
        polje.className="restoran";
        polje.innerHTML=this.restoran;
        porudzbina.appendChild(polje);

        polje=document.createElement("td");
        polje.className="cena";
        polje.innerHTML=this.cena;
        porudzbina.appendChild(polje);

        polje=document.createElement("td");
        polje.className="datumPorudzbine";
        polje.innerHTML=this.datum;
        porudzbina.appendChild(polje);

        polje=document.createElement("td");
        polje.className="preuzetaPorudzbina";
        polje.innerHTML=this.preuzeta;
        if(this.preuzeta==true)
        polje.style.backgroundColor="rgb(0,255,0)";
        porudzbina.appendChild(polje);

        polje=document.createElement("button");
        polje.className="btnObrisi";
        polje.innerHTML="Obrisi"
        porudzbina.appendChild(polje);
        polje.onclick=(ev)=>{
            this.obrisi();
        }

    }
    crtajDanasnjuPorudzbinu(host)
    {
        let div=document.createElement("div");
        host.appendChild(div);

                    let lab=document.createElement("label");
                    lab.innerHTML="Jelo: ";
                    div.appendChild(lab);

                    lab=document.createElement("label");
                    lab.innerHTML=this.nazivJela;
                    div.appendChild(lab);

                    div=document.createElement("div");
                    host.appendChild(div);

                    lab=document.createElement("label");
                    lab.innerHTML="Restoran: ";
                    div.appendChild(lab);

                    lab=document.createElement("label");
                    lab.innerHTML=this.restoran;
                    div.appendChild(lab);

                    div=document.createElement("div");
                    host.appendChild(div);

                    lab=document.createElement("label");
                    lab.innerHTML="Cena: ";
                    div.appendChild(lab);

                    lab=document.createElement("label");
                    lab.innerHTML=this.cena;
                    div.appendChild(lab);

                   
    }
    obrisi(){
        let roditelj=this.container.parentNode;
        console.log(roditelj);
        roditelj.removeChild(this.container);

        fetch("https://localhost:5001/Porudzbina/IzbrisiPorudzbinu/"+this.id,
        {
            method:"DELETE"
        }).then(s=>
            {
                if(s.ok){
                    alert("Izbirana porudzbina!");
                }
                else
                {
                    alert("Greska prilikom brisanja!");
                }
            })
    }
}
