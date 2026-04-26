const base_Url=" http://localhost:5252/";

export async function getData(Endpointurl) {
    try{
        const response = await fetch(`${base_Url}${Endpointurl}`);
        const data = await response.json()
        return data;
    }
    catch(error){
        console.error("No se puede establecer una conexión con el servidor");
    }
}