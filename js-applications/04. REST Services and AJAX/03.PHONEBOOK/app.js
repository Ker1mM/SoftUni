function attachEvents() {
    document.getElementById('btnLoad').addEventListener('click', load);
    document.getElementById('btnCreate').addEventListener('click', create)
}

function create(el) {
    let person = document.getElementById('person').value;
    let phone = document.getElementById('phone').value;
    let entry = { person, phone };

    function clear() {
        document.getElementById('person').value = '';
        document.getElementById('phone').value = '';
    }

    fetch('https://phonebook-nakov.firebaseio.com/phonebook.json',
        {
            method: 'POST',
            body: JSON.stringify(entry)
        })
        .then(x => load())
        .then(x => clear())
}

function deleteNumber(el) {
    fetch(`https://phonebook-nakov.firebaseio.com/phonebook/${el.currentTarget.value}.json`,
        {
            method: 'DELETE'
        })
        .then(res => load())
}

function displayNumbers(data) {
    let phonebook = document.getElementById('phonebook');
    phonebook.innerHTML = '';
    for (let key of Object.keys(data)) {
        let li = document.createElement('li');
        let name = data[key].person;
        let phone = data[key].phone;
        let btn = document.createElement('button');
        btn.innerHTML = 'Delete';
        btn.value = key;
        btn.addEventListener('click', deleteNumber)
        li.innerHTML = `${name}:${phone}`;
        li.appendChild(btn);
        phonebook.appendChild(li);
    }

}

function load(el) {
    fetch('https://phonebook-nakov.firebaseio.com/phonebook.json')
        .then(res => res.json())
        .then(data => displayNumbers(data));


}

attachEvents();