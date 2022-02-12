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
        r.preuzmiPodatke(sessionStorage.getItem("restoran"));
        var d1 = document.getElementById("btnSacuvaj");
        var d2 = document.getElementById("jela");
        var d3 = document.getElementById("komentari");
        var d4 = document.getElementById("rezervisi");
        d1.addEventListener("click",sacuvaj); 
        d2.addEventListener("click",jela);  
        d3.addEventListener("click",komentari); 
        d4.addEventListener("click",rezervisi); 
    }
    else
    {
        alert("Nemate privilegiju");
        location.href = "index.html";
    }
}