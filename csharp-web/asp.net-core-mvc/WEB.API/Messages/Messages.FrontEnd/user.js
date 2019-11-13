

function getUsername() {
    let token = localStorage.getItem('auth_token');
    let claims = token.split('.')[1];
    let claim = atob(claims);
    let id = JSON.parse(claim).nameid;

    $.post({
        url: APP_SERVICE_URL + 'user/username',
        headers: {
            'Content-Type': 'application/json'
        },
        data: JSON.stringify({id:id}),
        success: function (data) {
            user = data.username;
            $('#username-log').text(user);
        },
        error: function (error) {
            console.error(error);
        }
    });
}

function register() {
    let username = $('#username-reg').val();
    let password = $('#password-reg').val();

    $('#username').val('');
    $('#password').val('');

    let requestBody = {
        username: username,
        password: password
    };

    console.log(requestBody);

    $.post({
        url: APP_SERVICE_URL + 'user/register',
        headers: {
            'Content-Type': 'application/json'
        },
        data: JSON.stringify(requestBody),
        success: function success (data) {
            renderLogin();
        },
        error: function error (error) {
            alert('Username exists!');
        }
    });
}

function login() {
    let username = $('#username').val();
    let password = $('#password').val();

    $('#username').val('');
    $('#password').val('');

    let requestBody = {
        username: username,
        password: password
    };

     $.post({
         url: APP_SERVICE_URL + 'user/login',
         headers: {
             'Content-Type': 'application/json'
         },
         data: JSON.stringify(requestBody),
         success: function (data) {
             // CHANGE CAPTION TO 'Welcome to Chat-Inc!'
             // Save token to localStorage using saveToken()
             // EXTRACT FROM JWT TOKEN currently logged in user's username
             // Logged-in-data visualize
             // Hide Guest Navbar

             if(data == null){

             alert('Incorrect user!');
             } else {
                 saveToken(data);
                 renderUserHeadline();
                 renderLoggedIn();
                 getUsername();
             }
         },
         error: function (error) {
             console.error(error);
         }
     });
}

function logout() {
    let token = localStorage.getItem('auth_token');
    evictToken(token);
    user = null;
    renderGuestHeadline();
    renderLogin();
}

function saveToken(token) {
    localStorage.setItem('auth_token', token);
}

function evictToken(token) {
    localStorage.removeItem('auth_token', token);
}

