import { getAllEvents } from "../Event/GetEvents.js";
import { createEventCard } from "./CreateEventCard.js";

export async function EventsCards(pageNumber=1){

    const eventsSection = document.getElementById("events");
    
    const events = await getAllEvents(pageNumber);

    if(!events || events.length == 0)
        {
            eventsSection.innerHTML = "<p>No hay eventos programados</p>";
            return;
        }
    
    events.forEach(event => {
        const card = createEventCard(event);
        eventsSection.appendChild(card);
    });

    
}