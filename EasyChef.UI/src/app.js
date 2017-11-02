export class App {
  configureRouter(config, router) {
    config.title = 'EasyChef';
    config.map([
        { route: ['', 'setup-info'], name: 'setup-info', moduleId: 'setup-info', nav: false, title: 'Setup Info' },
        { route: 'setup-account', name: 'setup-account', moduleId: 'setup-account',  nav: false, title: 'Setup Account' },
        { route: 'child-router',  name: 'child-router', moduleId: 'child-router', nav: true, title: 'Child Router' }
    ]);

    this.router = router;
  }
}
