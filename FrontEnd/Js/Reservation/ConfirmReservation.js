import { putData } from "../Global/ApiClient.js";

export async function ConfirmReservation(reservationId) {
    const endpointUrl = `api/v1/reservations/${reservationId}`;
    return putData(endpointUrl);
}