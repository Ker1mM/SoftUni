import * as userController from "../controllers/identityController.js";
import * as homeController from "../controllers/homeController.js";
import { getCatalog, postCreate, getCreate, getDetails, getLeave, getJoin, getEdit, postEdit } from "../controllers/teamController.js";

var app = Sammy('#main', function () {
    this.use('Handlebars', 'hbs');
    this.get('#/', homeController.getHome);
    this.get('#/home', homeController.getHome);

    this.get('#/register', userController.registerGet);
    this.post('#/register', userController.registerPost);

    this.get('#/login', userController.loginGet);
    this.post('#/login', userController.loginPost);

    this.get('#/logout', userController.logout);

    this.get('#/about', homeController.getAbout);

    this.get('#/catalog', getCatalog);
    this.get('#/create', getCreate);
    this.post('#/create', postCreate);

    this.get('#/catalog/:teamId', getDetails)

    this.get('#/leave', getLeave);
    this.get('#/join/:teamId', getJoin)

    this.get('#/edit/:teamId', getEdit);
    this.post('#/edit/:teamId', postEdit);
});

app.run('#/');