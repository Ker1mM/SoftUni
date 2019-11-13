(function solve() {
    Array.prototype.last = function () {
        let lastElementIndex = this.length - 1;
        return this[lastElementIndex];
    };

    Array.prototype.skip = function (n) {
        let result = this.slice(n, this.length);
        return result;
    };

    Array.prototype.take = function (n) {
        let result = this.slice(0, n);
        return result;
    };

    Array.prototype.sum = function () {
        let result = this.reduce((a, b) => a + b, 0);
        return result;
    };

    Array.prototype.average = function () {
        let sum = this.sum();
        let result = sum / (this.length);
        return result;
    };
}());

let array = [3, 2, 1, 5, 4];
console.log(array.average());
console.log(array);
