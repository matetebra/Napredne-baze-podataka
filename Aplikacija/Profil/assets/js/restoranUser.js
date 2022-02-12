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
            var inf=document.getElementById("informacije");
            inf.innerHTML="";
          sessionStorage.clear();
          location.href ="index.html"
          });
        
    }
    else
    {
        alert("Nemate privilegiju");
        sessionStorage.clear();
        location.href = "index.html";
    }
}
