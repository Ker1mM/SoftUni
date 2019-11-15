function RotateArray(input) {
    let array = input.slice(0, input.length - 1);
    let arrayLength = array.length;
    let rotationCount = input[input.length - 1] % arrayLength;

    let front = array.slice(arrayLength - rotationCount, arrayLength)
    let back = array.slice(0, arrayLength - rotationCount);
    let result = front.concat(back);
    console.log(result.join(' '));
}