export function loadAllPartials(mainPath, ctx, partials = {}) {
    checkAuthorization(ctx);

    partials.header = '../views/common/header.hbs';
    partials.footer = '../views/common/footer.hbs';
    ctx.loadPartials(partials)
        .then(function () {
            this.partial(`../views/${mainPath}`)
        });
}

function checkAuthorization(ctx) {
    if (sessionStorage.getItem('authtoken')) {
        ctx.isLogged = true;
        ctx.username = sessionStorage.getItem('username');
    }
}

export function displaySuccess(message, ctx, redirectRoute) {
    let div = getNotiElement('successBox', 'success');

    div.innerHTML = message;
    document.getElementById('notifications').innerHTML = '';
    document.getElementById('notifications').appendChild(div);


    
    setTimeout(function () {
        document.getElementById('notifications').innerHTML = '';
        ctx.redirect(redirectRoute);
    }, 1000);

}

export function displayError(message) {
    let div = getNotiElement('errorBox', 'danger');

    div.innerHTML = message;
    document.getElementById('notifications').innerHTML = '';
    document.getElementById('notifications').appendChild(div);

    setTimeout(function () {
        document.getElementById('notifications').innerHTML = '';
    }, 5000);
}

export function displayLoading() {
    let div = getNotiElement('laodingBox', 'info');

    div.innerHTML = 'Loading...';
    document.getElementById('notifications').innerHTML = '';
    document.getElementById('notifications').appendChild(div);
}

function getNotiElement(id, type) {
    let div = document.createElement('div');
    div.setAttribute('id', id);
    div.setAttribute('class', `alert alert-${type}`);
    div.setAttribute('role', 'alert');
    div.style.display = 'block';

    return div;
}