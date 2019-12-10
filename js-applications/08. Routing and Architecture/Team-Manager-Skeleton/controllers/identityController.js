import { loadPartial } from "./shared.js";
import { post, get } from "../scripts/requester.js"

export function registerGet(ctx) {
    let partial = { 'registerForm': "../templates/register/registerForm.hbs" };
    loadPartial("register/registerPage.hbs", ctx, partial);
}

export function registerPost(ctx) {
    let { username, password, repeatPassword } = ctx.params;
    if (username && password && password === repeatPassword) {
        post('Basic', 'user', '', { username, password })
            .then(ctx.redirect('#/login'))
            .catch(console.log);
    }
}

export function loginGet(ctx) {
    let partial = { loginForm: "../templates/login/loginForm.hbs" };
    loadPartial("login/loginPage.hbs", ctx, partial);
}

export function loginPost(ctx) {
    let { username, password } = ctx.params;
    post('Basic', 'user', 'login', { username, password })
        .then(data => {
            saveUser(data);
            ctx.redirect('#/home');
        })
        .catch(console.log);
}

export function logout(ctx) {
    post('Kinvey', 'user', '_logout')
        .then(data => {
            sessionStorage.clear();
            ctx.redirect('#/');
        })
        .catch(console.log)
}

function saveUser(res) {
    sessionStorage.setItem('authtoken', res._kmd.authtoken);
    sessionStorage.setItem('userId', res._id);
    sessionStorage.setItem('username', res.username);
    sessionStorage.setItem('teamId', res.teamId);
}