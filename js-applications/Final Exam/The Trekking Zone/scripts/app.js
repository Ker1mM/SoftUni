import { getHome } from "../controllers/homeController.js"
import { getRegister, postRegister, getLogin, postLogin, getLogout, getProfile } from "../controllers/userController.js"
import { getRequestTrek, postRequestTrek, getDetails, getEdit, postEdit, getLike, getDelete } from "../controllers/trekController.js";

const app = Sammy('body', function () {
    this.use('Handlebars', 'hbs');

    this.get('#/', getHome);
    this.get('#/home', getHome);

    this.get('#/register', getRegister);
    this.post('#/register', postRegister);

    this.get('#/login', getLogin);
    this.post('#/login', postLogin);

    this.get('#/logout', getLogout);

    this.get('#/requestTrek', getRequestTrek);
    this.post('#/requestTrek', postRequestTrek);

    this.get('#/details/:trekId', getDetails);

    this.get('#/edit/:trekId', getEdit);
    this.post('#/edit/:trekId', postEdit);

    this.get('#/like/:trekId', getLike);
    this.get('#/close/:trekId', getDelete);

    this.get('#/profile', getProfile);

});

app.run('#/');