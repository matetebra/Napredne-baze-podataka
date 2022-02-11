import { restoran } from "./restoran.js";
var r=new restoran();
/*if ( sessionStorage.getItem("token") == null || sessionStorage.getItem("token") == "")  
{
  alert("Niste prijavljeni");
  location.href = "index.html";
} 
else 
{
    if(sessionStorage.getItem("role")=="Admin")
    {*/
        r.email=sessionStorage.getItem("username");
        var d1 = document.getElementById("odobri");
        var d2 = document.getElementById("obrisi");
        d1.addEventListener("click",odobri); 
        d2.addEventListener("click",obrisi);  
        var odjaviSeBtn = document.getElementById("btnOdjaviSe");
        odjaviSeBtn.addEventListener("click",function(){
          console.log("odjavi se");
        sessionStorage.clear();
        location.href ="index.html"
        });
   /* }
    else
    {
        alert("Nemate privilegiju");
        location.href = "index.html";
    }
}*/
function odobri(){
    var d3 = document.getElementById("radiSeOvde");
    d3.innerHTML="";
    restoran.preuzmiNeodobrene();
    restoran.crtajNeodobrene();
}
function obrisi(){
    var d3 = document.getElementById("radiSeOvde");
    d3.innerHTML="";
    d3.style=" margin: 5px";
    var label= document.createElement("label");
    label.innerHTML="Unesi email restorana";
    label.style=" margin: 5px";
    d3.appendChild(label);
    var input= document.createElement("input");
    input.style=" margin: 5px";
    input.id="email";
    d3.appendChild(input);
    var dugme= document.createElement("button");
    d3.appendChild(dugme);
    dugme.innerHTML="Obriši restoran"
    dugme.style=" margin: 5px";
    dugme.classList="btn btn-danger";
    dugme.addEventListener("click",function(){
        var email = document.getElementById("email").innerHTML;
        fetch("https://localhost:7284/Restoran/DeleteRestoran/" + email, {
            method: "DELETE",
            headers: {
              "Content-Type": "application/json",
              Authorization: "Bearer " + sessionStorage.getItem("token"),
            },
            body: JSON.stringify({}),
          })
            .then((p) => {
              if (p.ok) {
                alert("Restoran uspesno obrisan");
                location.href = "administrator.html";
              } else {
                alert("Greska kod brisanja");
              }
            })
            .catch((p) => {
              alert("Greška sa konekcijom.");
            });
      });
}
