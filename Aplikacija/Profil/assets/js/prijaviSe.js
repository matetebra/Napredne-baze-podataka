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
    //mslm da treba local ?
    var d = document.getElementById("login");
  
    d.addEventListener("click", Prijavi);
    function Prijavi() {
        //console.log(document.getElementById("usernameLogin").value)
    //   Login(
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