(function solve() {
    String.prototype.ensureStart = function (str) {
        if (typeof str === 'string' && !this.toString().startsWith(str)) {
            return str + this.toString();
        }
        return this.toString();
    };

    String.prototype.ensureEnd = function (str) {
        if (typeof str === 'string' && !this.toString().endsWith(str)) {
            return this.toString() + str;
        }
        return this.toString();
    };

    String.prototype.isEmpty = function () {
        return this.toString() === '';
    };

    String.prototype.truncate = function (n) {
        let str = this.toString();
        if (n < 4) {
            return '.'.repeat(n);
        }
        else if (str.length < n) {
            return str;
        } else if (str.length > n) {

            let result = str;
            while (result.length > n && result.includes(' ')) {
                let lastSpace = result.lastIndexOf(' ');
                result = result.slice(0, lastSpace) + '...';
            }

            str = result;
        }

        if (str.length > n) {
            str = str.slice(0, n - 3) + '...';
        }

        return str;
    };

    String.format = function () {
        let result = arguments[0];
        for (let i = 1; i < arguments.length; i++) {
            result = result.replace(`{${i - 1}}`, arguments[i]);
        }

        return result;
    };
}());
