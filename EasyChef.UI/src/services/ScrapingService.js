import { inject } from 'aurelia-framework';
import { HttpClient, json } from 'aurelia-fetch-client';
import { environment } from './environment';

@inject(HttpClient)
export class ScrapingService {

    constructor(client) {
        this.client = client;
    }

    verifyLogin(login, password) {
        return this.client.fetch('/user/verifylogin',
            {
                method: 'post',
                body: json({
                    Login: login,
                    Password: password
                })
            });
    }

}