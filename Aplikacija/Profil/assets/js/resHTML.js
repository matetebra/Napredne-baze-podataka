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
        var d4 = document.getElementById("rezervisi");
        d1.addEventListener("click",sacuvaj); 
        d2.addEventListener("click",jela);  
        d3.addEventListener("click",komentari); 
        d4.addEventListener("click",rezervisi); 
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
    var d1= document.getElementById("ovdeSeRadiJ");
    d1.innerHTML="";
    d2= document.getElementById("ovdeSeRadiD");
    d2.innerHTML="";
    var d= document.getElementById("ovdeSeRadiB");
    d.innerHTML="";
    r.crtajJela(d1);
    r.crtajDodatke(d2);
}
function komentari(){
    var d= document.getElementById("ovdeSeRadiJ");
    d.innerHTML="";
    var d2= document.getElementById("ovdeSeRadiD");
    d.innerHTML="";
    var d3= document.getElementById("ovdeSeRadiB");
    d3.innerHTML="";
    r.crtajKomentari(d);
}
function rezervisi(){
    var d1= document.getElementById("ovdeSeRadiJ");
    d1.innerHTML="";
    var d2= document.getElementById("ovdeSeRadiD");
    d2.innerHTML="";
    var d= document.getElementById("ovdeSeRadiB");
    d.innerHTML="";
    d.style="display:flex; flex-wrap:wrap; flex-direction:column; width:700px";
    var lab= document.createElement("h5");
    lab.innerHTML="Unesite broj mesta:";
    d.appendChild(lab);
    lab= document.createElement("input");
    lab.type="number";
    lab.id="mestoZakazivanja";
    d.appendChild(lab);
    lab= document.createElement("h5");
    lab.innerHTML="Unesite vreme u 24-h formatu:";
    d.appendChild(lab);
    lab= document.createElement("input");
    lab.type="number";
    lab.id="vremeZakazivanja";
    d.appendChild(lab);
    var lab= document.createElement("h5");
    lab.innerHTML="Unesite datum:";
    d.appendChild(lab);
    var inp5 = document.createElement("input");
    inp5.type = "date";
    inp5.id = "datumZakazivanja";
    d.appendChild(inp5);
    var pogled = document.createElement("button");
    pogled.classList.add("btn");
    pogled.classList.add("btn-danger");
    pogled.innerHTML = "Rezerviši";
    d3.appendChild(pogled);
    pogled.addEventListener("click",dodajRezervaciju);
    d.appendChild(pogled);
}
function dodajRezervaciju(){
  var mesta= document.getElementById("mestoZakazivanja").value;
  var vreme= document.getElementById("vremeZakazivanja").value;
  var datum= document.getElementById("datumZakazivanja").value;
  if(mesta==null || vreme==null || datum==null)
  {
    alert("Unesite sva polja");
    return;
  }
  fetch("https://localhost:7284/Rezervacija/AddReservation/" + r.email, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      brojMesta: mesta,
      vreme: vreme,
      datum: datum
    }),
  })
    .then((p) => {
      if (p.ok) {
        alert("Uspesno rezervisano: "+ mesta+" mesta.");
      } else {
        alert("Ne mozete rezervisati.");
      }
    })
    .catch(() => {
      alert("Greska sa konekcijom");
    });
}