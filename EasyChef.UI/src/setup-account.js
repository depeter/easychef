import { inject } from 'aurelia-framework';
import { Router } from 'aurelia-router';
import { ScrapingService } from 'services/ScrapingService';

@inject(Router, ScrapingService)
export class SetupAccount {
    heading = 'EasyChef';
    email = '';
    password = '';
    error = '';
    validating = false;

    constructor(router, scrapingService) {
        this.router = router;
        this.scrapingService = scrapingService;
    }

    validate() {
        if (!validateEmail(this.email))
            error = 'Gelieve een geldig emailadres in te geven.';

        if (password === '') {
            error = 'Gelieve je wachtwoord in te geven.';
        }

        this.validating = true;
        this.scrapingService.verifyLogin(this.email, this.password).then(result => {
            var validated = result.data;
            this.validating = false;

            if (validated) {
                this.validating = false;
                this.router.navigate('recepies');
            }
        });
    }

    validateEmail(email) {
        var re = /[^\s@]+@[^\s@]+\.[^\s@]+/;
        return re.test(email);
    }

}
