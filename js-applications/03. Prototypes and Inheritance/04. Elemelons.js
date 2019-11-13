function solve() {

    class Melon {
        constructor(weight, melonSort) {
            if (new.target === Melon) {
                throw new TypeError('Abstract class cannot be instantiated directly');
            }
            this.weight = weight;
            this.melonSort = melonSort;
            this._elementIndex = this.weight * (this.melonSort.length);
            this._element = this.constructor.name.replace('melon', '');
        }

        get elementIndex() {
            return this._elementIndex;
        }

        toString() {
            let result = [`Element: ${this._element}`,
            `Sort: ${this.melonSort}`,
            `Element Index: ${this.elementIndex}`];

            return result.join('\n');
        }
    }

    class Watermelon extends Melon {
        constructor(weight, melonSort) {
            super(weight, melonSort);
        }
    }

    class Firemelon extends Melon {
        constructor(weight, melonSort) {
            super(weight, melonSort);
        }
    }

    class Earthmelon extends Melon {
        constructor(weight, melonSort) {
            super(weight, melonSort);
        }
    }

    class Airmelon extends Melon {
        constructor(weight, melonSort) {
            super(weight, melonSort);
        }
    }

    class Melolemonmelon extends Watermelon {
        constructor(weight, melonSort) {
            super(weight, melonSort);
            this.morphOrder = ['Water', 'Fire', 'Earth', 'Air'];
            this.morph();
        }

        morph() {
            let next = this.morphOrder.shift();
            this._element = next;
            this.morphOrder.push(next);
        }
    }

    return { Melon, Watermelon, Firemelon, Airmelon, Earthmelon, Melolemonmelon };
}
solve();