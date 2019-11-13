class Stringer {
    constructor(string, length) {
        this.innerString = string;
        this.innerLength = length;
    }

    increase(length) {
        this.innerLength += length;
    }

    decrease(length) {
        let newLength = this.innerLength - length;
        this.innerLength = newLength < 0 ? 0 : newLength;
    }

    toString() {
        let result = this.innerString.substring(0, this.innerLength);
        if (this.innerLength < this.innerString.length) {
            result += '...';
        }
        return result;
    }
}