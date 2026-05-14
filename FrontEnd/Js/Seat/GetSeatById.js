import { getData } from "../Global/ApiClient.js";

export async function getSeatById(SeatId)
{
    const endpointUrl = `api/v1/seats/${SeatId}`;
    const result = await getData(endpointUrl);
    return result;
}
