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
        this.validating = false;
    }

    validate() {
        if (!this.validateEmail(this.email))
            this.error = 'Gelieve een geldig emailadres in te geven.';

        if (this.password === '') {
            this.error = 'Gelieve je wachtwoord in te geven.';
        }

        this.validating = true;
        var self = this;
        this.scrapingService.verifyLogin(this.email, this.password)
            .then(response => response.json())
            .then(data => {
                var validated = data;
                this.validating = false;

                if (validated) {
                    self.scrapingService.saveUser(self.email, self.password)
                        .then(response => response.json())
                        .then(data => {
                            localStorage.setItem('userid', data);
                            self.router.navigate('recepies');
                        });
                }
            });

    }

    validateEmail(email) {
        var re = /[^\s@]+@[^\s@]+\.[^\s@]+/;
        return re.test(email);
    }

}
