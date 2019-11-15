function fruit(fruit, quantity, price) {
    let inKgs = quantity / 1000;
    let totalPrice = price * inKgs;
    console.log('I need $' + Number(totalPrice).toFixed(2) + ' to buy ' + Number(inKgs).toFixed(2) + ' kilograms ' + fruit + '.');
}