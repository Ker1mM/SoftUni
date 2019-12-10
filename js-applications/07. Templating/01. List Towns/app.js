function attachEvents() {
    document.getElementById('btnLoadTowns').addEventListener('click', loadTowns);
}

async function loadTowns() {
    let input = document.getElementById('towns').value;
    let content = {};
    content.towns = input.split(/\s*,\s*/).filter(x => x);

    let tmplt = await fetch('./template.hbs').then(x => x.text());
    let tmpltScript = Handlebars.compile(tmplt);

    let body = document.getElementById('root');
    body.innerHTML = tmpltScript(content);
}

attachEvents();