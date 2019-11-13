function attachEvents() {
    document.getElementById('refresh').addEventListener('click', refresh);
    document.getElementById('submit').addEventListener('click', send);
}

function send() {
    let author = document.getElementById('author').value;
    let content = document.getElementById('content').value;

    let message = { author, content };
    fetch('https://rest-messanger.firebaseio.com/messanger.json',
        {
            method: 'POST',
            body: JSON.stringify(message)
        })
        .then(x => refresh())
        .then(y => clear())
}

function clear() {
    document.getElementById('author').value = '';
    document.getElementById('content').value = '';
}

function refresh() {
    fetch('https://rest-messanger.firebaseio.com/messanger.json')
        .then(x => x.json())
        .then(data => displayMessages(data))
}

function displayMessages(data) {
    let messages = [];
    if (data) {
        for (let key of Object.keys(data)) {
            let text = `${data[key].author}: ${data[key].content}`;
            messages.push(text);
        }
        document.getElementById('messages').innerHTML = messages.join('\n');
    }
}

setInterval(refresh, 1000);

attachEvents();