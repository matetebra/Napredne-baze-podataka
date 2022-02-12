import { korisnik } from "./korisnik.js";

 var inf=document.getElementById("informacije");
        inf.innerHTML="";
if(sessionStorage.getItem("token")==null || sessionStorage.getItem("token")=="")
{
   
    var pom=document.getElementById("omiljeniRes");
    pom.hidden=true;
    var pom1=document.getElementById("odjavi");
    pom1.hidden=true;
    var pom2=document.getElementById("sacuvaneNar");
    pom2.hidden=true;
    var pom3=document.getElementById("slika");
    pom3.hidden=true;
    var pom4=document.getElementById("sortingOptionID");
    pom4.hidden=true;
    var pom5=document.getElementById("SortirajPoKategoriji");
    pom5.hidden=true;
    var pom6=document.getElementById("kategorijaInput");
    pom6.hidden=true;
    var pom7=document.getElementById("kategorijalbl");
    pom7.hidden=true;
    
    var pom8=document.getElementById("poNazivuLbl");
    pom8.hidden=true;
    var pom10=document.getElementById("NazivInput");
    pom10.hidden=true;
    var pom9=document.getElementById("pretraziPoNazivu");
    pom9.hidden=true;
}
else{ 
      //var sort=document.getElementById("sortingOptionID").style.visibility="revert"
     
    var pom=document.getElementById("odjavi");
    pom.hidden=false;
    var pom3=document.getElementById("prijava");
    pom3.hidden=true;
    var pom4=document.getElementById("sortingOptionID");
    pom4.hidden=false;
    var pom5=document.getElementById("SortirajPoKategoriji");
    pom5.hidden=false;
    var pom6=document.getElementById("kategorijaInput");
    pom6.hidden=false;
    var pom7=document.getElementById("kategorijalbl");
    pom7.hidden=false;
    pom.addEventListener("click",function(){
        var inf=document.getElementById("informacije");
        inf.innerHTML="";
      sessionStorage.clear();
      location.href ="index.html"
      });
    
}
