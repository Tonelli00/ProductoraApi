
const base_Url="http://localhost:5252/";
//const base_Url = "https://ripply-pregastrular-barrett.ngrok-free.dev/"

export async function getData(Endpointurl) {
    try{
        const response = await fetch(`${base_Url}${Endpointurl}`, {
            headers: {
                "ngrok-skip-browser-warning": "true",
                "Accept": "application/json"
            }
        });
        const data = await response.json();
        return data;
    }
    catch(error){
        throw new Error(error.message || "No se pudo conectar con el servidor");
    }
}

export async function postData(endpointUrl, body) {
    try
    {
        const response = await fetch(`${base_Url}${endpointUrl}`, {
        method: "POST",
        headers: { "Content-Type": "application/json",  "ngrok-skip-browser-warning": "true" },
        body: JSON.stringify(body)
    });
    const data = await response.json();
    if (!response.ok) {
        throw new Error(data?.Message || "Error en la solicitud");
    }
    return data;
    }
     catch(error){
        throw new Error(error.message || "No se pudo conectar con el servidor");
    }
    
}

export async function putData(endpointUrl) {
    try {
        const response = await fetch(`${base_Url}${endpointUrl}`, {
            method: "PUT",
            headers: { "Content-Type": "application/json" ,  "ngrok-skip-browser-warning": "true" },
        });

        const data = await response.json();

        if (!response.ok) {
            throw new Error(data?.Message || "Error en la solicitud");
        }

        return data;

    } catch(error){
        throw new Error(error.message || "No se pudo conectar con el servidor");
    }
}