export const fetchOperations = {
    get: async (url, username = 'guest', password = 'guest') => {
        let options = getRequestOptions('GET', username, password);
        let data = await fetchData(url, options)

        return data;
    },
    post: async (url, username, password, body = undefined) => {
        let options = getRequestOptions('POST', username, password, body);
        return await fetchData(url, options);
    },
    put: async (url, username, password, body) => {
        let options = getRequestOptions('PUT', username, password, body);
        return await fetchData(url, options);
    },
    delete: async (url, username, password) => {
        let options = getRequestOptions('DELETE', username, password);
        return await fetchData(url, options);
    }
}

function getRequestOptions(method, username = 'guest', password = 'guest', body = undefined) {
    let reqBody = {
        method,
        headers: {
            'Authorization': 'Basic ' + btoa(`${username}:${password}`),
            'Content-type': 'application/json'
        }
    }

    if (body) {
        reqBody['body'] = JSON.stringify(body);
    }

    return reqBody;
}

function fetchData(url, options, errorHandler = handleError) {
    return fetch(url, options)
        .then(x => errorHandler(x))
        .then(x => x.json())
}

function handleError(data) {
    if (!data.ok) {
        throw new Error(data.statusText);
    }

    return data;
}