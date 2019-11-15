function solve() {
	let input = document.getElementById('input').value;
	input = input.split('').filter(x => x == 1 || x== 0).join('');
	let inputSum = input.split('').filter(x => x == 1).length;
	let sum = getSingleDigit(inputSum);
	let elementLength = input.length - (2 * sum);
	let resultedString = input.substr(sum, elementLength);
	let result = '';
	for (let i = 0; i < resultedString.length; i += 8) {
		let currentString = resultedString.substr(i, 8);
		let currentNumber = Number(currentString);
		let currentASCII = parseInt(currentNumber, 2);
		let currentChar = String.fromCharCode(currentASCII);
		if ((/[a-zA-Z ]/).test(currentChar)) {
			result += currentChar;
		}
	}
	document.getElementById('resultOutput').innerHTML = result;
	function getSingleDigit(number) {
		while (number > 9) {
			let current = 0;
			let numberToString = number.toString();
	
			for (let i = 0; i < numberToString.length; i++) {
				current += Number(numberToString[i]);
			}
	
			number = current;
		}
	
		return number;
	}

}
