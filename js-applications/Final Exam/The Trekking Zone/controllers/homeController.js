import { loadAllPartials, displayError } from "../scripts/common/shared.js";
import { get } from "../scripts/common/requester.js";

export function getHome(ctx) {
    let partials = {};
    if (sessionStorage.getItem('authtoken')) {
        getTreks()
            .then(y => y.sort((a, b) => b.likes - a.likes))
            .then(x => {
                ctx.treks = x;
                partials.treksPartial = '../views/trek/trek.hbs';
                loadAllPartials('home/home.hbs', ctx, partials);
            })
    } else {
        loadAllPartials('home/home.hbs', ctx, partials);
    }
}

function getTreks() {
    return get('Kinvey', 'appdata', 'treks')
        .catch(e => displayError(e.message));
}