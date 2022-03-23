import { Kantina } from "./Kantina.js";
import { Radnik } from "./Radnik.js";

let listaKantina=[];

fetch("https://localhost:5001/Kantina/VratiKantine",
{
    method:"GET"
}).then(p=>
    {
        if(p.ok){
            p.json().then(data=>
                {
                    data.forEach(element => {
                        console.log(element.nazivKantine)
                        let k=new Kantina(element.nazivKantine);
                        k.crtajKantinu(document.body);      
                    });
                }
            );
        }
    });
           
