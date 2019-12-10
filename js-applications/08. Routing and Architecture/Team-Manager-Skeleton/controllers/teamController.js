import { loadPartial, assignTeam, getTeamId, leaveTeam } from "./shared.js";
import { get, post, put } from "../scripts/requester.js";

export function getCatalog(ctx) {
    let partial = { team: './templates/catalog/team.hbs' };
    ctx.hasTeam = getTeamId() !== undefined;
    get('Kinvey', 'appdata', 'teams')
        .then(data => {
            ctx.teams = data;
            loadPartial('catalog/teamCatalog.hbs', ctx, partial);
        })
        .catch(console.log);
}

export function getCreate(ctx) {
    let partial = { createForm: './templates/create/createForm.hbs' };
    loadPartial('create/createPage.hbs', ctx, partial);
}

export function postCreate(ctx) {
    let { name, comment } = ctx.params;
    post('Kinvey', 'appdata', 'teams', { name, description: comment, members: [] })
        .then(x => {
            assignTeam(x._id);
            ctx.redirect('#/catalog')
        })
        .catch(console.log);
}

export function getDetails(ctx) {
    let partial = {
        teamMember: './templates/catalog/teamMember.hbs',
        teamControls: './templates/catalog/teamControls.hbs',
    }
    let teamId = ctx.params.teamId;
    ctx.teamId = teamId;
    ctx.isOnTeam = teamId === sessionStorage.getItem('teamId');
    get('Kinvey', 'appdata', `teams/${teamId}`)
        .then(x => {
            getMembers(teamId)
                .then(y => {
                    ctx.members = y;
                    ctx.name = x.name;
                    ctx.comment = x.description;
                    ctx.isAuthor = x._acl.creator === sessionStorage.getItem('userId');
                    loadPartial('catalog/details.hbs', ctx, partial);
                });
        })
        .catch(console.log);
}

export function getLeave(ctx) {
    leaveTeam()
        .then(ctx.redirect('#/catalog'))
        .catch(console.log);
}

export function getJoin(ctx) {
    if (getTeamId() !== undefined) {
        leaveTeam()
            .then(x => {
                assignTeam(ctx.params.teamId)
                    .then(ctx.redirect(`#/catalog/${ctx.params.teamId}`));
            })
            .catch(console.log);
    } else {
        assignTeam(ctx.params.teamId)
            .then(ctx.redirect(`#/catalog/${ctx.params.teamId}`))
            .catch(console.log);
    }
}

export function getEdit(ctx) {
    let partial = { editForm: './templates/edit/editForm.hbs' };
    get('Kinvey', 'appdata', `teams/${ctx.params.teamId}`)
        .then(x => {
            ctx.name = x.name;
            ctx.comment = x.description;
            ctx.teamId = ctx.params.teamId;
            loadPartial('edit/editPage.hbs', ctx, partial);
        })
        .catch(console.log);
}

export function postEdit(ctx) {
    let teamId = ctx.params.teamId;
    let { comment, name } = ctx.params;
    get('Kinvey', 'appdata', `teams/${teamId}`)
        .then(en => {
            put('Kinvey', 'appdata', `teams/${teamId}`, { name, description: comment, members: en.members })
                .then(ctx.redirect(`#/catalog/${teamId}`))
        })
        .catch(console.log);
}

function getMembers(teamId) {
    return get('Kinvey', 'appdata', `teams/${teamId}`)
        .then(x => x.members)
        .catch(console.log);
}