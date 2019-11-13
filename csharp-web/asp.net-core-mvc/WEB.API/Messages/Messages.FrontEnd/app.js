loadMessages();

function renderMessages(data) {
    $('#messages').empty();

    for(let message of data) {
        $('#messages')
            .append('<div class="message d-flex justify-content-start"><strong>'
                + message.user
                + '</strong>: '
                + message.content
                +'</div>')

    }
}

function loadMessages() {
    $.get({
        url: APP_SERVICE_URL + 'messages/all',
        success: function success(data) {
            renderMessages(data);
        },
        error: function error(error) {
            console.log(error);
        }
    });
}

function createMessage() {
    let message = $('#message').val();

    let username = user;

    if(username == null){
        alert('You have to choose a username first!');
        return;
    }

    if(message.length === 0) {
        alert('You cannot send empty message!');
        return;
    }

    $.post({
        url: APP_SERVICE_URL + 'messages/create',
        headers: {
            'Content-Type': 'application/json'
        },
        data: JSON.stringify({content: message, user: user}),
        success: function success(data) {
            loadMessages();
        },
        error: function error(error) {
            console.log(error);
        }
    });
}


setInterval(loadMessages, 100);