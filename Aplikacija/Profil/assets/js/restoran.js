export class restoran{
    constructor(naziv, adresa, grad, email, telefon, opis, radnoVreme, vremeDostave, cenaDostave, limitDostave, kapacitet, brSlobodnih ){
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
        this.komentari=[];
        this.kategorije=[];
        this.jela=[];
        this.dodatak=[];
        this.kontRestoran=null;
    }
    crtajRestoran(host){
       
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
        pogled.innerHTML = "Pogledaj";
        pogled.id=this.email;
        pom.appendChild(pogled);
        pogled.addEventListener("click",function(){
          sessionStorage.setItem("restoran")=pogled.id;
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
}