import { restoran } from "./restoran.js";
import { korisnik } from "./korisnik.js";
import { restorani } from "./restorani.js";
var res=new restorani();
res.ucitajRestorane();
if(sessionStorage.getItem("token")!=null && sessionStorage.getItem("token")!="")
{
var dugme=document.getElementById('prikaziBukmarkovane');
dugme.addEventListener('click',prikaziBukmarkovane);
var pomocna=new restorani();
function prikaziBukmarkovane(){
    pomocna.ucitajBukmarkovaneRestorane();
}
    
}