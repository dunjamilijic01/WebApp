export class Meni{

    constructor(id,naziv,jelo,dan,restoran,cena,nazivKantine){
        this.id=id;
        this.naziv=naziv;
        this.nazivKantine=nazivKantine
        this.jelo=jelo;
        this.dan=dan;
        this.restoran=restoran;
        this.cena=cena
        this.container=null;
        this.btnContainer=null;
    }

    crtajMeni(host){

        let divZaMeni=document.createElement("div");
        this.container=divZaMeni;
        divZaMeni.className="divZaMeni";
        host.appendChild(divZaMeni);

        let rbt=document.createElement("input");
        rbt.className="rbtNaziv";
        rbt.type="radio"
        rbt.name="meniji"+this.nazivKantine;
        rbt.value=this.id;
        divZaMeni.appendChild(rbt);

        let lab=document.createElement("label");
        lab.className="rbtNaziv";
        lab.innerHTML=this.naziv;
        divZaMeni.appendChild(lab);

        let btn=document.createElement("button");
        this.btnContainer=btn;
        btn.className="rbtBtn";
        btn.innerHTML="Info";
        divZaMeni.appendChild(btn);
        btn.onclick=(ev)=>{
            this.info();
        }
    }

        info()
        {
            alert(this.naziv+": Jelo: "+this.jelo+" Restoran: "+this.restoran+" Cena: "+this.cena);
        }


    }
