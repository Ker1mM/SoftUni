import { loadAllPartials, displayError, displaySuccess, displayLoading } from "../scripts/common/shared.js";
import { post, get } from "../scripts/common/requester.js"


export function getRegister(ctx) {
    loadAllPartials('user/register.hbs', ctx);
}

export function postRegister(ctx) {
    let { username, password, rePassword } = ctx.params;

    try {
        validateUser(username, password, rePassword);
        displayLoading();
        post('Basic', 'user', '', { username, password })
            .then(x => {
                logUser(username, password, ctx, 'Successfully registered user.', '#/home')
            })
    }
    catch (er) {
        displayError(er.message);
    }
}

export function getLogin(ctx) {
    loadAllPartials('user/login.hbs', ctx);
}

export function postLogin(ctx) {
    const { username, password } = ctx.params;
    logUser(username, password, ctx, 'Successfully logged user.', '#/home')
}

export function getLogout(ctx) {
    displayLoading();
    post('Kinvey', 'user', '_logout')
        .then(x => {
            sessionStorage.clear();
            displaySuccess('Logout successful.', ctx, '#/home');
        })
        .catch(e => displayError(e.message));
}

export function getProfile(ctx) {
    displayLoading();
    ctx.username = sessionStorage.getItem('username');
    get('Kinvey', 'appdata', 'treks')
        .then(x => x.filter(x => x.organizer === ctx.username))
        .then(y => {
            ctx.trekCount = y.length;
            ctx.treks = y;
            loadAllPartials('user/profile.hbs', ctx);
        })
        .catch(x => displayError(e.message));
}

function validateUser(username, password, rePassword) {
    if (username.length < 3) {
        throw new Error('The username should be at least 3 characters long.');
    }

    if (password.length < 6) {
        throw new Error('The password should be at least 6 characters long.');
    }

    if (password !== rePassword) {
        throw new Error('The repeat password should be equal to the password.')
    }

    return true;
}

function logUser(username, password, ctx, message, redirect) {
    displayLoading();
    post('Basic', 'user', 'login', { username, password })
        .then(x => {
            sessionStorage.setItem('authtoken', x._kmd.authtoken);
            sessionStorage.setItem('username', x.username);
            displaySuccess(message, ctx, redirect);
        })
        .catch(e => {
            displayError('Incorrect username or password!');
        })
}