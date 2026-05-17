import { getThreeEvents } from "../Event/GetEvents.js";
import { createEventCard } from "./CreateEventCard.js";
import { renderSkeletons } from "../Utils/RenderSkeletons.js";
export async function createHomeCards() {
    
    const featuredevents = document.getElementById("featured-events");
    renderSkeletons(featuredevents, 3);

    const events = await getThreeEvents();

    if(!events || events.length == 0)
        {
        featuredevents.innerHTML = "<p>No hay eventos disponibles</p>";
        return;
        }


    featuredevents.innerHTML = "";

    events.forEach(event => {
    const card = createEventCard(event);
    featuredevents.appendChild(card);
    });
}