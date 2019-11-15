function solve() {
    let inputText = document.getElementById('array').value;
    let input = JSON.parse(inputText);
    let key = input[0];
    let pattern = new RegExp(`(?<=([ ]|^)${key}\\s*?)([A-Z!%$#]{8,})(?=[ ,.]|$)`, 'gim');
    let result = document.getElementById('result');

    for (let i = 1; i < input.length; i++) {
        let text = input[i];
        let matches = text.match(pattern);

        if (matches !== null) {
            matches.forEach(element => {
                if (element === element.toUpperCase()) {
                    let encoded = encode(element);
                    input[i] = input[i].replace(element, encode);
                }
            });
        }
        ;
        let paragraph = document.createElement('p');
        paragraph.innerHTML = input[i];
        result.appendChild(paragraph);
    }

    function encode(string) {
        string = string.replace(/%/g, '2');
        string = string.replace(/!/g, '1');
        string = string.replace(/#/g, '3');
        string = string.replace(/\$/g, '4');
        string = string.toLowerCase();

        return string;
    }
}
