function arenaTier(input) {
    let pool = {}

    for (let data of input) {
        if (data === 'Ave Cesar') {
            break;
        }
        let splitted = data.split(/ -> | vs /);

        if (splitted.length > 2) {
            let gladiator = splitted[0].trim();
            let technique = splitted[1].trim();
            let skill = Number(splitted[2].trim());

            if (pool.hasOwnProperty(gladiator) === false) {
                pool[gladiator] = {
                    totalPoints: 0,
                    techniques: {}
                };
            }

            if (pool[gladiator]['techniques'].hasOwnProperty(technique) === false) {
                pool[gladiator]['techniques'][technique] = 0;
                pool[gladiator]["totalPoints"] += skill;
            }

            if (skill > pool[gladiator]['techniques'][technique]) {
                pool[gladiator]['techniques'][technique] = skill;

                let diff = skill - pool[gladiator]['techniques'][technique];
                pool[gladiator]["totalPoints"] += diff;
            }
        }

        else {
            let gladiator1 = splitted[0].trim();
            let gladiator2 = splitted[1].trim();

            if (pool.hasOwnProperty(gladiator1) && pool.hasOwnProperty(gladiator2)) {
                let haCommonTechnique = false;

                for (let t1 of Object.entries(pool[gladiator1]['techniques'])) {
                    for (let t2 of Object.entries(pool[gladiator2]['techniques'])) {
                        if (t1[0] === t2[0]) {
                            haCommonTechnique = true;
                            break;
                        }
                    }
                    if (haCommonTechnique) {
                        break;
                    }
                }

                if (haCommonTechnique) {
                    if (pool[gladiator1]["totalPoints"] > pool[gladiator2]["totalPoints"]) {
                        delete pool[gladiator2];
                    }
                    else if (pool[gladiator2]["totalPoints"] > pool[gladiator1]["totalPoints"]) {
                        delete pool[gladiator1];
                    }
                }
            }
        }
    }

    let result = Object.entries(pool).filter(x => x[1] !== null)
        .sort((a, b) => b[1]["totalPoints"] === a[1]["totalPoints"] ?
            a[0] - b[0] : b[1]["totalPoints"] - a[1]["totalPoints"]);

    for (let gladiator of result) {
        console.log(`${gladiator[0]}: ${gladiator[1]["totalPoints"]} skill`);

        let sortedTechniques = Object.entries(gladiator[1]["techniques"])
            .sort((a, b) => b[1] === a[1] ?
                stringSort(a[0], b[0]) : b[1] - a[1]);

        for (let technique of sortedTechniques) {
            console.log(`- ${technique[0]} <!> ${technique[1]}`);
        }
    }

    function stringSort(a, b) {
        if (a < b) {
            return -1;
        } else if (b < a) {
            return 1;
        }

        return 0;
    }
}