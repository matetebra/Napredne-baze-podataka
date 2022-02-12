export class narudzbina{
    constructor(naziv,cena){
        this.jelo=[];
        this.dodaci=[];
        this.naziv=naziv;
        this.cena=cena;

    }
    dodajJelo(j) {
        this.jelo.push(j);
      }
    dodajDodaci(j) {
        this.dodaci.push(j);
      }
crtajNarudzbinu(host){
  if (!host) throw new Error("Greska u hostu");
        var pom = document.createElement("div");
        pom.classList.add("col-md-3");
        pom.classList.add("col-lg-3");
        pom.classList.add("d-md-flex");
        pom.classList.add("align-items-md-stretch");

        pom.classList.add("card-box-a");
        pom.classList.add("razmak");
        pom.classList.add("row");
        pom.classList.add("marg")
        host.appendChild(pom);
        host.classList.add("marg")

        const pom1 = document.createElement("div");
        pom1.classList.add("card-overlay");
        pom1.classList.add("card-overlay-a-content");
        pom.appendChild(pom1);
        
        const naziv = document.createElement("h1");
        naziv.classList.add("card-title-a");
        naziv.classList.add("naziv");
        naziv.innerHTML ="Naziv restorana: "+ this.naziv;
        pom.appendChild(naziv);

        const mejl = document.createElement("h2");
        mejl.classList.add("card-title-a");
        mejl.classList.add("naziv");
        mejl.innerHTML ="Cena: "+this.cena;
        pom.appendChild(mejl);
        const nasl = document.createElement("h1");
        nasl.innerHTML="Jela:";
        pom.appendChild(nasl);
        this.jelo.forEach((j)=>{
          const i = document.createElement("h2");
          i.innerHTML=j;
          pom.appendChild(i);
      });
      
        if(!this.dodaci.length==0)
        {
          const nasl1 = document.createElement("h1");
        nasl1.innerHTML="Dodaci:";
        pom.appendChild(nasl1);
          this.dodaci.forEach((j)=>{
          const i = document.createElement("h2");
          i.innerHTML=j;
          pom.appendChild(i);
      });
        }
        

        
        /*var pogled = document.createElement("button");
        pogled.classList.add("btn");
        pogled.classList.add("btn-a");
        pogled.classList.add("col-md-3");
        pogled.innerHTML = "Pogledaj";
        pogled.id=this.email;
        host.appendChild(pogled);*/
}
    }