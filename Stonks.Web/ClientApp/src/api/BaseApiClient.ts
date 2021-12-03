export class BaseApiClient {

    public baseUrl = "/";

    public async post(url: string, body: any) : Promise<any> {

        body = { title: 'React POST Request Example' };
        // Simple POST request with a JSON body using fetch
        const requestOptions = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(body)
        };

        const response = await fetch(url, requestOptions);
        const data = await response.json();

        return data;
    }

    public async get(url: string) : Promise<any> {
        const response = await fetch(url);
        const data = await response.json();

        return data;
    }
}

