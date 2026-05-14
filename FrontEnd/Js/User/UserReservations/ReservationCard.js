import { getSeatById } from "../../Seat/GetSeatById.js";
import { GetSectorById } from "../../Sector/GetSectorById.js";
import { getEvent } from "../../Event/GetEvents.js";
import { PaymentModal } from "../../EventDetails/EventMap/Utils/PaymentModal.js";
export async function CreateReservationCard(Reservation) {

  const seat   = await getSeatById(Reservation.seatId);
  const sector = await GetSectorById(seat.sectorId);
  const event  = await getEvent(sector.eventId);

  const BADGE = {
    Paid:      "bg-green-50  text-green-700 border border-green-200",
    Pending:   "bg-amber-50  text-amber-600 border border-amber-200",
    Cancelled: "bg-red-50    text-red-600   border border-red-200",
  };

  const BADGE_LABEL = {
    Paid:      "Pagada",
    Pending:   "Pendiente",
    Cancelled: "Cancelada",
  };

  function formatDate(iso) {
    if (!iso) return "—";
    const d = new Date(iso);
    const months = ["ene","feb","mar","abr","may","jun","jul","ago","sep","oct","nov","dic"];
    return `${d.getDate()} ${months[d.getMonth()]}. ${d.getFullYear()} · ${String(d.getHours()).padStart(2,"0")}:${String(d.getMinutes()).padStart(2,"0")}`;
  }

  const card = document.createElement("div");
  card.className = "bg-white border border-gray-200 rounded-xl p-5 flex items-center gap-5 hover:shadow-md hover:-translate-y-0.5 transition-all duration-200 cursor-pointer";

  card.innerHTML = `
    <div class="flex-shrink-0 w-12 h-12 bg-gray-100 rounded-lg flex items-center justify-center">
      <svg class="w-6 h-6 text-gray-400" fill="none" stroke="currentColor" stroke-width="1.5" viewBox="0 0 24 24">
        <path stroke-linecap="round" stroke-linejoin="round"
          d="M16.5 6v.75m0 3v.75m0 3v.75m0 3V18m-9-5.25h5.25M7.5 15h3M3.375
             5.25c-.621 0-1.125.504-1.125 1.125v3.026a2.999 2.999 0 010
             5.198v3.026c0 .621.504 1.125 1.125 1.125h17.25c.621
             0 1.125-.504 1.125-1.125v-3.026a2.999 2.999 0
             010-5.198V6.375c0-.621-.504-1.125-1.125-1.125H3.375z"/>
      </svg>
    </div>

    <div class="flex-1 min-w-0 space-y-1">
      <h1 class="text-sm font-bold text-gray-900 truncate">${event.name}</h1>
      <p class="text-xs text-gray-400 font-mono truncate">${Reservation.id}</p>
      <p class="text-sm font-semibold text-gray-800">Asiento <span class="font-mono">${seat.seatNumber} — ${sector.name}</span></p>
      <p class="text-xs text-gray-400">Reservado el ${formatDate(Reservation.reservedAt)}</p>
    </div>

    <div class="flex-shrink-0 flex flex-col items-end gap-2">
      <span class="text-xs px-2.5 py-0.5 rounded-full font-medium ${BADGE[Reservation.status] ?? "bg-gray-100 text-gray-500"}">
        ${BADGE_LABEL[Reservation.status] ?? Reservation.status}
      </span>
      <p class="text-xs text-gray-400">Expira ${formatDate(Reservation.expiresAt)}</p>
      ${Reservation.status === "Pending" ? `
        <button class="btn-confirmar mt-1 text-xs px-3 py-1 rounded-full border border-green-200 text-green-500 hover:bg-green-50 transition">
          Confirmar reserva
        </button>` : ""}
    </div>
  `;

  if (Reservation.status === "Pending") {
    const selected = { seat:seat, sector:sector };
    card.querySelector(".btn-confirmar")
        .addEventListener("click", (e) => {
            e.stopPropagation();
            PaymentModal(Reservation.id, event.name, selected);
        });
}

  return card;
}