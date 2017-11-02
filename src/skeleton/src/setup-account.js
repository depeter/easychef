import { inject } from 'aurelia-framework';
import { Router } from 'aurelia-router';

@inject(Router)
export class SetupAccount {
    heading = 'EasyChef';
    subtitle = 'Om deze app te kunnen gebruiken heb je een Collect&Go account nodig.';
    linkText = 'Klik hier om er eentje aan te maken.';
    linkUrl = 'https://colruyt.collectandgo.be/cogo/nl/aanmelden'

    constructor(router) {
        this.router = router;
    }

    navigate() {
        console.writeLine("navigating...");
        this.router.navigate('setup-account');
    }
}
