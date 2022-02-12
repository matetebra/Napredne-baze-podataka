import { restoran } from "./restoran.js";
import { korisnik } from "./korisnik.js";
import { restorani } from "./restorani.js";
var res = new restorani();
res.ucitajRestorane();

var dugm = document.getElementById("homebtn");
dugm.hidden = true;
//res.ucitajBukmarkovaneRestorane();
if (
  sessionStorage.getItem("token") != null &&
  sessionStorage.getItem("token") != ""
) {
  var k = new korisnik();
  k.ucitajKorisnika();
  var dugme = document.getElementById("omiljeniRes");

  dugme.addEventListener("click", bukmarkovani);

  var dugme1 = document.getElementById("sacuvaneNar");

  dugme1.addEventListener("click", sacuvani);

  var dugme3 = document.getElementById("SortirajPoKategoriji");
  dugme3.addEventListener("click", PoKategoriji);
}
document.getElementById("sortingOptionID").options[0].selected = true;

export function sortNajm() {
  console.log("sdv");
  const host = document.getElementById("restorani");
  host.innerHTML = "";

  res.sortirajPoNajnizoj();
  var dugm = document.getElementById("homebtn");
  dugm.hidden = false;
  dugm.addEventListener("click", vrati);
}
export function sortNajv() {
  console.log("sdvsdfsdf");
  const host = document.getElementById("restorani");
  host.innerHTML = "";
  res.sortirajPoNajvisoj();
  var dugm = document.getElementById("homebtn");
  dugm.hidden = false;
  dugm.addEventListener("click", vrati);
}
function PoKategoriji() {
  const host = document.getElementById("restorani");
  host.innerHTML = "";
  res.sortirajPoKategoriji();
  var dugm = document.getElementById("homebtn");
  dugm.hidden = false;
  dugm.addEventListener("click", vrati);
}
function bukmarkovani() {
  const host = document.getElementById("restorani");
  host.innerHTML = "";
  res.ucitajBukmarkovaneRestorane();
  var dugm = document.getElementById("homebtn");
  dugm.hidden = false;
  dugm.addEventListener("click", vrati);
}
function vrati() {
  const host = document.getElementById("restorani");
  host.innerHTML = "";
  res.ucitajRestorane();
  var dugm = document.getElementById("homebtn");
  dugm.hidden = true;
}
function sacuvani() {
  const host = document.getElementById("restorani");
  host.innerHTML = "";
  res.ucitajPrethodnePorudzbine();

  dugm.addEventListener("click", vrati);
}
