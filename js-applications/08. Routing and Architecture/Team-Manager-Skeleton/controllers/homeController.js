import { loadPartial, getTeamId } from "./shared.js";

export function getHome(ctx) {
    let teamId = getTeamId();
    ctx.hasTeam = teamId !== undefined;
    ctx.teamId = teamId;
    loadPartial("home/home.hbs", ctx);
}

export function getAbout(ctx) {
    loadPartial("about/about.hbs", ctx);
}