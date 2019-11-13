function attachEventsListeners() {
    Array
        .from(document.getElementsByTagName('div'))
        .map(x => x.querySelector('[type="button"]').addEventListener('click', function (e) {
            let value = x.querySelector('[type="text"]').value;
            let type = x.querySelector('[type="text"]').id;
            convert(type, value);
        }));

    function convert(type, value) {
        let seconds = value;
        switch (type) {
            case 'days':
                seconds = value * 86400;
                break;
            case 'hours':
                seconds = value * 3600;
                break;
            case 'minutes':
                seconds = value * 60;
            default:
                break;
        }

        let minutes = (seconds / 60);
        let hours = (seconds / 3600);
        let days = (seconds / 86400);

        document.getElementById('days').value = days;
        document.getElementById('hours').value = hours;
        document.getElementById('minutes').value = minutes;
        document.getElementById('seconds').value = seconds;
    }
}