function heroicInventory(input) {

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