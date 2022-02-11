import { restoran} from "./restoran.js";
var r=new restoran();
/*if ( sessionStorage.getItem("token") == null || sessionStorage.getItem("token") == "")  
{
  alert("Niste prijavljeni");
  location.href = "index.html";
} 
else 
{
    if(sessionStorage.getItem("role")=="Korisnik")
    {*/
        r.preuzmiPodatke("cezar@nis.rs");
        //r.preuzmiPodatke(sessionStorage.getItem("restoran"));
        //r.email=sessionStorage.getItem("username");
        var d1 = document.getElementById("btnSacuvaj");
        var d2 = document.getElementById("jela");
        var d3 = document.getElementById("komentari");
        d1.addEventListener("click",sacuvaj); 
        d2.addEventListener("click",jela);  
        d3.addEventListener("click",komentari); 
    /*}
    else
    {
        alert("Nemate privilegiju");
        location.href = "index.html";
    }
}*/
function sacuvaj(){
  fetch("https://localhost:7284/Slavko/BookMarkRestaurant/"+ r.email, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      Authorization: "Bearer " + sessionStorage.getItem("token"),
    }
  })
    .then((p) => {
      if (p.ok) {
        alert("Uspesno bookmarkovanje");
      } else {
        alert("Greska kod bookarkovanja");
      }
    })
    .catch((p) => {
      alert("Greška sa konekcijom.");
    });
}
function jela(){
    var d= document.getElementById("ovdeSeRadi");
    d.innerHTML="";
    r.crtajJela();
}
function komentari(){
    var d= document.getElementById("ovdeSeRadi");
    d.innerHTML="";
    r.crtajKomentari();
}