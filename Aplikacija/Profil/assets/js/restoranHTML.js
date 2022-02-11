import { restoran } from "./restoran.js";
import { korisnik } from "./korisnik.js";

export class restorani{
    constructor(){
        this.restorani=[];
    }   

ucitajRestorane(){
    fetch("https://localhost:7284/Slavko/GetRestourants").then((p) => {
        p.json().then((data) => {
          data.forEach((element) => {
            var r = new restoran();
            r.naziv=element.naziv;
            r.adresa=element.adresa;
            r.email=element.email;
            r.telefon=element.telefon;
            this.restorani.push(r);
          });
          this.crtajsveRestorane();
        });
      });
    }

crtajsveRestorane(){
    const host=document.getElementById("restorani");
    this.restorani.forEach((rest)=>{
        rest.crtajRestoran(host)
    });
}
}
var res=new restorani();
res.ucitajRestorane();
