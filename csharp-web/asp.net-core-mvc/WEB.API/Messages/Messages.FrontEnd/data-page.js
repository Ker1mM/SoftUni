
function renderLogin() {
    renderGuestHeadline();
    $('#data').empty().append('<div id="login">' +
        '<h1 class="text-center">Login</h1>'+
        '<div class="form-group w-50 mx-auto">' +
        '<label for="username" class="font-weight-bold">Username</label>' +
        '<input class="form-control" name="username" id="username" type="text" placeholder="Username..." >' +
        '</div>' +
        '<div class="form-group w-50 mx-auto">' +
        '<label for="password" class="font-weight-bold">Password</label>' +
        '<input class="form-control" name="password" id="password" type="password" placeholder="Password..." >' +
        '</div>' +
        '<div class="d-flex justify-content-center">' +
        '<button class="btn btn-dark" onclick="login()" id="login">Login</button>' +
        '</div>');
}

function renderRegister() {
    renderGuestHeadline();
    $('#data').empty().append('<div id="register">' +
        '<h1 class="text-center">Register</h1>'+
        '<div class="form-group w-50 mx-auto">' +
        '<label for="username-reg" class="font-weight-bold">Username</label>' +
        '<input class="form-control" name="username-reg" id="username-reg" type="text" placeholder="Username..." >' +
        '</div>' +
        '<div class="form-group w-50 mx-auto">' +
        '<label for="password-reg" class="font-weight-bold">Password</label>' +
        '<input class="form-control" name="password-reg" id="password-reg" type="password" placeholder="Password..." >' +
        '</div>' +
        '<div class="d-flex justify-content-center">' +
        '<button class="btn btn-dark" onclick="register()" id="register">Register</button>' +
        '</div>');
}

function renderGuestHeadline() {
    $('#headline').empty().append('<hr style="height: 2px" class="bg-dark"/>' +
        '<h2 class="text-center">Choose your username to begin chatting!</h2>'+
        '<hr style="height: 2px" class="bg-dark"/>'+
        '<div class="d-flex justify-content-around">' +
        '<button class="btn btn-dark" onclick="renderLogin()">Login</button>'+
        '<button class="btn btn-dark" onclick="renderRegister()">Register</button>'+
        '</div>');
}

function renderUserHeadline() {
    $('#headline').empty().append('<hr style="height: 2px" class="bg-dark"/>' +
        '<h2 class="text-center">Welcome to Chat-Inc!</h2>'+
        '</div>');
}

function renderLoggedIn(){
    $('#data').empty().append('<div id="logged-in-data">' +
        '<div class="form-group w-50 mx-auto">' +
        '<label for="username-log" class="font-weight-bold">Username</label>' +
        '<h3 id="username-log"></h3>' +
        '</div>' +
        '<div class="d-flex justify-content-center">' +
        '<button class="btn btn-dark" id="logout-button" onclick="logout()">Logout</button>' +
        '</div>')
}

renderLogin();
renderGuestHeadline();