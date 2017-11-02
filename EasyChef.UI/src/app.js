export class App {
  configureRouter(config, router) {
    config.title = 'EasyChef';
    config.map([
        { route: ['', 'setup-info'], name: 'setup-info', moduleId: 'setup-info', nav: false, title: 'Setup Info' },
        { route: 'setup-account', name: 'setup-account', moduleId: 'setup-account',  nav: false, title: 'Setup Account' },
        { route: 'recepies', name: 'recepies', moduleId: 'recepies', nav: true, title: 'Recepten' },
        { route: 'order', name: 'order', moduleId: 'order', nav: true, title: 'Bestelling' },
        { route: 'stock', name: 'stock', moduleId: 'stock', nav: true, title: 'Voorraad' },
        { route: 'settings', name: 'settings', moduleId: 'settings', nav: true, title: 'Instellingen' },
    ]);

    this.router = router;
  }
}
