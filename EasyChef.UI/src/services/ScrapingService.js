import { inject } from 'aurelia-framework';
import { HttpClient, json } from 'aurelia-fetch-client';
import { environment } from '../environment';

@inject(HttpClient)
export class ScrapingService {

    constructor(client) {
        this.client = client;
        
        client.configure(config => {
            config
                .withBaseUrl('http://localhost:60000/api/')
                .withDefaults({
                    mode: 'cors',
                    headers: {
                        'Accept': 'application/json',
                        'X-Requested-With': 'Fetch',
                        'Content-type': 'application/json'
                    }
                })
                .withInterceptor({
                    request(request) {
                        console.log(`Requesting ${request.method} ${request.url}`);
                        return request;
                    },
                    response(response) {
                        console.log(`Received ${response.status} ${response.url}`);
                        return response;
                    }
                });
        });

    }

    verifyLogin(login, password) {
        return this.client.fetch('user/verifylogin',
            {
                method: 'post',
                body: json({
                    Login: login,
                    Password: password
                })
            });
    }

    saveUser(login, password) {
        return this.client.fetch('user/save',
            {
                method: 'post',
                body: json({
                    Email: login,
                    Password: password
                })
            });
    }

}