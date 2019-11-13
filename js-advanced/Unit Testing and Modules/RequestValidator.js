function HTTPRequest(request) {
    let method = request.method;
    if (method !== 'GET' && method !== 'POST' && method !== 'DELETE' && method != 'CONNECT') {
        throwError('Method');
    }

    let uri = request.uri;
    let uriRegEx = RegExp('(^[a-zA-Z0-9.]+$)|(^[*]$)');
    if (!uri || !uriRegEx.test(uri)) {
        throwError('URI');
    }

    let version = request.version;
    if (!version || (version !== 'HTTP/0.9' && version !== 'HTTP/1.0' && version !== 'HTTP/1.1' && version !== 'HTTP/2.0')) {
        throwError('Version');
    }

    let message = request.message;
    let messageRegEx = RegExp(/^[^<>\\&\'"]*$/);

    if (message === undefined || !messageRegEx.test(message)) {
        throwError('Message');
    }

    return request;

    function throwError(er) {
        throw new Error(`Invalid request header: Invalid ${er}`);
    }
}

console.log(HTTPRequest({
    method: 'GET',
    uri: 'svn.public.catalog',
    version: 'HTTP/1.1',
    message: 'asl\\pls'
}

));