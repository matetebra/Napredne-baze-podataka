import { restoran} from "./restoran.js";
var r=new restoran();
if ( sessionStorage.getItem("token") == null || sessionStorage.getItem("token") == "")  
{
  alert("Niste prijavljeni");
  location.href = "index.html";
} 
else 
{
    if(sessionStorage.getItem("role")=="Restoran")
    {
        r.preuzmiPodatke(sessionStorage.getItem("username"));

        var d=document.getElementById("btnOdjaviSe");
        d.addEventListener("click",function(){
  
            //var inf=document.getElementById("informacije");
            //inf.innerHTML="";
          sessionStorage.clear();
          location.href ="index.html"
          });

        var host=document.getElementById("iscrtavanje");
        host.innerHTML="";
        var d1=document.getElementById("dodajJelo");
        d1.addEventListener('click',dodajJ(host));
        var d2=document.getElementById("brisiJelo");
        d2.addEventListener('click',brisiJ(host));
        var d3=document.getElementById("dodajDodatak");
        d3.addEventListener('click',dodajD(host));
        var d4=document.getElementById("brisiDodatak");
        d4.addEventListener('click',brisiD(host));
        var d5=document.getElementById("brisiRezervaciju");
        d5.addEventListener('click',brisiR(host));
        var d6=document.getElementById("prihvatiRezervaciju");
        d6.addEventListener('click',prihvatiR(host));
        
        
    }
    else
    {
        alert("Nemate privilegiju");
        sessionStorage.clear();
        location.href = "index.html";
    }
}
function dodajJ(host){
 
  //r.crtajDodajJelo(host);
}
function brisiJ(){
  //r.crtajObrisiJelo(host);
}
function dodajD(){
  r.crtajDodajDodatak(host);
}
function brisiD(){
 
}
function brisiR(){
  
}
function prihvatiR(){
  
}