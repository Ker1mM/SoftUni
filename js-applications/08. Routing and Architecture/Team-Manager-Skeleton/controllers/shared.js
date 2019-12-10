import { post, put, get } from "../scripts/requester.js";

export function loadPartial(path, ctx, partials = {}) {
    partials.header = '../templates/common/header.hbs';
    partials.footer = '../templates/common/footer.hbs'
    checkLogged(ctx);
    ctx.loadPartials(partials)
        .then(function () {
            this.partial(`../templates/${path}`)
        });
}

function checkLogged(ctx) {
    ctx.isLogged = sessionStorage.getItem('authtoken') !== null;

    if (ctx.isLogged) {
        ctx.username = sessionStorage.getItem('username');
    }
}

export function getTeamId() {
    let id = sessionStorage.getItem('teamId');

    return id === 'undefined' ? undefined : id;
}


export function assignTeam(teamId) {
    sessionStorage.setItem('teamId', teamId);
    let userId = sessionStorage.getItem('userId');

    return get('Kinvey', 'appdata', `teams/${teamId}`)
        .then(x => {
            let { members, name, description } = x;
            members.push(sessionStorage.getItem('username'));
            put('Kinvey', 'appdata', `teams/${teamId}`, { name, description, members })
                .then(y => {
                    put('Kinvey', 'user', userId, { teamId });
                })
        }).catch(console.log);
}

export function leaveTeam() {
    let teamId = sessionStorage.getItem('teamId');
    sessionStorage.setItem('teamId', undefined);
    let userId = sessionStorage.getItem('userId');

    return get('Kinvey', 'appdata', `teams/${teamId}`)
        .then(x => {
            let { members, name, description } = x;
            let index = members.indexOf(sessionStorage.getItem('username'));
            if (index >= 0) {
                members.splice(index, 1)
            };
            put('Kinvey', 'appdata', `teams/${teamId}`, { name, description, members })
                .then(y => {
                    put('Kinvey', 'user', userId, { teamId: undefined });
                })
        }).catch(console.log);
}
