import { getEvent } from "../Event/GetEvents.js";
import { initLoginModal } from "../UserForms/LoginUserForm.js";
import { CreateEventMap,syncSeats} from "./EventMap/CreateEventMap.js";

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
  const userId = localStorage.getItem("UserId");

  if (!userId) {
    seatMap.innerHTML = `
        <div class="flex flex-col items-center gap-3 py-12 text-center">
            <p class="text-sm text-gray-500">Necesitás iniciar sesión para ver y reservar asientos.</p>
            <button id="btn-login-event" class="px-4 py-2 text-sm bg-black text-white rounded hover:bg-gray-800 transition">
                Iniciar sesión
            </button>
        </div>
    `;
    initLoginModal();
    document.getElementById("btn-login-event").addEventListener("click", () => {
        document.getElementById("modal-login").classList.remove("hidden");
    });
}
else{
      const map = CreateEventMap(event,userId)
      seatMap.appendChild(map);

      setInterval(async () => {
        const updatedEvent = await getEvent(eventId);

        syncSeats(updatedEvent);

      }, 3000);
    }
  
}