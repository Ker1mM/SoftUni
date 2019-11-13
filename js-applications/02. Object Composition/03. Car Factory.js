function solve(input) {

    function getEngine({ power }) {
        let volume = 3500;
        if (power <= 90) {
            power = 90;
            volume = 1800;
        } else if (power <= 120) {
            power = 120;
            volume = 2400;
        } else {
            power = 200;
        }

        return { power, volume };
    }

    function getCarriage(input) {
        let { carriage } = input;
        let { color } = input;

        let type = 'hatchback';

        if (carriage.toLowerCase() === 'coupe') {
            type = 'coupe';
        }

        return { type, color };
    }

    function getWheels({ wheelsize }) {
        let newSize = Math.floor(wheelsize);
        if (newSize % 2 === 0) {
            newSize--;
        }

        return [newSize, newSize, newSize, newSize];
    }

    let { model } = input;
    let engine = getEngine(input);
    let carriage = getCarriage(input);
    let wheels = getWheels(input);

    return { model, engine, carriage, wheels };
}

console.log(
    solve(
        {
            model: 'VW Golf II',
            power: 110,
            color: 'blue',
            carriage: 'coupe',
            wheelsize: 14
        }
    )
)