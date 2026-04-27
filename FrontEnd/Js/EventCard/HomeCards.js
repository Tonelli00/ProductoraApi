import { getThreeEvents } from "../Event/GetEvents.js";
import { createEventCard } from "./CreateEventCard.js";

export async function createHomeCards() {
    
    const featuredevents = document.getElementById("featured-events");

    const events = await getThreeEvents();

    if(!events || events.length == 0)
        {
        featuredevents.innerHTML = "<p>No hay eventos disponibles</p>";
        return;
        }

    events.forEach(event => {
    const card = createEventCard(event);
    featuredevents.appendChild(card);
    });
}