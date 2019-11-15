function validityChecker(input) {
    let x1 = input[0];
    let y1 = input[1];
    let x2 = input[2];
    let y2 = input[3];


    let oneTo0 = Math.sqrt((x1 * x1) + (y1 * y1));
    let twoTo0 = Math.sqrt((x2 * x2) + (y2 * y2));

    x1 -= x2;
    y1 -= y2;

    let oneToTwo = Math.sqrt((x1 * x1) + (y1 * y1));

    if (oneTo0 === parseInt(oneTo0, 10)) {
        console.log('{' + input[0] + ', ' + input[1] + '} to {0, 0} is valid');
    } else {
        console.log('{' + input[0] + ', ' + input[1] + '} to {0, 0} is invalid');
    }

    if (twoTo0 === parseInt(twoTo0, 10)) {
        console.log('{' + input[2] + ', ' + input[3] + '} to {0, 0} is valid');
    } else {
        console.log('{' + input[2] + ', ' + input[3] + '} to {0, 0} is invalid');
    }

    if (oneToTwo === parseInt(oneToTwo, 10)) {
        console.log('{' + input[0] + ', ' + input[1] + '} to {'+ input[2] + ', ' + input[3] +'} is valid');
    } else {
        console.log('{' + input[0] + ', ' + input[1] + '} to {'+ input[2] + ', ' + input[3] +'} is invalid');
    }
}
