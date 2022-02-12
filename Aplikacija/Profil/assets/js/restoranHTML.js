import { restoran } from "./restoran.js";
import { korisnik } from "./korisnik.js";
import { restorani } from "./restorani.js";

var res=new restorani();
res.ucitajRestorane();
var dugm=document.getElementById("homebtn");
dugm.hidden=true;
//res.ucitajBukmarkovaneRestorane();
if(sessionStorage.getItem("token")!=null && sessionStorage.getItem("token")!="")
{
    var dugme=document.getElementById("omiljeniRes");  
    dugme.addEventListener('click',bukmarkovani);
}
function bukmarkovani(){
    const host=document.getElementById("restorani");
    host.innerHTML="";
        res.ucitajBukmarkovaneRestorane();  
        var dugm=document.getElementById("homebtn");
        dugm.hidden=false;
        dugm.addEventListener('click',vrati);
}
function vrati(){
    const host=document.getElementById("restorani");
    host.innerHTML="";
        res.ucitajRestorane();  
        var dugm=document.getElementById("homebtn");
        dugm.hidden=true;
}