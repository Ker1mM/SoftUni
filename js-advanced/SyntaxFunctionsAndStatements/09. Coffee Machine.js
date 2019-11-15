function coffeeMachine(input) {
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