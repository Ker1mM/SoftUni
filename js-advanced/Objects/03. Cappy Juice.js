function cappyJuice(input) {
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

            juices[juiceType] -= (1000*bottleCount);
        }
    }

    for (const bottle in bottles) {
        console.log(`${bottle} => ${bottles[bottle]}`);
    }
}