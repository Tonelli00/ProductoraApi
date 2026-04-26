import {createHomeCards} from "./Js/EventCard/HomeCards.js";
import { EventsCards } from "./Js/EventCard/EventsCards.js";
import { loadEventDetail } from "./Js/EventDetails/LoadEventDetails.js";
document.addEventListener("DOMContentLoaded", () => {
  
const path = window.location.pathname;

if (path.includes("index.html")) {
  createHomeCards();
}

if (path.includes("Events.html")) {
  EventsCards();
}

if (path.includes("Event.html")) {
  loadEventDetail();
}

});