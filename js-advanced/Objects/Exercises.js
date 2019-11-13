function HeroicInventory(input) {

    let result = [];
    for (const hero of input) {
        let info = hero.split(' / ');
        let heroInfo = {};
        heroInfo.name = info[0];
        heroInfo.level = Number(info[1]);
        heroInfo.items = [];
        if (info[2]) {
            heroInfo.items = info[2].split(', ');
        }
        result.push(heroInfo);
    }

    console.log(JSON.stringify(result));
}

function JSONTable(input) {
    let result = ["<table>"]

    for (const row of input) {
        let arr = JSON.parse(row);
        result.push('<tr>');
        addRow(arr);
        result.push('</tr>');
    }
    result.push('</table>');

    function addRow(row) {
        for (const value of Object.values(row)) {
            let resultRow = escapeHtml(String(value));
            result.push(`<td>${resultRow}</td>`)
        }
    }

    console.log(result.join('\n'));

    function escapeHtml(unsafe) {
        return unsafe
            .replace(/&/g, "&amp;")
            .replace(/</g, "&lt;")
            .replace(/>/g, "&gt;")
            .replace(/"/g, "&quot;")
            .replace(/'/g, "&#039;");
    }
}

function CappyJuice(input) {
    let juices = {}
    let bottles = {};

    for (const juice of input) {
        let juiceInfo = juice.split(' => ');
        let juiceType = juiceInfo[0];
        let juiceQuantity = Number(juiceInfo[1]);

        if (juices[juiceType] === undefined) {
            juices[juiceType] = juiceQuantity;
        } else {
            juices[juiceType] += juiceQuantity;
        }

        let bottleCount = parseInt(juices[juiceType] / 1000, 10);
        if (bottleCount > 0) {
            if (bottles[juiceType] === undefined) {
                bottles[juiceType] = bottleCount;
            } else {
                bottles[juiceType] += bottleCount;
            }

            juices[juiceType] -= (1000 * bottleCount);
        }
    }

    for (const bottle in bottles) {
        console.log(`${bottle} => ${bottles[bottle]}`);
    }
}

function StoreCatalogue(input) {
    let ordered = input.sort();


    let currentLetter = '';
    for (const product of ordered) {
        let productInfo = product.split(' : ');
        let productName = productInfo[0];
        let productPrice = productInfo[1];

        if (productName[0] !== currentLetter) {
            currentLetter = productName[0];
            console.log(currentLetter)
        }

        console.log(`  ${productName}: ${productPrice}`)
    }
}

function AutoEngineeringCompany(input) {
    let producedCars = [];

    for (const vehicle of input) {
        let carInfo = vehicle.split(' | ');
        let carBrand = carInfo[0];
        let carModel = carInfo[1];
        let carCount = Number(carInfo[2]);


        if (producedCars[carBrand] !== undefined) {
            if (producedCars[carBrand].models[carModel] !== undefined) {
                producedCars[carBrand].models[carModel] += carCount;
            } else {
                producedCars[carBrand].models[carModel] = carCount;
            }
        } else {
            producedCars[carBrand] = {};
            producedCars[carBrand].models = [];
            producedCars[carBrand].models[carModel] = carCount;
        }
    }

    for (const vehicle in producedCars) {
        console.log(vehicle);
        for (const car in producedCars[vehicle].models) {
            console.log(`###${car} -> ${producedCars[vehicle].models[car]}`);
        }
    }
}

function SystemComponents(input) {

    let systems = [];

    for (const system of input) {
        let systemComponents = system.split(' | ');
        let systemName = systemComponents[0];
        let component = systemComponents[1];
        let subcomponent = systemComponents[2];

        if (systems[systemName]) {

            if (systems[systemName].components[component]) {
                systems[systemName].components[component].subcomponent.push(subcomponent);
            } else {
                systems[systemName].components[component] = {};
                systems[systemName].components[component].subcomponent = [];
                systems[systemName].components[component].subcomponent.push(subcomponent);
            }

        } else {
            systems[systemName] = {};
            systems[systemName].components = [];

            systems[systemName].components[component] = {};
            systems[systemName].components[component].subcomponent = [];
            systems[systemName].components[component].subcomponent.push(subcomponent);
        }
    }

    function SortSubcomponentCount(a, b) {
        if (a.length() > b.length) {
            return -1;
        }
        if (b.length() > a.length()) {
            return 1;
        }
        return 0;
    }

    function SortSystems(a, b) {
        if (a.length() == b.length()) {
            if (a.toLower() < b.toLower()) {
                return 1;
            }
            if (a.toLower() < b.toLower()) {
                return -1;
            }
            return 0;
        }
        return SortSubcomponentCount(a, b);
    }

    for (const system in systems.sort(SortSystems)) {
        console.log(system);
        for (const component in systems[system].components.sort(function(a, b){return SortSubcomponentCount(a, b)})) {
            console.log(`|||${component}`)
            for (const subcomponent of systems[system].components[component].subcomponent) {
                console.log(`||||||${subcomponent}`);
            }
        }
    }
}

SystemComponents(['SULS | Main Site | Home Page',
    'SULS | Main Site | Login Page',
    'SULS | Main Site | Register Page',
    'SULS | Judge Site | Login Page',
    'SULS | Judge Site | Submittion Page',
    'Lambda | CoreA | A23',
    'SULS | Digital Site | Login Page',
    'Lambda | CoreB | B24',
    'Lambda | CoreA | A24',
    'Lambda | CoreA | A25',
    'Lambda | CoreC | C4',
    'Indice | Session | Default Storage',
    'Indice | Session | Default Security'
]);