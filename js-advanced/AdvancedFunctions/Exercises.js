function SortArray(input, order) {

    var result = input;
    if (order === 'asc') {
        result = input.sort(function (a, b) { return a - b });
    } else if (order === 'desc') {
        result = input.sort(function (a, b) { return b - a });
    }

    return result;
}

function ArgumentInfo() {
    let types = [];

    for (let argument of arguments) {
        let type = typeof argument;

        let currentType = types.find(x => x.name === type)
        if (!currentType) {
            let newType = { name: type, count: 1 };
            types.push(newType);
        } else {
            currentType.count++;
        }

        console.log(`${type}: ${argument}`)
    }

    types.sort(function (a, b) { return b.count - a.count });

    for (let type of types) {
        console.log(`${type.name} = ${type.count}`);
    }
}

function FunctionalSum(num) {
    let sum = num;

    function result(num) {
        sum += num;
        return result;
    }

    result.toString = () => sum.toString();

    return result
};

function VectorMath() {
    let add = (vec1, vec2) => [vec1[0] + vec2[0], vec1[1] + vec2[1]];
    let multiply = (vec1, scalar) => [vec1[0] * scalar, vec1[1] * scalar];
    let length = (vec1) => Math.sqrt(Math.pow(vec1[0], 2) + Math.pow(vec1[1], 2));
    let dot = (vec1, vec2) => vec1[0] * vec2[0] + vec1[1] * vec2[1];
    let cross = (vec1, vec2) => vec1[0] * vec2[1] - vec1[1] * vec2[0];

    return { add, multiply, length, dot, cross };
};

let robot = (
    /* Judge Submission Start */
    function name() {
        let ingredients = {
            protein: {
                name: 'protein',
                quantity: 0
            },
            carbohydrate: {
                name: 'carbohydrate',
                quantity: 0
            },
            fat: {
                name: 'fat',
                quantity: 0
            },
            flavour: {
                name: 'flavour',
                quantity: 0
            }
        }

        let mealCooking = {
            apple: (quantity) => useRequiredIngredients([
                { element: ingredients.carbohydrate, amount: quantity },
                { element: ingredients.flavour, amount: quantity * 2 }
            ]),
            lemonade: (quantity) => useRequiredIngredients([
                { element: ingredients.carbohydrate, amount: quantity * 10 },
                { element: ingredients.flavour, amount: quantity * 20 }
            ]),
            burger: (quantity) => useRequiredIngredients([
                { element: ingredients.carbohydrate, amount: quantity * 5 },
                { element: ingredients.fat, amount: quantity * 7 },
                { element: ingredients.flavour, amount: quantity * 3 }
            ]),
            eggs: (quantity) => useRequiredIngredients([
                { element: ingredients.protein, amount: quantity * 5 },
                { element: ingredients.fat, amount: quantity },
                { element: ingredients.flavour, amount: quantity }
            ]),
            turkey: (quantity) => useRequiredIngredients([
                { element: ingredients.protein, amount: quantity * 10 },
                { element: ingredients.carbohydrate, amount: quantity * 10 },
                { element: ingredients.fat, amount: quantity * 10 },
                { element: ingredients.flavour, amount: quantity * 10 }
            ]), 
        }

        function useRequiredIngredients(requiredIngredients) {
            for (let i = 0; i < requiredIngredients.length; i++) {
                if (requiredIngredients[i].element.quantity < requiredIngredients[i].amount) {
                    returnTakenElements(i);
                    return `Error: not enough ${requiredIngredients[i].element.name} in stock`;
                }

                requiredIngredients[i].element.quantity -= requiredIngredients[i].amount;
            }

            return 'Success';

            function returnTakenElements(indexOfMissingElement) {
                for (let i = indexOfMissingElement - 1; i >= 0; i--) {
                    requiredIngredients[i].element.quantity += requiredIngredients[i].amount;
                }
            }
        }

        let commands = {
            restock: (microelement, quantity) => {
                ingredients[microelement].quantity += Number(quantity);
                return 'Success';
            },
            prepare: (recipe, quantity) => {
                let meal = mealCooking[recipe.toLowerCase()];
                if (meal) {
                    return meal(Number(quantity));
                }

                return `Error: recipe for ${recipe} does not exists!`;
            },
            report: () => Object.keys(ingredients)
                .map(name => `${name}=${ingredients[name].quantity}`)
                .join(' ')
        }

        return function (command) {
            if (command === undefined) {
                return;
            }

            let cmdTokens = command.split(' ');
            let cmd = commands[cmdTokens[0]];
            if (cmd) {
                return cmd(cmdTokens[1], cmdTokens[2]);
            }

            return 'Error: Command does not exists!';
        }
    }
    /* Judge Submission End */
)()

console.log(robot("prepare lemonade 4"));