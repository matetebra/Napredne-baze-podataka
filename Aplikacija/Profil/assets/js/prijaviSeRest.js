function registerRestoran() {
    let naziv = document.getElementById("Naziv").value;
    let adresa = document.getElementById("AdresaRes").value;
    let telefon = document.getElementById("TelefonRes").value;
    let grad = document.getElementById("grad").value;
    let opis = document.getElementById("Opis").value;
    let email = document.getElementById("emailRegister").value;
    let pass = document.getElementById("passwordRegister").value;
    let confPass = document.getElementById("confirmLReg").value;
    let kapacitet = document.getElementById("Kapacitet").value;
  
    console.log(pass);
    console.log(confPass);
    if (pass != confPass) {
      alert("Sifre se ne slazu!");
      return;
    }
fetch("https://localhost:7284/Account/registerRestoran", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        naziv: naziv,
        adresa: adresa,
        email: email,
        telefon: telefon,
        opis: opis,
        grad:grad,
        kapacitet:kapacitet,
        password: pass,
        confirmPassword: confPass,
      }),
    })
      .then((p) => {
        if (p.ok) {
          alert("Restoran uspesno registrovan");
        } else {
          alert("Postoji student sa takvim emailom");
        }
      })
      .catch(() => {
        alert("Greska sa konekcijom");
      });
  }
  let btn1 = document.getElementById("registrujSe");
btn1.addEventListener("click", function () {
  registerRestoran();
});