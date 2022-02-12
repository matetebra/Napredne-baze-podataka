export class restoran {
  constructor(
    naziv,
    adresa,
    grad,
    email,
    telefon,
    opis,
    radnoVreme,
    vremeDostave,
    cenaDostave,
    limitDostave,
    kapacitet,
    brSlobodnih,
    cena,
    o
  ) {
    this.naziv = naziv;
    this.adresa = adresa;
    this.grad = grad;
    this.email = email;
    this.telefon = telefon;
    this.opis = opis;
    this.radnoVrem = radnoVreme;
    this.vremeDostave = vremeDostave;
    this.cenaDostave = cenaDostave;
    this.limitDostave = limitDostave;
    this.kapacitet = kapacitet;
    this.brSlobodnih = brSlobodnih;
    this.cena = cena;
    this.prosecnaOcena = o;
    this.komentari = [];
    this.kategorije = [];
    this.jela = [];
    this.dodatak = [];
    this.id = null;
    this.kontRestoran = null;
    this.jelaNar = [];
    this.dodNar = [];
    this.jelapom = [];
    this.dodatakpom = [];
    this.rezervacijepom = [];
  }
  crtajRestoran(host) {
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
    pom.classList.add("marg");
    host.appendChild(pom);
    host.classList.add("marg");

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
    mejl.innerHTML = "Email: " + this.email;
    pom.appendChild(mejl);

    const telefon = document.createElement("h2");
    telefon.classList.add("card-title-a");
    telefon.classList.add("naziv");
    telefon.innerHTML = "Telefon: " + this.telefon;
    pom.appendChild(telefon);

    var pogled = document.createElement("button");
    pogled.classList.add("btn");
    pogled.classList.add("btn-a");
    pogled.classList.add("col-md-1");
    pogled.innerHTML = "Pogledaj";
    pogled.id = this.email;
    host.appendChild(pogled);
    pogled.addEventListener("click", function () {
      sessionStorage.setItem("restoran", pogled.id);
      location.href = "restoran.html";
    });
  }
  crtajNeodobreniRestoran(host) {
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
    pom.classList.add("marg");
    host.appendChild(pom);
    host.classList.add("marg");

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
    mejl.innerHTML = "Email: " + this.email;
    pom.appendChild(mejl);

    const telefon = document.createElement("h2");
    telefon.classList.add("card-title-a");
    telefon.classList.add("naziv");
    telefon.innerHTML = "Telefon: " + this.telefon;
    pom.appendChild(telefon);

    var pogled = document.createElement("button");
    pogled.classList.add("btn");
    pogled.classList.add("btn-danger");
    pogled.innerHTML = "Odobri";
    pom.appendChild(pogled);
    pogled.id = this.email;
    pogled.addEventListener("click", function () {
      console.log(pogled.id);
      fetch("https://localhost:7284/Restoran/OdobriRestoran/" + pogled.id, {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
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
    });
  }
  Osvezi() {
    var d1 = (document.getElementById("adresa").innerHTML = this.adresa);
    d1 = document.getElementById("opis").innerHTML = this.opis;
    d1 = document.getElementById("kapacitet").innerHTML = this.kapacitet;
    d1 = document.getElementById("cenaDostave").innerHTML = this.cenaDostave;
    d1 = document.getElementById("vremeDostave").innerHTML = this.vremeDostave;
    d1 = document.getElementById("limitDostave").innerHTML = this.limitDostave;
    d1 = document.getElementById("mail").innerHTML = this.email;
    d1 = document.getElementById("phone").innerHTML = this.telefon;
    d1 = document.getElementById("ime").innerHTML = this.naziv;
    d1 = document.getElementById("prosecnaOcena").innerHTML =
      this.prosecnaOcena;
  }
  crtajJela(host) {
    //<div class="pic"><img src="assets/img/team/team-1.jpg" class="img-fluid" alt=""></div> //za sliku
    if (!host) throw new Error("Greska u hostu");
    for (let i = 0; i < 4; i++) {
      this.jela.forEach((e) => {
        var d1 = document.createElement("div");
        d1.className = "col-lg-4 member";
        host.appendChild(d1);
        var d2 = document.createElement("div");
        d2.classList = "d-flex align-items-start";
        d1.appendChild(d2);
        var d3 = document.createElement("div");
        d3.className = "member-info";
        d2.appendChild(d3);
        var d4 = document.createElement("h5");
        d4.innerHTML = e.naziv;
        d3.appendChild(d4);
        var d5 = document.createElement("h6");
        d5.innerHTML = "Cena: " + e.cena;
        d3.appendChild(d5);
        var d6 = document.createElement("h6");
        d6.innerHTML = "Gramaza: " + e.gramaza;
        d3.appendChild(d6);
        d6 = document.createElement("h6");
        d6.innerHTML = e.opis;
        d3.appendChild(d6);
        var l = document.createElement("h6");
        l.innerHTML = "";
        e.nazivNamirnica.forEach((n) => {
          l.innerHTML = l.innerHTML + " " + n;
        });
        d3.appendChild(l);
        var pogled = document.createElement("button");
        pogled.classList.add("btn");
        pogled.classList.add("btn-danger");
        pogled.innerHTML = "+";
        pogled.id = e.id;
        d3.appendChild(pogled);
        pogled.addEventListener("click", function () {
          var j = document.getElementById("jelaNam");
          j.innerHTML = j.innerHTML + pogled.id + ",";
          alert("Dodato u korpu");
          console.log(j.innerHTML);
        });
      });
    }
  }
  crtajDodatke(host) {
    if (!host) throw new Error("Greska u hostu");
    this.dodatak.forEach((e) => {
      var d1 = document.createElement("div");
      d1.className = "col-lg-4 member";
      host.appendChild(d1);
      var d2 = document.createElement("div");
      d2.classList = "d-flex align-items-start";
      d1.appendChild(d2);
      var d3 = document.createElement("div");
      d3.className = "member-info";
      d2.appendChild(d3);
      var d4 = document.createElement("h5");
      d4.innerHTML = e.naziv;
      d3.appendChild(d4);
      var d5 = document.createElement("h6");
      d5.innerHTML = "Cena: " + e.cena;
      d3.appendChild(d5);
      var pogled = document.createElement("button");
      pogled.classList.add("btn");
      pogled.classList.add("btn-danger");
      pogled.innerHTML = "+";
      pogled.id = e.id;
      d3.appendChild(pogled);
      pogled.addEventListener("click", function () {
        var j = document.getElementById("dodNam");
        j.innerHTML = j.innerHTML + pogled.id + ",";
        alert("Dodato u korpu");
      });
    });
  }
  crtajPorudzbine(host){
    if (!host) throw new Error("Greska u hostu");
    const pom = document.createElement("div");
    this.dodajNamirniceIDodatke();
  }
  dodajNamirniceIDodatke(){
    var labJ=document.getElementById("jelaNam").value;
    var labD=document.getElementById("dodNam").value;
    console.log(labJ);
    var labJela=labJ.split(",");
    var labDod=labD.split(",");
    console.log(labJ);
    console.log(labDod);
  }
  crtajKomentari(host) {
    if (!host) throw new Error("Greska u hostu");
    this.komentari.forEach((e) => {
      var d1 = document.createElement("div");
      d1.className = "col-lg-12 member";
      host.appendChild(d1);
      var d2 = document.createElement("div");
      d2.classList = "d-flex align-items-start";
      d1.appendChild(d2);
      var d3 = document.createElement("div");
      d3.className = "member-info";
      d2.appendChild(d3);
      var d4 = document.createElement("h5");
      d4.innerHTML = e.email;
      d3.appendChild(d4);
      var d4 = document.createElement("h6");
      d4.innerHTML = e.tekst;
      d3.appendChild(d4);
    });
  }
  preuzmiPodatke(emails) {
    fetch(
      "https://localhost:7284/Slavko/GetAllRestourantInformations/" + emails,
      {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
      }
    )
      .then((response) => response.json())
      .then((data) => {
        if (data.title == "Unauthorized") alert("Greska.");
        else {
          this.naziv = data.naziv;
          this.id = data.id;
          this.adresa = data.adresa;
          this.grad = data.grad;
          this.email = data.email;
          this.telefon = data.telefon;
          this.opis = data.opis;
          this.radnoVrem = data.radnoVreme;
          this.vremeDostave = data.vremeDostave;
          this.cenaDostave = data.cenaDostave;
          this.limitDostave = data.limitDostave;
          this.kapacitet = data.kapacitet;
          this.brSlobodnih = data.slobodnaMesta;
          this.komentari = data.komentari;
          this.kategorije = data.kategorije;
          this.prosecnaOcena = data.prosecnaOcena;
          this.jela = data.jela;
          this.dodatak = data.dodaci;
          this.Osvezi();
          return this;
        }
      })
      .catch((error) => console.error("Greska", error));
  }
  preuzmi(emails) {
    fetch(
      "https://localhost:7284/Slavko/GetAllRestourantInformations/" + emails,
      {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
      }
    ).then((data) => {
      this.naziv = data.naziv;
      this.id = data.id;
      this.adresa = data.adresa;
      this.grad = data.grad;
      this.email = data.email;
      this.telefon = data.telefon;
      this.opis = data.opis;
      this.radnoVrem = data.radnoVreme;
      this.vremeDostave = data.vremeDostave;
      this.cenaDostave = data.cenaDostave;
      this.limitDostave = data.limitDostave;
      this.kapacitet = data.kapacitet;
      this.brSlobodnih = data.slobodnaMesta;
      this.komentari = data.komentari;
      this.kategorije = data.kategorije;
      this.prosecnaOcena = data.prosecnaOcena;
      this.jela = data.jela;
      this.dodatak = data.dodaci;
      this.Osvezi();
      return this;
    });
  }
  oceniRestoran(ocena) {
    fetch(
      "https://localhost:7284/Restoran/OceniRestoran/" +
        this.email +
        "/" +
        ocena,
      {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
      }
    )
      .then((p) => {
        if (p.ok) {
          alert("Uspesno ocenjivanje");
        } else {
          alert("Vec ste ocenili");
        }
      })
      .catch((p) => {
        alert("Greška sa konekcijom.");
      });
  }
  crtajDodajJelo(host) {
    var d = document.createElement("div");
    host.appendChild(d);

    var label = document.createElement("label");
    label.innerHTML = "Naziv: ";
    label.style = " margin: 5px";
    d.appendChild(label);
    var input = document.createElement("input");
    input.style = " margin: 5px";
    input.id = "naziv";
    d.appendChild(input);

    var d1 = document.createElement("div");
    host.appendChild(d1);
    var label = document.createElement("label");
    label.innerHTML = "Kategorija: ";
    label.style = " margin: 5px";
    d1.appendChild(label);
    var input = document.createElement("input");
    input.style = " margin: 5px";
    input.id = "kategorija";
    d1.appendChild(input);

    var d2 = document.createElement("div");
    host.appendChild(d2);
    var label = document.createElement("label");
    label.innerHTML = "Gramaza: ";
    label.style = " margin: 5px";
    d2.appendChild(label);
    var input = document.createElement("input");
    input.style = " margin: 5px";
    input.id = "gramaza";
    d2.appendChild(input);

    var d3 = document.createElement("div");
    host.appendChild(d3);
    var label = document.createElement("label");
    label.innerHTML = "Opis: ";
    label.style = " margin: 5px";
    d3.appendChild(label);
    var input = document.createElement("input");
    input.style = " margin: 5px";
    input.id = "opiss";
    d3.appendChild(input);

    var d4 = document.createElement("div");
    host.appendChild(d4);
    var label = document.createElement("label");
    label.innerHTML = "Slika: ";
    label.style = " margin: 5px";
    d4.appendChild(label);
    var input = document.createElement("input");
    input.style = " margin: 5px";
    input.id = "slika";
    d4.appendChild(input);

    var d5 = document.createElement("div");
    host.appendChild(d5);
    var label = document.createElement("label");
    label.innerHTML = "Cena: ";
    label.style = " margin: 5px";
    d5.appendChild(label);
    var input = document.createElement("input");
    input.style = " margin: 5px";
    input.id = "cena";
    d5.appendChild(input);

    var d6 = document.createElement("div");
    host.appendChild(d6);
    var label = document.createElement("label");
    label.innerHTML = "Namirnice: ";
    label.style = " margin: 5px";
    d6.appendChild(label);
    var input = document.createElement("input");
    input.id = "namirnice";
    var d6 = document.createElement("div");
    host.appendChild(d6);
    d6.appendChild(input);
    var label1 = document.createElement("label");
    label1.innerHTML = "Namirnice odvojiti iskljucivo ',' ";
    label1.style = " margin: 5px";
    d6.appendChild(label1);

    var dugme = document.createElement("button");
    host.appendChild(dugme);
    dugme.innerHTML = "Dodaj";
    dugme.style = " margin: 5px";
    dugme.classList = "btn btn-danger";

    //var namir=nam.split(",")
    dugme.addEventListener("click", function () {
      var naz = document.getElementById("naziv").value;
      var kat = document.getElementById("kategorija").value;
      var gram = document.getElementById("gramaza").value;
      var op = document.getElementById("opiss").value;
      var sl = document.getElementById("slika").value;
      var cen = document.getElementById("cena").value;
      var nam = document.getElementById("namirnice").value;
      var namir = nam.split(",");
      console.log(naz);
      console.log(kat);
      console.log(gram);
      console.log(op);
      console.log(sl);
      console.log(cen);
      console.log(nam);
      console.log(namir);
      fetch("https://localhost:7284/Restoran/AddMeal", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
        body: JSON.stringify({
          naziv: naz,
          kategorija: kat,
          gramaza: gram,
          opis: op,
          slika: sl,
          cena: cen,
          nazivNamirnica: namir,
        }),
      })
        .then((p) => {
          if (p.ok) {
            alert("Uspesno dodavanje jela");
            komentari();
          } else {
            alert("Vec ste ocenili");
          }
        })
        .catch(() => {
          alert("Greska sa konekcijom");
        });
      //}
    });
  }
  crtajObrisiJelo(host) {
    fetch("https://localhost:7284/Restoran/GetMeals", {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + sessionStorage.getItem("token"),
      },
    }).then((p) => {
      p.json().then((data) => {
        data.forEach((element) => {
          var j = new jelo();
          j.naziv = element.naziv;
          j.kategorija = element.kategorija;
          j.gramaza = element.gramaza;
          j.opis = element.opis;
          j.slika = element.slika;
          j.cena = element.cena;
          j.namirnice.forEach((nam) => {
            j.dodajNam(nam);
          });
          this.jelapom.push(j);
          console.log(this.jelapom);
        });
        this.crtajsvaJela(host);
      });
    });
  }
  crtajObrisiDodatatk(host) {
    fetch("https://localhost:7284/Restoran/GetDodatatk", {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + sessionStorage.getItem("token"),
      },
    }).then((p) => {
      p.json().then((data) => {
        data.forEach((element) => {
          var j = new dodatak();
          j.naziv = element.naziv;
          j.cena = element.cena;
          this.dodatakpom.push(j);
        });
        this.crtajsveDodatke(host);
      });
    });
  }
  crtajsveDodatke(host) {
    this.dodatakpom.forEach((j) => {
      j.crtajDodatak(host);
    });
  }
  crtajsvaJela(host) {
    this.jelapom.forEach((j) => {
      j.crtajJelo(host);
    });
  }
  crtajDodajDodatak(host) {
    var d = document.createElement("div");
    host.appendChild(d);

    var label = document.createElement("label");
    label.innerHTML = "Naziv: ";
    label.style = " margin: 5px";
    d.appendChild(label);
    var input = document.createElement("input");
    input.style = " margin: 5px";
    input.id = "naziv";
    d.appendChild(input);

    var d1 = document.createElement("div");
    host.appendChild(d1);
    var label = document.createElement("label");
    label.innerHTML = "Cena: ";
    label.style = " margin: 5px";
    d1.appendChild(label);
    var input = document.createElement("input");
    input.style = " margin: 5px";
    input.id = "cena";
    d1.appendChild(input);

    var dugme = document.createElement("button");
    host.appendChild(dugme);
    dugme.innerHTML = "Dodaj";
    dugme.style = " margin: 5px";
    dugme.classList = "btn btn-danger";

    //var namir=nam.split(",")
    dugme.addEventListener("click", function () {
      var naz = document.getElementById("naziv").value;
      var cen = document.getElementById("cena").value;
      fetch("https://localhost:7284/Restoran/AddDodatak", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
        body: JSON.stringify({
          naziv: naz,
          cena: cen,
        }),
      })
        .then((p) => {
          if (p.ok) {
            alert("Uspesno dodavanje jela");
            komentari();
          } else {
            alert("Ne mozete komentarisati.");
          }
        })
        .catch(() => {
          alert("Greska sa konekcijom");
        });
      //}
    });
  }
  brisiRezervaciju(host) {
    fetch("https://localhost:7284/Slavko/VratiRezervacije", {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + sessionStorage.getItem("token"),
      },
    }).then((p) => {
      p.json().then((data) => {
        data.forEach((element) => {
          var j = new rezervacija();
          j.id = element.IDRezervacije;
          j.email = element.EmailKorisnika;
          j.telefon = element.TelefonKorisnika;
          j.vreme = element.Vreme;
          j.brojMesta = element.brojMesta;
          this.rezervacijepom.push(j);
        });
        this.crtajsveRezervacije(host);
      });
    });
  }
  crtajsveRezervacije(host) {
    this.rezervacijepom.forEach((j) => {
      j.crtajRezervaciju(host);
    });
  }
}
export class jelo {
  constructor(naziv, kategorija, gramaza, opis, slika, cena) {
    this.naziv = naziv;
    this.kategorija = kategorija;
    this.gramaza = gramaza;
    this.opis = opis;
    this.slika = slika;
    this.cena - cena;
    this.namirnice = [];
  }
  dodajNam(n) {
    this.namirnice.push(n);
  }
  crtajJelo(host) {
    var d = document.createElement("div");
    host.appendChild(d);

    var label = document.createElement("label");
    label.innerHTML = this.naziv;
    label.style = " margin: 5px";
    d.appendChild(label);

    var dugme = document.createElement("button");
    d.appendChild(dugme);
    dugme.innerHTML = "Obrisi jelo";
    dugme.id = this.naziv;
    dugme.style = " margin: 5px";
    dugme.classList = "btn btn-danger";
    dugme.addEventListener("click", function () {
      fetch("https://localhost:7284/Restoran/DeleteJelo/" + this.naziv, {
        method: "DELETE",
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
        body: JSON.stringify({}),
      })
        .then((p) => {
          if (p.ok) {
            alert("Jelo je uspesno obrisan");
          } else {
            alert("Greska kod brisanja");
          }
        })
        .catch((p) => {
          alert("Greška sa konekcijom.");
        });
    });
  }
}
export class dodatak {
  constructor(naziv, cena) {
    this.naziv = naziv;
    this.cena - cena;
  }
  crtajDodatak(host) {
    var d = document.createElement("div");
    host.appendChild(d);

    var label = document.createElement("label");
    label.innerHTML = this.naziv;
    label.style = " margin: 5px";
    d.appendChild(label);
    var label = document.createElement("label");
    label.innerHTML = "  " + this.cena;
    label.style = " margin: 5px";
    d.appendChild(label);

    var dugme = document.createElement("button");
    d.appendChild(dugme);
    dugme.innerHTML = "Obrisi jelo";
    dugme.id = this.naziv;
    dugme.style = " margin: 5px";
    dugme.classList = "btn btn-danger";
    dugme.addEventListener("click", function () {
      fetch("https://localhost:7284/Restoran/DeleteDodatak/" + this.naziv, {
        method: "DELETE",
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
        body: JSON.stringify({}),
      })
        .then((p) => {
          if (p.ok) {
            alert("Jelo je uspesno obrisan");
          } else {
            alert("Greska kod brisanja");
          }
        })
        .catch((p) => {
          alert("Greška sa konekcijom.");
        });
    });
  }
}
export class rezervacija {
  constructor(id, email, brojMesta, vreme, telefon) {
    this.id = id;
    this.email = email;
    this.brojMesta = brojMesta;
    this.vreme = vreme;
    this.telefon = telefon;
  }
  crtajRezervaciju() {
    var d = document.createElement("div");
    host.appendChild(d);

    var label = document.createElement("label");
    label.innerHTML = this.email;
    label.style = " margin: 5px";
    d.appendChild(label);
    var label1 = document.createElement("label");
    label1.innerHTML = "  " + this.telefon;
    label1.style = " margin: 5px";
    d.appendChild(label1);
    var label2 = document.createElement("label");
    label2.innerHTML = "  " + this.vreme;
    label2.style = " margin: 5px";
    d.appendChild(label2);

    var dugme = document.createElement("button");
    d.appendChild(dugme);
    dugme.innerHTML = "Obrisi rezervaciju";
    dugme.id = this.naziv;
    dugme.style = " margin: 5px";
    dugme.classList = "btn btn-danger";
    dugme.addEventListener("click", function () {
      fetch("https://localhost:7284/Slavko/obrisiRezervaciju/" + this.id, {
        method: "DELETE",
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
        body: JSON.stringify({}),
      })
        .then((p) => {
          if (p.ok) {
            alert("Jelo je uspesno obrisan");
          } else {
            alert("Greska kod brisanja");
          }
        })
        .catch((p) => {
          alert("Greška sa konekcijom.");
        });
    });
  }
}

