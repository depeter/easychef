import { inject } from 'aurelia-framework';
import { Router } from 'aurelia-router';
import { ScrapingService } from 'services/ScrapingService';

@inject(Router)
export class SetupAccount {
    heading = 'EasyChef';
    email = '';
    password = '';
    error = '';

    constructor(router) {
        this.router = router;
    }

    validate() {
        if (!validateEmail(this.email))
            error = 'Gelieve een geldig emailadres in te geven.';

        if (password === '') {
            error = 'Gelieve je wachtwoord in te geven.';
        }


    }

    continue() {
        this.router.navigate('setup-account');
    }

    validateEmail(email) {
        var re = /[^\s@]+@[^\s@]+\.[^\s@]+/;
        return re.test(email);
    }

}
