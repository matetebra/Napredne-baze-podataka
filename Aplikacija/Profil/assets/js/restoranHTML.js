import { restoran } from "./restoran.js";
var res=new restoran("naziv","adresa","grad","mail","4234234","opis","radno vreme","vreme dostave","cena","limit","kapacitet","br slobodnih");
var res1=new restoran("naziv","adresa","grad","mail","423sd4234","opis","radno vreme","vreme dostave","cena","limit","kapacitet","br slobodnih");
var res2=new restoran("naziv","adresa","grad","mail","4234ds234","opis","radno vreme","vreme dostave","cena","limit","kapacitet","br slobodnih");
var res3=new restoran("naziv","adresa","grad","mail","423a4234","opis","radno vreme","vreme dostave","cena","limit","kapacitet","br slobodnih");
res.crtajRestoran(document.getElementById("restorani"));
res1.crtajRestoran(document.getElementById("restorani"));
res2.crtajRestoran(document.getElementById("restorani"));
res3.crtajRestoran(document.getElementById("restorani"));