export class porudzbina {
  constructor(id, email, brojMesta, vreme, telefon) {
    this.id = id;
    this.email = email;
    this.brojMesta = brojMesta;
    this.vreme = vreme;
    this.telefon = telefon;
  }
  crtajPorudzbinu() {
    var d = document.createElement("div");
    host.appendChild(d);

    var label = document.createElement("label");
    label.innerHTML = this.email;
    label.style = " margin: 5px";
    d.appendChild(label);
    var label1 = document.createElement("label");
    label1.innerHTML = "  " + this.telefon;
    label1.style = " margin: 5px";
    d.appendChild(label1);
    var label2 = document.createElement("label");
    label2.innerHTML = "  " + this.vreme;
    label2.style = " margin: 5px";
    d.appendChild(label2);

    var dugme = document.createElement("button");
    d.appendChild(dugme);
    dugme.innerHTML = "Obrisi rezervaciju";
    dugme.id = this.naziv;
    dugme.style = " margin: 5px";
    dugme.classList = "btn btn-danger";
    dugme.addEventListener("click", function () {
      fetch("https://localhost:7284/Slavko/obrisiRezervaciju/" + this.id, {
        method: "DELETE",
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + sessionStorage.getItem("token"),
        },
        body: JSON.stringify({}),
      })
        .then((p) => {
          if (p.ok) {
            alert("Jelo je uspesno obrisan");
          } else {
            alert("Greska kod brisanja");
          }
        })
        .catch((p) => {
          alert("Greška sa konekcijom.");
        });
    });
  }
}
