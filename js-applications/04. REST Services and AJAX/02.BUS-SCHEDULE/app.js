function solve() {
    let departButton = document.getElementById('depart');
    let arriveButton = document.getElementById('arrive');
    let info = document.getElementById('info');
    let nextId = 'depot';
    let nextName = '';

    function handleError(res) {
        if (!res.ok) {
            info.innerHTML = 'Error';
            arriveButton.setAttribute('disabled', '');
            departButton.setAttribute('disabled', '');
        } else {
            return res.json();
        }
    }

    function getNextStop(res) {
        nextId = res.next;
        nextName = res.name;
        info.innerHTML = `Next stop ${nextName}`
    }

    function depart() {
        departButton.setAttribute('disabled', '');
        arriveButton.removeAttribute('disabled');

        fetch(`https://judgetests.firebaseio.com/schedule/${nextId}.json`)
            .then(res => handleError(res))
            .then(data => getNextStop(data))
    }

    function arrive() {
        arriveButton.setAttribute('disabled', '');
        departButton.removeAttribute('disabled');

        info.innerHTML = `Arriving at ${nextName}`;
    }

    return {
        depart,
        arrive
    };
}

let result = solve();