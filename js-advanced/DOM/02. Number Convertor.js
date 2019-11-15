function solve() {
    let selectMenu = document.getElementById('selectMenuTo');
    let hexadecimal = document.createElement('option');
    hexadecimal.setAttribute('value', 'hexadecimal');
    hexadecimal.text = 'Hexadecimal';
    let binary = document.createElement('option');
    binary.setAttribute('value', 'binary');
    binary.text = 'Binary';

    selectMenu.appendChild(hexadecimal);
    selectMenu.appendChild(binary);

    document.getElementsByTagName('button')[0].addEventListener('click', convert);

    function convert() {
        let input = Number(document.getElementById('input').value);
        let convertTo = document.getElementById('selectMenuTo').value;

        let result = 'Please enter a number!';
        if (input) {
            if (convertTo === 'hexadecimal') {
                result = input.toString(16).toUpperCase();
            } else if (convertTo === 'binary') {
                result = input.toString(2);
            } else {
                result = 'Select to what to convert!'
            }
        }
        document.getElementById('result').value = result;
    }
}