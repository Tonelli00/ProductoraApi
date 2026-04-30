import { postData } from "../Global/ApiClient.js";

export function makeReservation(userId, seatId)
{
const endpointUrl = `api/v1/reservations`;

const body = 
{
    userId: userId,
    seatId: seatId
};

return postData(endpointUrl,body);
}