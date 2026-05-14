import { getData } from "../Global/ApiClient.js";
export async function GetSectorById(SectorId) {
    const endpointUrl = `api/v1/sectors/${SectorId}/summary`;
    const result = await getData(endpointUrl);
    return result;
}