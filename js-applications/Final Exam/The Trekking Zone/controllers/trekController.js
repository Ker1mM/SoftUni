import { loadAllPartials, displayError, displaySuccess, displayLoading } from "../scripts/common/shared.js";
import { post, get, put, del } from "../scripts/common/requester.js";

export function getRequestTrek(ctx) {
    loadAllPartials('trek/requestTrek.hbs', ctx);
}

export function postRequestTrek(ctx) {
    let { location, dateTime, description, imageURL } = ctx.params;
    let likes = 0;
    let organizer = sessionStorage.getItem('username');
    if (location.length >= 6 && description.length >= 10) {
        post('Kinvey', 'appdata', 'treks', { location, dateTime, description, imageURL, likes, organizer })
            .then(x => {
                displaySuccess('Trek created successfully.', ctx, '#/home');
            })
            .catch(e => displayError(e.messsage));
    }
    else {
        displayError('Invalid input.');
    }
}

export function getDetails(ctx) {
    let { trekId } = ctx.params;
    displayLoading();
    get('Kinvey', 'appdata', `treks/${trekId}`)
        .then(x => {
            ctx.trek = x;
            ctx.isCreator = x.organizer === sessionStorage.getItem('username');
            loadAllPartials('trek/trekDetails.hbs', ctx);
        })
        .catch(e => displayError(e.messsage));
}

export function getEdit(ctx) {
    let { trekId } = ctx.params;
    displayLoading();
    get('Kinvey', 'appdata', `treks/${trekId}`)
        .then(x => {
            ctx.trek = x;
            loadAllPartials('trek/edit.hbs', ctx);
        })
        .catch(e => displayError(e.messsage));
}

export function postEdit(ctx) {
    let { location, dateTime, description, imageURL, likes, organizer, trekId } = ctx.params;
    if (location.length >= 6 && description.length >= 10) {
        displayLoading();
        put('Kinvey', 'appdata', `treks/${trekId}`, { location, dateTime, description, imageURL, likes, organizer })
            .then(x => {
                displaySuccess('Trek edited successfully.', ctx, '#/home');
            })
            .catch(e => displayError(e.messsage));
    }
    else {
        displayError('Invalid input.');
    }
}

export function getLike(ctx) {
    let { trekId } = ctx.params;
    displayLoading();
    get('Kinvey', 'appdata', `treks/${trekId}`)
        .then(x => {
            let { location, dateTime, description, imageURL, likes, organizer } = x;
            likes++;
            put('Kinvey', 'appdata', `treks/${trekId}`, { location, dateTime, description, imageURL, likes, organizer })
                .then(y => {
                    displaySuccess('You liked the trek successfully.', ctx, `#/details/${trekId}`);
                })
        })
        .catch(e => displayError(e.messsage));
}

export function getDelete(ctx) {
    let { trekId } = ctx.params;

    del('Kinvey', 'appdata', `treks/${trekId}`)
        .then(x => {
            displaySuccess('You closed the trek successfully.', ctx, '#/home');
        })
        .catch(e => displayError(e.messsage));
}