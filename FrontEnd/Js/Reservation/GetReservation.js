import { getData } from "../Global/ApiClient.js"
export async function GetReservation(reservationId)
{
    const endpointUrl = `api/v1/reservations/${reservationId}`
    const reservation = await getData(endpointUrl);
    return reservation;
}

export async function GetUserReservations(userId)
{
    const endpointUrl = `api/v1/reservations/${userId}/user`
    const reservations = await getData(endpointUrl);
    return reservations;
}
