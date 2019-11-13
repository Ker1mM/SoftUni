class Kitchen {
    constructor(budget) {
        this.budget = budget;
        this.menu = {};
        this.productsInStock = {};
        this.actionsHistory = [];
    }

    loadProducts(products) {
        for (let product of products) {
            let tokens = product.split(' ');
            let name = tokens.slice(0, tokens.length - 2).join(' ');
            let quantity = Number(tokens[tokens.length - 2]);
            let price = Number(tokens[tokens.length - 1]);

            if (this.budget >= price) {
                if (this.productsInStock.hasOwnProperty(name)) {
                    this.productsInStock[name] += quantity;
                } else {
                    this.productsInStock[name] = quantity;
                }

                this.budget -= price;
                this.actionsHistory.push(`Successfully loaded ${quantity} ${name}`);
            } else {
                this.actionsHistory.push(`There was not enough money to load ${quantity} ${name}`);
            }
        }

        return this.actionsHistory.join('\n').trim() + '\n';
    }

    addToMenu(meal, neededProducts, price) {
        if (this.menu.hasOwnProperty(meal)) {
            return `The ${meal} is already in our menu, try something different.`;
        } else {
            this.menu[meal] = { products: neededProducts, price: Number(price) };
            return `Great idea! Now with the ${meal} we have ${Object.keys(this.menu).length} meals in the menu, other ideas?`;
        }
    }

    showTheMenu() {
        if (Object.keys(this.menu).length === 0) {
            return `Our menu is not ready yet, please come later...`;
        } else {
            let result = [];

            for (let menuItem of Object.keys(this.menu)) {
                result.push(`${menuItem} - $ ${this.menu[menuItem].price}`);
            }
            return result.join('\n').trim() + '\n';
        }
    }

    makeTheOrder(meal) {
        if (this.menu.hasOwnProperty(meal)) {

            let needed = {};
            for (const prdct of this.menu[meal].products) {
                let tokens = prdct.split(' ');
                let quantity = Number(tokens.pop());
                let name = tokens.join(' ');

                if (!this.productsInStock.hasOwnProperty(name) || this.productsInStock[name] < quantity) {
                    return (`For the time being, we cannot complete your order (${meal}), we are very sorry...`);
                }

                needed[name] = quantity;
            }

            for (let mealProduct of Object.keys(needed)) {
                this.productsInStock[mealProduct] -= needed[mealProduct];
            }
            this.budget += this.menu[meal].price;
            return `Your order (${meal}) will be completed in the next 30 minutes and will cost you ${this.menu[meal].price}.`;
        } else {
            return `There is not ${meal} yet in our menu, do you want to order something else?`;
        }
    }
}

let kitchen = new Kitchen(1000);
console.log(kitchen.loadProducts(['Banana 10 5', 'Banana 20 10', 'Strawberries 50 30', 'Yogurt 10 10', 'Yogurt 500 1500', 'Honey 5 50']));
console.log(kitchen.addToMenu('frozen Yogurt', ['Yogurt 1', 'Honey 1', 'Banana 1', 'Strawberries 10'], 9.99));
console.log(kitchen.addToMenu('Pizza', ['Flour 0.5', 'Oil 0.2', 'Yeast 0.5', 'Salt 0.1', 'Sugar 0.1', 'Tomato sauce 0.5', 'Pepperoni 1', 'Cheese 1.5'], 15.55));
console.log(kitchen.addToMenu('Pizza', ['Flour 0.5', 'Oil 0.2', 'Yeast 0.5', 'Salt 0.1', 'Sugar 0.1', 'Tomato sauce 0.5', 'Pepperoni 1', 'Cheese 1.5'], 15.55));
console.log(kitchen.showTheMenu());
console.log(kitchen.makeTheOrder('frozen Yogurt'));