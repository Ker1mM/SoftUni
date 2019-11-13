function ArrayWithDelimeter(input) {
    let result = input.slice(0, input.length - 1).join(input[input.length - 1]);
    console.log(result);
}

function PrintNthElement(input) {
    let inputLength = input.length;
    let array = input.slice(0, inputLength - 1);
    let step = Number(input[inputLength - 1]);

    for (let i = 0; i < inputLength - 1; i += step) {
        console.log(array[i]);
    }
}

function AddRemove(input) {
    let result = [];
    let number = 1;

    input.forEach(element => {
        if (element === 'add') {
            result.push(number++);
        } else {
            result.pop();
            number++;
        }
    });

    if (result.length === 0) {
        console.log('Empty')
    } else {
        console.log(result.join('\n'));
    }

}

function RotateArray(input) {
    let array = input.slice(0, input.length - 1);
    let arrayLength = array.length;
    let rotationCount = input[input.length - 1] % arrayLength;

    let front = array.slice(arrayLength - rotationCount, arrayLength)
    let back = array.slice(0, arrayLength - rotationCount);
    let result = front.concat(back);
    console.log(result.join(' '));
}

function IncreasingSubsequence(input) {
    let biggest = input[0];
    let result = input.reduce((total, current) => {
        if (current >= biggest) {
            biggest = current;
            total.push(current)
        };

        return total;
    }, [])

    console.log(result.join('\n'));
}

function Sort2(input) {

    function Sort(a, b) {
        if (a.length > b.length) {
            return 1;
        }

        if (a.length < b.length) {
            return -1;
        }

        if (a.toLowerCase() > b.toLowerCase()) {
            return 1;
        }

        if (a.toLowerCase() < b.toLowerCase()) {
            return -1;
        }

        return 0;
    }

    let result = input.sort(Sort);
    console.log(result.join('\n'));
}

function MagicMatrices(input) {

    let sum = input[0].reduce((total, current) => { return total + current });
    let result = true;

    for (let i = 0; i < input.length; i++) {
        let col = input.reduce((total, current) => {
            return total + current[i];
        }, 0);

        let row = input[i].reduce((total, current) => { return total + current });

        if( row !== sum || col !== sum){
            result = false;
            break;
        }
    }

    console.log(result);
}

