export class restoran{
    constructor(naziv, adresa, grad, email, telefon, opis, radnoVreme, vremeDostave, cenaDostave, limitDostave, kapacitet, brSlobodnih, cena,o ){
        this.naziv=naziv;
        this.adresa= adresa;
        this.grad= grad;
        this.email= email;
        this.telefon= telefon;
        this.opis= opis;
        this.radnoVrem= radnoVreme;
        this.vremeDostave= vremeDostave;
        this.cenaDostave= cenaDostave;
        this.limitDostave= limitDostave;
        this.kapacitet= kapacitet;
        this.brSlobodnih= brSlobodnih;
        this.cena=cena;
        this.prosecnaOcena=o;
        this.komentari=[];
        this.kategorije=[];
        this.jela=[];
        this.dodatak=[];
        this.id=null;
        this.kontRestoran=null;
        this.jelaNar=[];
        this.dodNar=[];
    }
    crtajRestoran(host){
        if (!host) throw new Error("Greska u hostu");
        
        var pom = document.createElement("div");
        pom.classList.add("col-md-3");
        pom.classList.add("col-lg-3");
        pom.classList.add("d-md-flex");
        pom.classList.add("align-items-md-stretch");
        pom.classList.add("margine");
        //pom.classList.add("col-sm-6");
        pom.classList.add("card-box-a");
        pom.classList.add("razmak");
        pom.classList.add("row");
        pom.classList.add("marg")
        host.appendChild(pom);
        host.classList.add("marg")

       // var img = document.createElement("img");
        //img.classList.add(".card-overlay-a-content");
        //img.src = "assets/img/rest1.jpg";
       // img.classList.add("col-md-6")
        //pom.appendChild(img);

        const pom1 = document.createElement("div");
        pom1.classList.add("card-overlay");
        pom1.classList.add("card-overlay-a-content");
        //pom1.classList.add("col-md-6")
        pom.appendChild(pom1);
        
        const naziv = document.createElement("h1");
        naziv.classList.add("card-title-a");
        naziv.classList.add("naziv");
        naziv.innerHTML = this.naziv;
        pom.appendChild(naziv);

        const mejl = document.createElement("h2");
        mejl.classList.add("card-title-a");
        mejl.classList.add("naziv");
        mejl.innerHTML ="Email: "+ this.email;
        pom.appendChild(mejl);

        const telefon = document.createElement("h2");
        telefon.classList.add("card-title-a");
        telefon.classList.add("naziv");
        telefon.innerHTML = "Telefon: "+ this.telefon;
        pom.appendChild(telefon);

        var pogled = document.createElement("button");
        pogled.classList.add("btn");
        pogled.classList.add("btn-a");
        pogled.classList.add("col-md-1");
        pogled.innerHTML = "Pogledaj";
        pogled.id=this.email;
        host.appendChild(pogled);
        pogled.addEventListener("click",function(){
          sessionStorage.setItem("restoran",pogled.id);
          location.href="restoran.html";
        });
    }
    crtajNeodobreniRestoran(host){
        if (!host) throw new Error("Greska u hostu");
        const pom = document.createElement("div");
        pom.classList.add("col-md-3");
        pom.classList.add("col-lg-3");
        pom.classList.add("d-md-flex");
        pom.classList.add("align-items-md-stretch");
        //pom.classList.add("col-sm-6");
        pom.classList.add("card-box-a");
        pom.classList.add("razmak");
        pom.classList.add("row");
        pom.classList.add("marg")
        host.appendChild(pom);
        host.classList.add("marg")

        const pom1 = document.createElement("div");
        pom1.classList.add("card-overlay");
        pom1.classList.add("card-overlay-a-content");
        //pom1.classList.add("col-md-6")
        pom.appendChild(pom1);
        
        const naziv = document.createElement("h1");
        naziv.classList.add("card-title-a");
        naziv.classList.add("naziv");
        naziv.innerHTML = this.naziv;
        pom.appendChild(naziv);

        const mejl = document.createElement("h2");
        mejl.classList.add("card-title-a");
        mejl.classList.add("naziv");
        mejl.innerHTML ="Email: "+ this.email;
        pom.appendChild(mejl);

        const telefon = document.createElement("h2");
        telefon.classList.add("card-title-a");
        telefon.classList.add("naziv");
        telefon.innerHTML = "Telefon: "+ this.telefon;
        pom.appendChild(telefon);

        var pogled = document.createElement("button");
        pogled.classList.add("btn");
        pogled.classList.add("btn-danger");
        pogled.innerHTML = "Odobri";
        pom.appendChild(pogled);
        pogled.id=this.email;
        pogled.addEventListener("click",function(){
            console.log(pogled.id);
            fetch("https://localhost:7284/Restoran/OdobriRestoran/"+pogled.id, {
                method: "PUT",
                headers: {
                  "Content-Type": "application/json",
                  Authorization: "Bearer " + sessionStorage.getItem("token"),
                }
              })
                .then((p) => {
                  if (p.ok) {
                    alert("Uspesno odobravanje");
                  } else {
                    alert("Greska kod odobravanje");
                  }
                })
                .catch((p) => {
                  alert("Greška sa konekcijom.");
                });
        })
        
    }
    Osvezi(){
      var d1 = document.getElementById("adresa").innerHTML=this.adresa;
      d1 = document.getElementById("opis").innerHTML=this.opis;
      d1 = document.getElementById("kapacitet").innerHTML=this.kapacitet;
      d1 = document.getElementById("cenaDostave").innerHTML=this.cenaDostave;
      d1 = document.getElementById("vremeDostave").innerHTML=this.vremeDostave;
      d1 = document.getElementById("limitDostave").innerHTML=this.limitDostave;
      d1 = document.getElementById("mail").innerHTML=this.email;
      d1 = document.getElementById("phone").innerHTML=this.telefon;
      d1 = document.getElementById("ime").innerHTML=this.naziv;
      d1 = document.getElementById("prosecnaOcena").innerHTML=this.prosecnaOcena;
    }
    crtajJela(host){
      //<div class="pic"><img src="assets/img/team/team-1.jpg" class="img-fluid" alt=""></div> //za sliku
      if (!host) throw new Error("Greska u hostu");
      for (let i = 0; i < 4; i++) {
      this.jela.forEach(e => {
        var d1= document.createElement("div");
        d1.className="col-lg-4 member";
        host.appendChild(d1);
        var d2= document.createElement("div");
        d2.classList="d-flex align-items-start";
        d1.appendChild(d2);
        var d3= document.createElement("div");
        d3.className="member-info";
        d2.appendChild(d3);
        var d4= document.createElement("h5");
        d4.innerHTML=e.naziv;
        d3.appendChild(d4);
        var d5= document.createElement("h6");
        d5.innerHTML="Cena: " + e.cena;
        d3.appendChild(d5);
        var d6= document.createElement("h6");
        d6.innerHTML="Gramaza: " + e.gramaza;
        d3.appendChild(d6);
        d6= document.createElement("h6");
        d6.innerHTML=e.opis;
        d3.appendChild(d6);
        var l= document.createElement("h6");
        l.innerHTML="";
        e.nazivNamirnica.forEach(n => {
          l.innerHTML=l.innerHTML + " " + n ;
        });
        d3.appendChild(l);
        var pogled = document.createElement("button");
        pogled.classList.add("btn");
        pogled.classList.add("btn-danger");
        pogled.innerHTML = "+";
        pogled.id=e.id;
        d3.appendChild(pogled);
        pogled.addEventListener("click",function(){
          var j= document.getElementById("jelaNam");
          j.innerHTML=j.innerHTML+pogled.id + ",";
          alert("Dodato u korpu");
          console.log(j.innerHTML);
        });
      });}
    }
    crtajDodatke(host){
      if (!host) throw new Error("Greska u hostu");
      this.dodatak.forEach(e => {
        var d1= document.createElement("div");
        d1.className="col-lg-4 member";
        host.appendChild(d1);
        var d2= document.createElement("div");
        d2.classList="d-flex align-items-start";
        d1.appendChild(d2);
        var d3= document.createElement("div");
        d3.className="member-info";
        d2.appendChild(d3);
        var d4= document.createElement("h5");
        d4.innerHTML=e.naziv;
        d3.appendChild(d4);
        var d5= document.createElement("h6");
        d5.innerHTML="Cena: " + e.cena;
        d3.appendChild(d5);
        var pogled = document.createElement("button");
        pogled.classList.add("btn");
        pogled.classList.add("btn-danger");
        pogled.innerHTML = "+";
        pogled.id=e.id;
        d3.appendChild(pogled);
        pogled.addEventListener("click",function(){
          var j= document.getElementById("dodNam");
          j.innerHTML=j.innerHTML+pogled.id + ",";
          alert("Dodato u korpu");
        });
      });
    }
    crtajKomentari(host)
    {
      if (!host) throw new Error("Greska u hostu");
      this.komentari.forEach(e => {
        var d1= document.createElement("div");
        d1.className="col-lg-12 member";
        host.appendChild(d1);
        var d2= document.createElement("div");
        d2.classList="d-flex align-items-start";
        d1.appendChild(d2);
        var d3= document.createElement("div");
        d3.className="member-info";
        d2.appendChild(d3);
        var d4= document.createElement("h5");
        d4.innerHTML=e.email;
        d3.appendChild(d4);
        var d4= document.createElement("h6");
        d4.innerHTML=e.tekst;
        d3.appendChild(d4);
      });
    }
    preuzmiPodatke(emails){
      fetch("https://localhost:7284/Slavko/GetAllRestourantInformations/"+emails, {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + sessionStorage.getItem("token")
      }
    })
      .then((response) => response.json())
      .then((data) => {
        if (data.title == "Unauthorized")
          alert("Greska.");
        else {
        this.naziv=data.naziv;
        this.id=data.id;
        this.adresa= data.adresa;
        this.grad= data.grad;
        this.email= data.email;
        this.telefon= data.telefon;
        this.opis= data.opis;
        this.radnoVrem= data.radnoVreme;
        this.vremeDostave= data.vremeDostave;
        this.cenaDostave= data.cenaDostave;
        this.limitDostave= data.limitDostave;
        this.kapacitet= data.kapacitet;
        this.brSlobodnih= data.slobodnaMesta;
        this.komentari=data.komentari;
        this.kategorije=data.kategorije;
        this.prosecnaOcena=data.prosecnaOcena;
        this.jela=data.jela;
        this.dodatak=data.dodaci;
        this.Osvezi();
        }
      })
      .catch((error) => console.error("Greska", error));
    }
    oceniRestoran(ocena){
      fetch("https://localhost:7284/Restoran/OceniRestoran/"+ r.email+"/"+ocena, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + sessionStorage.getItem("token")
        }
      })
        .then((p) => {
          if (p.ok) {
            alert("Uspesno ocenjivanje");
          } else {
            alert("Greska kod ocenjivanja");
          }
        })
        .catch((p) => {
          alert("Greška sa konekcijom.");
        });
    }
    posaljiNarudzbinu(listaJela,listaDodatak,email){
      var napomena= document.getElementById("Napomena").value;
      fetch("https://localhost:7284/Slavko//Slavko/dodajNarudzbinu", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + sessionStorage.getItem("token")
        },
        body: JSON.stringify({
          napomena: napomena,
          jelaID :listaJela,
          emailRestoran: email,
          dodaciID: listaDodatak
        })
      })
        .then((p) => {
          if (p.ok) {
            alert("Uspesno narucivanje");
          } else {
            alert("Greska kod narucivanja");
          }
        })
        .catch((p) => {
          alert("Greška sa konekcijom.");
        });
    }
}