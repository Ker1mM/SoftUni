class Rat {
    constructor(name) {
        this.name = name;
        this.unity = [];
    }

    unite(newRat) {
        if (newRat && Object.getPrototypeOf(newRat) === Rat.prototype) {
            this.unity.push(newRat);
        }
    }

    getRats() {
        return this.unity;
    }

    toString() {
        let result = `${this.name}\n`;
        for (let rat of this.unity) {
            result += `##${rat.name}\n`;
        }

        return result;
    }
}