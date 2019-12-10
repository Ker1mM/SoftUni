function attachEvents() {
    document.addEventListener('click', handleClick)
}

function handleClick(e) {
    if (typeof actions[e.target.className] === 'function') {
        let el = e.target.parentNode;
        actions[e.target.className](el);
    }
}

attachEvents();

////
//
////

function getCatchInfo(cth) {
    let info = {};
    for (const att of catchParameters) {
        info[att] = cth.getElementsByClassName(att)[0].value;
    }

    return info;
}

function printCatches(catches) {
    var frag = document.createDocumentFragment();
    if (catches) {
        for (const id of Object.keys(catches)) {
            var catchClass = document.createElement('div');
            catchClass.setAttribute('class', 'catch');
            catchClass.setAttribute('data-id', id);
            catchClass.innerHTML = getCatchElement(catches[id]);
            frag.appendChild(catchClass);
        }
    }
    html.catches().innerHTML = '';
    html.catches().appendChild(frag);
}


function getCatchElement(obj) {
    let text = `<label>Angler</label>` +
        `<input type="text" class="angler" value="${obj.angler}" />` +
        `<hr>` +
        `<label>Weight</label>` +
        `<input type="number" class="weight" value="${obj.weight}" />` +
        `<hr>` +
        `<label>Species</label>` +
        `<input type="text" class="species" value="${obj.species}" />` +
        `<hr>` +
        `<label>Location</label>` +
        `<input type="text" class="location" value="${obj.location}" />` +
        `<hr>` +
        `<label>Bait</label>` +
        `<input type="text" class="bait" value="${obj.bait}" />` +
        `<hr>` +
        `<label>Capture Time</label>` +
        `<input type="number" class="captureTime" value="${obj.captureTime}" />` +
        `<hr>` +
        `<button class="update">Update</button>` +
        `<button class="delete">Delete</button>`;


    return text;
}

////
//
////

const html = {
    catches: () => document.getElementById('catches'),
    addFrom: () => document.getElementById('addForm'),

}

const catchParameters = ['angler', 'weight', 'species', 'location', 'bait', 'captureTime'];

const actions = {
    load: async () => {
        let catches = await fetchData(urlTemplates.load());
        printCatches(catches);
    },
    add: async () => {
        let newCatch = getCatchInfo(html.addFrom());
        await fetchData(urlTemplates.load(), 'POST', newCatch);
        actions.load();
    },
    update: async (ph) => {
        var id = ph.getAttribute('data-id');
        let updatedCatch = getCatchInfo(ph);
        await fetchData(urlTemplates.post(id), 'PUT', updatedCatch);
        actions.load();
    },
    delete: async (ph) => {
        var id = ph.getAttribute('data-id');
        await fetchData(urlTemplates.post(id), 'DELETE');
        actions.load();
    }
}

const urlTemplates = {
    load: () => 'https://fisher-game.firebaseio.com/catches.json',
    post: (id) => `https://fisher-game.firebaseio.com/catches/${id}.json`,
}

////
//fetch data
////

function fetchData(url, method = 'GET', body = undefined, errorHandler = handleError) {
    var reqBody = { method }
    if (body) {
        reqBody['body'] = JSON.stringify(body);
    }

    return fetch(url, reqBody)
        .then(res => errorHandler(res))
        .then(data => data.json())
}

function handleError(er) {
    if (!er.ok) {
        throw new Error('Error');
    }
    return er;
}
