function solve() {

    let namePattern = /(?<= )([A-Z][a-zA-Z]*-[A-Z][a-zA-Z]*)((\.-[A-Z][a-zA-Z]*)?)(?= )/g;
    let airportPattern = /(?<= )([A-Z]{3}\/[A-Z]{3})(?= )/g;
    let flightNumberPattern = /(?<= )([A-Z]{1,3}[0-9]{1,5})(?= )/g;
    let companyPattern = /(?<=- )([A-Z][a-zA-Z]*\*[A-Z][a-zA-Z]*)(?= )/g

    debugger;
    let input = document.getElementById('string').value;
    let tokens = input.split(', ');
    let outputCode = tokens[1];
    let text = tokens[0];

    let name = text.match(namePattern)[0].replace(/-/g, ' ').trim();
    let airport = text.match(airportPattern)[0].split('/');
    let flightNumber = text.match(flightNumberPattern)[0].trim();
    let company = text.match(companyPattern)[0].replace(/[- ]/g, '').replace(/\*/g, ' ').trim();

    let result;
    switch (outputCode) {
        case 'name':
            result = `Mr/Ms, ${name}, have a nice flight!`;
            break;
        case 'flight':
            result = `Your flight number ${flightNumber} is from ${airport[0]} to ${airport[1]}.`
            break;
        case 'company':
            result = `Have a nice flight with ${company}.`;
            break;
        default:
            result = `Mr/Ms, ${name}, your flight number ${flightNumber} is from ${airport[0]} to ${airport[1]}. Have a nice flight with ${company}.`;
            break;
    }

    document.getElementById('result').innerHTML = result;
}
