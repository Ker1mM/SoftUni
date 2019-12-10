const baseUrl = 'https://baas.kinvey.com';
const appKey = 'kid_B18eX0u6r'; //TODO
const appSecret = '7d525a76036842fc8084518f45204619'; // TODO

function createAuthorization(type) {
    return type === 'Basic'
        ? `Basic ${btoa(`${appKey}:${appSecret}`)}`
        : `Kinvey ${sessionStorage.getItem('authtoken')}`
}

function createHeader(httpMethod, data, type) {
    const headers = {
        method: httpMethod,
        headers: {
            'Authorization': createAuthorization(type),
            'Content-Type': 'application/json',
        },
    };

    if (httpMethod === 'POST' || httpMethod === 'PUT') {
        headers.body = JSON.stringify(data);
    }

    return headers;
}

function handleError(e) {
    if (!e.ok) {
        throw new Error(e.statusText);
    }

    return e;
}

function deserializeData(x) {
    if (x.status === 204) {
        return x;
    }

    return x.json();
}

function fetchData(kinveyModule, endpoint, headers) {
    const url = `${baseUrl}/${kinveyModule}/${appKey}/${endpoint}`;

    return fetch(url, headers)
        .then(handleError)
        .then(deserializeData);
}

export function get(type, kinveyModule, endpoint) {
    const headers = createHeader('GET', type);
    return fetchData(kinveyModule, endpoint, headers);
}

export function post(type, kinveyModule, endpoint, data) {
    const headers = createHeader('POST', data, type);
    return fetchData(kinveyModule, endpoint, headers);
}

export function put(type, kinveyModule, endpoint, data) {
    const headers = createHeader('PUT', data, type);
    return fetchData(kinveyModule, endpoint, headers);
}

export function del(type, kinveyModule, endpoint) {
    const headers = createHeader('DELETE', type);
    return fetchData(kinveyModule, endpoint, headers);
}