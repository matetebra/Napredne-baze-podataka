import { restoran} from "./restoran.js";
var r=new restoran();
if ( sessionStorage.getItem("token") == null || sessionStorage.getItem("token") == "")  
{
  alert("Niste prijavljeni");
  location.href = "index.html";
} 
else 
{
    if(sessionStorage.getItem("role")=="Korisnik")
    {
        //r.preuzmiPodatke("cezar@nis.rs");
        r.preuzmiPodatke(sessionStorage.getItem("restoran"));
        //r.email=sessionStorage.getItem("username");
        var d1 = document.getElementById("btnSacuvaj");
        var d2 = document.getElementById("jela");
        var d3 = document.getElementById("komentari");
        var d4 = document.getElementById("rezervisi");
        var d5 = document.getElementsByClassName("oceni");
        d1.addEventListener("click",sacuvaj); 
        d2.addEventListener("click",jela);  
        d3.addEventListener("click",komentari); 
        d4.addEventListener("click",rezervisi); 
        for(let i=0; i<5; i++)
        {
          d5[i].addEventListener("click",oceni);
          var label=document.getElementById("Vrednost")
          label.value=d5[i].innerHTML;
        }
        //var d6 = document.getElementById("Korpa");
        var d7 = document.getElementById("Pocetna");
        d7.addEventListener("click",pocetna);  
    }
    else
    {
        alert("Nemate privilegiju");
        sessionStorage.clear();
        location.href = "index.html";
    }
}
function oceni(){
  r.oceniRestoran(document.getElementById("Vrednost").value);
}
function pocetna(){
  let confirmAction = confirm("Ukoliko izadjete sa stranice izgubicete stvari iz korpe.");
        if (confirmAction) {
          location.href="index.html";
          //sessionStorage.setItem("")
        } 
}
function sacuvaj(){
  fetch("https://localhost:7284/Slavko/BookMarkRestaurant/"+ r.email, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      Authorization: "Bearer " + sessionStorage.getItem("token")
    }
  })
    .then((p) => {
      if (p.ok) {
        alert("Uspesno bookmarkovanje");
      } else {
        alert("Vec ste bookmarkovali");
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
    var d4= document.getElementById("ovdeSeRadiK");
    d4.innerHTML="";
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
    var sve= document.getElementById("ovdeSeRadiK");
    sve.innerHTML="";
    sve.style="display:flex;flex-direction:row; flex-wrap:wrap;"

    var d4=document.createElement("div");
    sve.appendChild(d4);
    d4.className="col-md-6";
    d4.style="display:flex; flex-direction:column; flex-wrap:wrap;"
    var d7= document.createElement("h5");
    d7.innerHTML="Unesite komentar: "
    d7.style="margin-right:5px; margin-top:5px"
    d4.appendChild(d7);
    var d8= document.createElement("input");
    d8.type="textarea"
    d8.id="komentarText"
    d8.style="height:100px; max-width:500px"
    d4.appendChild(d8);
    var pogled = document.createElement("button");
    pogled.classList.add("btn");
    pogled.classList.add("btn-danger");
    pogled.innerHTML = "Dodaj";
    pogled.style="width:100px; margin-top:5px; margin-bottom:5px;"
    d4.appendChild(pogled);
    pogled.addEventListener("click",dodajKomentar);
    r.crtajKomentari(d4);
}
function dodajKomentar(){
  var komentar=document.getElementById("komentarText").value;
  if(komentar==null)
  {
    alert("Unesite tekst");
    return;
  }
  fetch("https://localhost:7284/Slavko/AddComment/" + r.email, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      Authorization: "Bearer " + sessionStorage.getItem("token"),
    },
    body: JSON.stringify({
      tekst: komentar
    }),
  })
    .then((p) => {
      if (p.ok) {
        alert("Uspesno dodavanje komentara");
        komentari();
      } else {
        alert("Ne mozete komentarisati.");
      }
    })
    .catch(() => {
      alert("Greska sa konekcijom");
    });
}
function rezervisi(){
    var d4= document.getElementById("ovdeSeRadiK");
    d4.innerHTML="";
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
      Authorization: "Bearer " + sessionStorage.getItem("token"),
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