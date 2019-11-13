function getFibonator() {
    let previous = 0;
    let next = 0;

    let getNext = function () {
        let result = previous + next;
        if (result === 1 || result === 0) {
            result = 1;
        }
        previous = next;
        next = result;
        return result;
    }

    return getNext;
}


let fib = getFibonator();
console.log(fib());
console.log(fib());
console.log(fib());
console.log(fib());
console.log(fib());