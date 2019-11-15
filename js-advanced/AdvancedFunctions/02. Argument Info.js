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