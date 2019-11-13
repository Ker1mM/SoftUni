function Fruit(fruit, quantity, price) {
    let inKgs = quantity / 1000;
    let totalPrice = price * inKgs;
    console.log('I need $' + Number(totalPrice).toFixed(2) + ' to buy ' + Number(inKgs).toFixed(2) + ' kilograms ' + fruit + '.');
}

function FindGCD(num1, num2) {
    function GCD(num1, num2) {
        if (num1 == 0) {
            return num2;
        }
        return GCD(num2 % num1, num1);
    }

    console.log(GCD(num1, num2))
}

function SameNumbers(number) {
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

function TimeToWalk(stepsCount, stepLength, speed) {
    Number.prototype.pad = function (size) {
        var s = String(this);
        while (s.length < (size || 2)) { s = "0" + s; }
        return s;
    }

    let distance = stepsCount * stepLength;
    let totalTime = parseInt(distance / 500) * 60;
    let speedMS = ((speed * 1000) / 3600);

    totalTime += Math.round(distance / speedMS);

    let seconds = parseInt(totalTime % 60);
    let minutes = parseInt((totalTime / 60) % 60, 10);
    let hours = parseInt(totalTime / 3600, 10);

    console.log(hours.pad(2) + ':' + minutes.pad(2) + ':' + seconds.pad(2));
}

function CalorieObject(objects) {
    let result = [];
    for (let i = 0; i < objects.length - 1; i += 2) {
        result.push(objects[i] + ': ' + objects[i + 1]);
    }

    console.log('{ ' + result.join(', ') + ' }');
}

function RoadRadar(input) {
    const motorwayLimit = 130;
    const interstateLimit = 90;
    const cityLimit = 50;
    const residentialLimit = 20;

    var speed = input[0];
    var location = input[1];

    var limit = 0;
    switch (location) {
        case 'city':
            limit = cityLimit;
            break;

        case 'motorway':
            limit = motorwayLimit;
            break;

        case 'residential':
            limit = residentialLimit;
            break;

        case 'interstate':
            limit = interstateLimit;
            break;

        default:
            break;
    }

    var aboveSpeedLimit = speed - limit;

    if (aboveSpeedLimit > 40) {
        console.log('reckless driving');
    } else if (aboveSpeedLimit > 20) {
        console.log('excessive speeding');
    } else if (aboveSpeedLimit > 0) {
        console.log('speeding');
    }
}

function CookingByNumbers(input) {
    let number = parseInt(input[0], 10);

    for (let i = 1; i < input.length; i++) {
        switch (input[i]) {
            case 'chop':
                number /= 2;
                break;
            case 'dice':
                number = Math.sqrt(number);
                break;
            case 'spice':
                number += 1;
                break;
            case 'bake':
                number *= 3;
                break;
            case 'fillet':
                number -= (number * 0.20);
                break;
            default:
                break;
        }
        console.log(number);
    }
}

function ValidityChecker(input) {
    let x1 = input[0];
    let y1 = input[1];
    let x2 = input[2];
    let y2 = input[3];


    let OneTo0 = Math.sqrt((x1 * x1) + (y1 * y1));
    let TwoTo0 = Math.sqrt((x2 * x2) + (y2 * y2));

    x1 -= x2;
    y1 -= y2;

    let OneToTwo = Math.sqrt((x1 * x1) + (y1 * y1));

    if (OneTo0 === parseInt(OneTo0, 10)) {
        console.log('{' + input[0] + ', ' + input[1] + '} to {0, 0} is valid');
    } else {
        console.log('{' + input[0] + ', ' + input[1] + '} to {0, 0} is invalid');
    }

    if (TwoTo0 === parseInt(TwoTo0, 10)) {
        console.log('{' + input[2] + ', ' + input[3] + '} to {0, 0} is valid');
    } else {
        console.log('{' + input[2] + ', ' + input[3] + '} to {0, 0} is invalid');
    }

    if (OneToTwo === parseInt(OneToTwo, 10)) {
        console.log('{' + input[0] + ', ' + input[1] + '} to {' + input[2] + ', ' + input[3] + '} is valid');
    } else {
        console.log('{' + input[0] + ', ' + input[1] + '} to {' + input[2] + ', ' + input[3] + '} is invalid');
    }
}

function CoffeeMachine(input) {
    const caffeine = 0.80;
    const decaf = 0.90;
    const tea = 0.80;

    let totalPrice = 0;
    for (let i = 0; i < input.length; i++) {


        let order = input[i].split(', ');
        let next = 0;

        let money = order[next++];
        let beverage = order[next++];
        let coffeeType = null;

        if (beverage === 'coffee') {
            coffeeType = order[next++];
        }

        let milk = order[next++];
        let sugar = null;
        if (milk === 'milk') {
            sugar = order[next++];
        } else {
            sugar = milk;
            milk = null;
        }

        let price = 0;

        if (beverage === 'tea') {
            price = tea;
        } else if (coffeeType === 'caffeine') {
            price = caffeine;
        } else if (coffeeType === 'decaf') {
            price = decaf;
        }

        if (milk !== null) {
            price = Math.round((price * 1.10)*10)/10;
        }

        if (sugar > 0) {
            price += 0.10;
        }

        if (price <= money) {
            totalPrice += price;
            let change = money-price;
            console.log('You ordered ' + beverage + '. Price: $' + Number(price).toFixed(2) + ' Change: $' + Number(change).toFixed(2));
        } else {
            let needed = price-money;
            console.log('Not enough money for ' + beverage + '. Need $' + Number(needed).toFixed(2) + ' more.')
        }
    }

    console.log('Income Report: $' + Number(totalPrice).toFixed(2));
}

CoffeeMachine(['1.00, coffee, caffeine, milk, 4', '0.40, tea, milk, 2', '1.00, coffee, decaf, 0']);