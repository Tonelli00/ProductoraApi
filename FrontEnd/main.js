import {createHomeCards} from "./Js/EventCard/HomeCards.js";
import { EventsCards } from "./Js/EventCard/EventsCards.js";
import { loadEventDetail } from "./Js/EventDetails/LoadEventDetails.js";
import { initRegisterModal } from "./Js/UserForms/CreateUserForm.js";
import { initLoginModal } from "./Js/UserForms/LoginUserForm.js";
import { updateNav } from "./Js/UserForms/LoginUserForm.js";
import { ReservationList } from "./Js/User/UserReservations/ReservationList.js";
document.addEventListener("DOMContentLoaded", () => {
  
const path = window.location.pathname;

if (path.includes("index.html")) {
  createHomeCards();
  initRegisterModal();
  initLoginModal();
  updateNav();
}

if (path.includes("Events.html")) {
  EventsCards();
  initRegisterModal();
  initLoginModal();
  updateNav();
}

if (path.includes("Event.html")) {
  loadEventDetail();
  updateNav();
}
if (path.includes("Reservations.html")) {
  ReservationList();
}


});