function handleError(res) {
    if (!res.ok) {
        document.getElementById('stopName').innerHTML = 'Error';
    } else {
        return res.json();
    }
}

function showResult(res) {
    document.getElementById('stopName').innerHTML = res.name;
    for (let busId of Object.keys(res.buses)) {
        let arrivalTime = `Bus ${busId} arrives in ${res.buses[busId]} minutes`;
        let li = document.createElement('li');
        li.innerHTML = arrivalTime;
        document.getElementById('buses').appendChild(li);
    }
}

function getInfo() {
    let stopId = document.getElementById('stopId').value;
    document.getElementById('buses').innerHTML = '';
    fetch(`https://judgetests.firebaseio.com/businfo/${stopId}.json`)
        .then(response => handleError(response))
        .then(data => showResult(data))
}

