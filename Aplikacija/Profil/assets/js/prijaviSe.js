function Login(usern, pass){
    fetch("https://localhost:7284/Account/Login", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      email: usern,
      password: pass,
    }),
  })
    .then((response) => response.json())
    .then((data) => {
      console.log(data);
      if (data.title == "Unauthorized") alert("Lose korisnicko ime ili sifra.");
      else {
        
        /*if (data.odobren == false && data.role == "Restoran") {
          alert("Vas nalog jos uvek nije odobren");
          location.href = "restoran.html";
          return;
        }*/
        sessionStorage.setItem("username", usern);
        console.log(data);
        sessionStorage.setItem("token", data.token);
        sessionStorage.setItem("role", data.role);


        if (data.role == "Korisnik") {
            
          location.href = "index.html";
        } else if (data.role == "Restoran"){        
         location.href = "Restoran.html";
        }
        else if (data.role == "Admin"){
        
          location.href = "admin.html";
        }
        else{
            
        }
      }
    })
    .catch((error) => console.error("Greska sa prijavljivanjem", error));
}
if (
    sessionStorage.getItem("token") == null ||
    sessionStorage.getItem("token") == ""
  ) {
 
    var d = document.getElementById("login");
  
    d.addEventListener("click", Prijavi);
    function Prijavi() {
        Login(
         document.getElementById("usernameLogin").value,
        document.getElementById("passwordLogin").value
       );
    };
  } else if (sessionStorage.getItem("role") == "Korisnik") {
    location.href = "index.html";
  } else if (sessionStorage.getItem("role") == "Restoran") {
    location.href = "restoran.html";
  }
    else if (sessionStorage.getItem("role") == "Admin") {
    alert("Vec ste prijavljeni");
    location.href = "administrator.html";
  }
  function registerKorisnik() {
    let ime = document.getElementById("imeRegister").value;
    let prezime = document.getElementById("prezimeRegister").value;
    let email = document.getElementById("emailRegister").value;
    let pass = document.getElementById("passwordRegister").value;
    let confPass = document.getElementById("confirmLReg").value;
    let adresa = document.getElementById("adresa").value;
  let grad = document.getElementById("grad").value;
    let telefon = document.getElementById("telefon").value;
    if (pass != confPass) {
      alert("Sifre se ne slazu!");
      return;
    }
    fetch("https://localhost:7284/Account/registerKorisnik", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        email: email,
        password: pass,
        confirmPassword: confPass,
        adresa: adresa,
        ime: ime,
        grad: grad,
        prezime: prezime,
        telefon: telefon,
        
      }),
    })
      .then((p) => {
        if (p.ok) {
          alert("Korisnik uspesno registrovan");
          location.href="index.html";
        } else {
          alert("Postoji korisnik sa takvim emailom");
        }
      })
      .catch(() => {
        alert("Greska sa konekcijom");
      });
  }
  let btn = document.getElementById("registrujSe");
btn.addEventListener("click", function () {
  registerKorisnik();
});
