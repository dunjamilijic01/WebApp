export class Radnik{

    constructor(jmbg,ime,prezime){
        this.ime=ime;
        this.prezime=prezime;
        this.jmbg=jmbg;
        this.container=null;
    }

    crtajRadnika(host){

        this.container=host;
        console.log(host);
        let labela=document.createElement("label");
        labela.innerHTML="RADNIK";
        labela.className="labRadnik";
        host.appendChild(labela);

        let div=document.createElement("div");

        labela=document.createElement("label");
        labela.innerHTML="Ime: ";
        div.appendChild(labela);

        labela=document.createElement("label");
        labela.innerHTML=this.ime;
        labela.className="imeLabela";
        div.appendChild(labela);

        host.appendChild(div);

        div=document.createElement("div");

        labela=document.createElement("label");
        labela.innerHTML="Prezime: ";
        div.appendChild(labela);

        labela=document.createElement("label");
        labela.innerHTML=this.prezime;
        labela.className="imeLabela";
        div.appendChild(labela);

        host.appendChild(div);

        div=document.createElement("div");

        labela=document.createElement("label");
        labela.innerHTML="JMBG: ";
        div.appendChild(labela);

        labela=document.createElement("label");
        labela.innerHTML=this.jmbg;
        labela.className="jmbgLabela";
        div.appendChild(labela);

        host.appendChild(div);
    }


}