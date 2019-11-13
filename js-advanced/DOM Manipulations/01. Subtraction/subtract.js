function subtract() {
    let firstNumber = document.getElementById('firstNumber').value;
    let secondNumber = document.getElementById('secondNumber').value;

    console.log(firstNumber);
    let result = Number(firstNumber) - Number(secondNumber);

    document.getElementById('result').textContent = result;
}