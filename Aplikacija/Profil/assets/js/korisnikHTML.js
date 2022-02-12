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
}
else{ 
      //var sort=document.getElementById("sortingOptionID").style.visibility="revert"
     
    var pom=document.getElementById("odjavi");
    pom.hidden=false;
    var pom3=document.getElementById("prijava");
    pom3.hidden=true;

    pom.addEventListener("click",function(){
        var inf=document.getElementById("informacije");
        inf.innerHTML="";
      sessionStorage.clear();
      location.href ="index.html"
      });
    
}
