import { restoran } from "./restoran.js";
import { korisnik } from "./korisnik.js";

export class restorani{
    constructor(){
      this.restorani=[];
    }   
ucitajRestorane(){
  fetch("https://localhost:7284/Slavko/GetRestourants", {
            method: "GET",
            headers: {
              "Content-Type": "application/json",
              Authorization: "Bearer " + sessionStorage.getItem("token"),
            },
          })
                  
      .then((p) => {
      p.json().then((data) => {
        data.forEach((element) => {
          var r = new restoran();
          r.naziv=element.naziv;
          r.adresa=element.adresa;
          r.email=element.email;
          r.telefon=element.telefon;
          this.restorani.push(r);
        });
        this.crtajsveRestorane();
      });
    });
  }

crtajsveRestorane(){
  const host=document.getElementById("restorani");
  this.restorani.forEach((rest)=>{
      rest.crtajRestoran(host)
  });
}
crtajNeodobrene(){
  const host=document.getElementById("radiSeOvde");
  this.restorani.forEach((rest)=>{
    rest.crtajNeodobreniRestoran(host);
});
}
preuzmiNeodobrene(){
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
                  p1.email=element.email;
                  p1.naziv=element.naziv;
                  p1.telefon=element.telefon;
                  p1.adresa=element.adresa;
                  this.restorani.push(p1);
                });
                this.crtajNeodobrene();
              }
            })
    }
  }