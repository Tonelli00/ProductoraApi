import { postData } from "../Global/ApiClient.js";

export async function createUser(name,email,password)
{
    const endpointUrl = `api/v1/users`;
    const body = {
        name:name,
        email:email,
        passwordHash:password
    }

    return await postData(endpointUrl,body);

}