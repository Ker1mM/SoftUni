function toggle() {
    let state = document.getElementsByClassName('button')[0].textContent;

    if (state === 'More') {
        document.getElementsByClassName('button')[0].textContent = 'Less';
        document.getElementById('extra').setAttribute('style', 'display: block');
    } else if (state === 'Less') {
        document.getElementsByClassName('button')[0].textContent = 'More';
        document.getElementById('extra').setAttribute('style', 'display: none');
    }
}