import { restoran } from "./restoran.js";
import { korisnik } from "./korisnik.js";
export class restorani{
    constructor(){
        this.restorani=[];
    }   
}
//ucitajRestorane(){

//}
var res=new restoran("naziv","adresa","grad","mail","4234234","opis","radno vreme","vreme dostave","cena","limit","kapacitet","br slobodnih");
var res1=new restoran("naziv","adresa","grad","mail","423sd4234","opis","radno vreme","vreme dostave","cena","limit","kapacitet","br slobodnih");
var res2=new restoran("naziv","adresa","grad","mail","4234ds234","opis","radno vreme","vreme dostave","cena","limit","kapacitet","br slobodnih");
var res3=new restoran("naziv","adresa","grad","mail","423a4234","opis","radno vreme","vreme dostave","cena","limit","kapacitet","br slobodnih");
res.crtajRestoran(document.getElementById("restorani"));
res1.crtajRestoran(document.getElementById("restorani"));
res2.crtajRestoran(document.getElementById("restorani"));
res3.crtajRestoran(document.getElementById("restorani"));
var kor=new korisnik("email@mail.com","Pera","Peric","Bulevar Nemanjica","Nis", "064123456");
kor.crtajInformacije();