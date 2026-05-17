import { GetUserReservations } from "../../Reservation/GetReservation.js";
import { CreateReservationCard } from "./ReservationCard.js";

export async function ReservationList()
{
    const container = document.getElementById("reservations-container");
    renderSkeletons(container, 4);
    const userId = localStorage.getItem("UserId");
    const reservations = await GetUserReservations(userId);

    const TotalReservation = document.getElementById("total-reservation");
    TotalReservation.textContent=reservations.length;
   
    const ConfirmedReservation = document.getElementById("confirmed-reservation");
    ConfirmedReservation.textContent = reservations.filter(r=> r.status == "Paid").length;
   
    const PendingReservation = document.getElementById("pending-reservation");
    PendingReservation.textContent = reservations.filter(r=> r.status == "Pending").length;
 
    
    container.innerHTML = "";

    if (!reservations || reservations.length === 0) {
        document.getElementById("empty-state").classList.remove("hidden");
        return;
    }

  document.getElementById("empty-state").classList.add("hidden");

  for (const reservation of reservations) {
    const card = await CreateReservationCard(reservation);
    container.appendChild(card);
}


}