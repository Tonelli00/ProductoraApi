import { getData } from "../Global/ApiClient"
export async function GetReservation(reservationId)
{
    const endpointUrl = `api/v1/reservations/${reservationId}`
    const reservation = await getData(endpointUrl);
    return reservation;
}