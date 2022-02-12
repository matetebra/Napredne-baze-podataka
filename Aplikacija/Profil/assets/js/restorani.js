import { restoran } from "./restoran.js";
import { korisnik } from "./korisnik.js";
import { narudzbina } from "./narudzbina.js";
export class restorani {
  constructor() {
    this.restorani = [];
    this.bukmarkovani = [];
    this.stareNarudzbine = [];
  }
  ucitajRestorane() {
    this.restorani.splice(0, this.restorani.length);
    fetch("https://localhost:7284/Slavko/GetRestourants", {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + sessionStorage.getItem("token"),
      },
    }).then((p) => {
      p.json().then((data) => {
        data.forEach((element) => {
          var r = new restoran();
          r.naziv = element.naziv;
          r.adresa = element.adresa;
          r.email = element.email;
          r.telefon = element.telefon;
          this.restorani.push(r);
        });
        this.crtajsveRestorane();
      });
    });
  }

  crtajsveRestorane() {
    const host = document.getElementById("restorani");
    host.innerHTML = "";
    this.restorani.forEach((rest) => {
      rest.crtajRestoran(host);
    });
  }
  crtajNeodobrene() {
    const host = document.getElementById("radiSeOvde");
    this.restorani.forEach((rest) => {
      rest.crtajNeodobreniRestoran(host);
    });
  }
  preuzmiNeodobrene() {
    fetch("https://localhost:7284/Slavko/GetRestourantsNeodobreni", {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + sessionStorage.getItem("token"),
      },
    })
      .then((response) => response.json())
      .then((data) => {
        if (data.title == "Unauthorized")
          alert("Lose korisnicko ime ili sifra.");
        else {
          data.forEach((element) => {
            var p1 = new restoran();
            p1.email = element.email;
            p1.naziv = element.naziv;
            p1.telefon = element.telefon;
            p1.adresa = element.adresa;
            this.restorani.push(p1);
          });
          this.crtajNeodobrene();
        }
      });
  }

  ucitajBukmarkovaneRestorane() {
    this.bukmarkovani.splice(0, this.bukmarkovani.length);
    fetch("https://localhost:7284/Slavko/GetBookmarked", {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + sessionStorage.getItem("token"),
      },
    }).then((p) => {
      p.json().then((data) => {
        data.forEach((element) => {
          var r = new restoran();
          r.naziv = element.naziv;
          r.adresa = element.adresa;
          r.email = element.email;
          r.telefon = element.telefon;
          this.bukmarkovani.push(r);
        });
        this.crtajBukmarkovane();
      });
    });
  }
  crtajBukmarkovane() {
    const host = document.getElementById("restorani");
    host.innerHTML = "";
    this.bukmarkovani.forEach((rest) => {
      rest.crtajRestoran(host);
    });
  }
  ucitajPrethodnePorudzbine() {
    this.stareNarudzbine.splice(0, this.bukmarkovani.length);
    fetch("https://localhost:7284/Slavko/PrethodnePorudzbine", {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + sessionStorage.getItem("token"),
      },
    }).then((p) => {
      p.json().then((data) => {
        console.log(data);
        data.forEach((element) => {
          var r = new narudzbina();

          element.jela.forEach((el) => {
            r.dodajJelo(el.naziv);
          });
          element.dodaci.forEach((el) => {
            r.dodajDodaci(el.naziv);
          });
          r.naziv = element.naziv;
          r.cena = element.cena;
          this.stareNarudzbine.push(r);
        });

        this.crtajPorudzbine();
      });
    });
  }
  osveziRestorane() {
    this.restorani.splice(0, this.restorani.length);
    this.crtajsveRestorane();
  }
  crtajPorudzbine() {
    const host = document.getElementById("restorani");
    host.innerHTML = "";
    this.stareNarudzbine.forEach((rest) => {
      rest.crtajNarudzbinu(host);
    });
  }
  sortirajPoNajnizoj() {
    this.restorani.splice(0, this.restorani.length);
    fetch("https://localhost:7284/Restoran/SoryByLowest", {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + sessionStorage.getItem("token"),
      },
    }).then((p) => {
      p.json().then((data) => {
        data.forEach((element) => {
          var r = new restoran();
          r.naziv = element.naziv;
          r.adresa = element.adresa;
          r.email = element.email;
          r.telefon = element.telefon;
          this.restorani.push(r);
        });
        this.crtajsveRestorane();
      });
    });
  }

  sortirajPoNajvisoj() {
    this.restorani.splice(0, this.restorani.length);
    fetch("https://localhost:7284/Restoran/SoryByGreatest", {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + sessionStorage.getItem("token"),
      },
    }).then((p) => {
      p.json().then((data) => {
        data.forEach((element) => {
          var r = new restoran();
          r.naziv = element.naziv;
          r.adresa = element.adresa;
          r.email = element.email;
          r.telefon = element.telefon;
          this.restorani.push(r);
        });
        this.crtajsveRestorane();
      });
    });
  }
}
