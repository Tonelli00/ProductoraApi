import { getEvent } from "../Event/GetEvents.js";
import { CreateEventMap } from "./CreateEventMap.js";

export async function loadEventDetail() {
  const eventId = localStorage.getItem("event_id");

  if (!eventId) {
    window.location.href = "Events.html";
    return;
  }

  const event = await getEvent(eventId);

  if (!event) {
    document.getElementById("seat-map-container").innerHTML =
      "<p class='text-sm text-gray-400 mt-4'>No se pudo obtener la información del evento.</p>";
    return;
  }

  const date = new Date(event.eventDate);

  document.title = event.name;
  document.getElementById("breadcrumb-name").textContent = event.name;
  document.getElementById("ev-name").textContent = event.name;
  document.getElementById("ev-venue").textContent = event.venue;

  document.getElementById("ev-date").textContent = date.toLocaleDateString("es-AR", {
    weekday: "long", day: "numeric", month: "long", year: "numeric"
  });

  document.getElementById("info-date").textContent = date.toLocaleDateString("es-AR", {
    day: "numeric", month: "long", year: "numeric"
  });

  document.getElementById("info-time").textContent =
    date.toLocaleTimeString("es-AR", { hour: "2-digit", minute: "2-digit" }) + " hs";

  document.getElementById("info-sectors").textContent =
    `${event.sectors?.length ?? 0} disponibles`;

  const badge = document.getElementById("ev-status");
  badge.textContent = event.status;
  badge.className = event.status === "Activo"
    ? "inline-flex items-center text-[11px] font-medium uppercase tracking-wide px-2.5 py-1 rounded-md bg-green-50 text-green-700"
    : "inline-flex items-center text-[11px] font-medium uppercase tracking-wide px-2.5 py-1 rounded-md bg-gray-100 text-gray-400";

  const seatMap = document.getElementById("seat-map-container");

  seatMap.appendChild(CreateEventMap(event));
  
}