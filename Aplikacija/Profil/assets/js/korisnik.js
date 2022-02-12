
export class korisnik{
    constructor(email, ime, prezime, adresa, grad, telefon){
        this.email=email;
        this.adresa=adresa;
        this.ime=ime;
        this.prezime=prezime;
        this.grad=grad;
        this.telefon=telefon;
        this.omiljeniRestorani=[];
        this.porudzbina=[];
        this.trenutnaPorudzbina=[];

    }
    crtajInformacije(){
        var inf=document.getElementById("informacije");
        var ime=document.createElement("h1");
        ime.innerHTML=this.ime+" "+this.prezime;
        ime.classList.add("text-light");
        inf.appendChild(ime);
        var mejl=document.createElement("h1");
        mejl.innerHTML=this.email;
        mejl.classList.add("text-light");
        inf.appendChild(mejl);
        var br=document.createElement("h1");
        br.innerHTML=this.telefon;
        br.classList.add("text-light");
        inf.appendChild(br);

    }
    ucitajKorisnika(){
        this.ime="";
        this.prezime="";
        this.email="";
        this.grad="";
        this.telefon="";
      fetch("https://localhost:7284/Slavko/getUserInformaiton", {
                method: "GET",
                headers: {
                  "Content-Type": "application/json",
                  Authorization: "Bearer " + sessionStorage.getItem("token"),
                },
              })
          .then((p) => {
          p.json().then((data) => {
           
              this.ime=data.ime;
              this.prezime=data.prezime;
              this.email=data.email;
              this.grad=data.grad;
              this.telefon=data.telefon;
              
           
            this.crtajInformacije();
          });
        });
      } 

}