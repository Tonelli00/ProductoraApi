import { postData } from "../Global/ApiClient.js";

export async function login(email,password)
{
   const endpointUrl = `api/v1/users/login`;
   const body = {
        email:email,
        password:password
    }

    return await postData(endpointUrl,body);

}