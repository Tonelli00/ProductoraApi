import { getData } from "../Global/ApiClient.js";

export async function getAllEvents(page) {
    const endpointUrl = `api/v1/events?Page=${page}&PageSize=10`
    const events = await getData(endpointUrl);
    console.log(events)
    return events;

}
export async function getThreeEvents() {
    const endpointUrl = `api/v1/Events?Page=1&PageSize=3`;
    const events = await getData(endpointUrl);
    return events;
}
export async function getEvent(eventId) {
    const endpointUrl = `api/v1/events/${eventId}`;
    const event = await getData(endpointUrl);
    return event;
}