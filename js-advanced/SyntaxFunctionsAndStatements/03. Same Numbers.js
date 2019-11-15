function sameNumbers(number) {
    number = Math.abs(number);

    let numberToCheck = number % 10;
    number = parseInt(number / 10, 10);

    let totalSum = numberToCheck;
    let result = true;
    while (number > 0) {
        let nextNumber = number % 10;
        number = parseInt(number / 10, 10);

        totalSum += nextNumber;
        if (result && (numberToCheck !== nextNumber)) {
            result = false;
        }
    }

    console.log(result);
    console.log(totalSum);
}