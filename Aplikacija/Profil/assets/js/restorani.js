
export class restorani{
    constructor(){
        
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
                  this.dodajRestoran(p1);
                });
                this.crtajNeodobrene(document.getElementById("crtajPredmeteprofesora"));
              }
            })
    }
}