class Hex {
    constructor(value) {
        this.value = value;
    }

    valueOf() {
        return this.value;
    }

    toString() {
        let result = '0x' + this.value.toString(16).toUpperCase();
        return result;
    }

    plus(number) {
        let result = this.value;
        if (number instanceof Hex) {
            result += number.value;
        } else if (number instanceof Number) {
            result += number;
        }

        return new Hex(result);
    }

    minus(number) {
        let result = this.value;
        if (number instanceof Hex) {
            result -= number.value;
        } else if (number instanceof Number) {
            result -= number;
        }

        return new Hex(result);
    }

    parse(string) {
        let hexNumber = string.substr(2);

        let number = this.parseInt(hexNumber, 16);
        return number;
    }
}

// let FF = new Hex(255);
// console.log(FF.toString());
// FF.valueOf() + 1 == 256;
// let a = new Hex(10);
// let b = new Hex(5);
// console.log(a.plus(b).toString());
// console.log(a.plus(b).toString() === '0xF');