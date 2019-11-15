function PrintNthElement(input) {
    let inputLength = input.length;
    let array = input.slice(0, inputLength - 1);
    let step = Number(input[inputLength - 1]);

    for (let i = 0; i < inputLength - 1; i += step) {
        console.log(array[i]);
    }
}