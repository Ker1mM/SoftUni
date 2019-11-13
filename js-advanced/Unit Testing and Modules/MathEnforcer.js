var chai = require('chai');
var assert = require('assert');
var expect = chai.expect;

let mathEnforcer = {
    addFive: function (num) {
        if (typeof (num) !== 'number') {
            return undefined;
        }
        return num + 5;
    },
    subtractTen: function (num) {
        if (typeof (num) !== 'number') {
            return undefined;
        }
        return num - 10;
    },
    sum: function (num1, num2) {
        if (typeof (num1) !== 'number' || typeof (num2) !== 'number') {
            return undefined;
        }
        return num1 + num2;
    }
};

describe('mathEnforcer', function () {
    describe('addFive', function () {
        it('should return correct result with a non-number parameter', function () {
            expect(mathEnforcer.addFive('12')).to.equal(undefined, 'Incorrect parameter type!');
        });

        it('should return correct result with a non-number parameter', function () {
            expect(mathEnforcer.addFive({ type: 'Non-number' })).to.equal(undefined, 'Incorrect parameter type!');
        });

        it('should return correct result with integer parameter', function () {
            expect(mathEnforcer.addFive(5)).to.equal(10, 'Incorrect result with integer parameter!');
        });

        it('should return correct result with negative integer parameter', function () {
            expect(mathEnforcer.addFive(-5)).to.equal(0, 'Incorrect result with negative integer parameter!');
        });

        it('should return correct result with floating-point parameter', function () {
            expect(mathEnforcer.addFive(5.255)).to.closeTo(10.25, 0.01, 'Incorrect result with floating-point parameter!');
        });

        it('should return correct result with floating-point parameter', function () {
            expect(mathEnforcer.addFive(5.255)).to.closeTo(10.25, 0.01, 'Incorrect result with floating-point parameter!');
        });

        it('should return correct result with negative floating-point parameter', function () {
            expect(mathEnforcer.addFive(-5.255)).to.closeTo(-0.25, 0.01, 'Incorrect result with negative floating-point parameter!');
        });
    });

    describe('subtractTen', function () {
        it('should return correct result with a non-number parameter', function () {
            expect(mathEnforcer.subtractTen('12')).to.equal(undefined, 'Incorrect parameter type!');
        });

        it('should return correct result with a non-number parameter', function () {
            expect(mathEnforcer.subtractTen({ type: 'Non-number' })).to.equal(undefined, 'Incorrect parameter type!');
        });

        it('should return correct result with integer parameter', function () {
            expect(mathEnforcer.subtractTen(15)).to.equal(5, 'Incorrect result with integer parameter!');
        });

        it('should return correct result with negative integer parameter', function () {
            expect(mathEnforcer.subtractTen(-5)).to.equal(-15, 'Incorrect result with negative integer parameter!');
        });

        it('should return correct result with floating-point parameter', function () {
            expect(mathEnforcer.subtractTen(15.255)).to.closeTo(5.25, 0.01, 'Incorrect result with floating-point parameter!');
        });

        it('should return correct result with floating-point parameter', function () {
            expect(mathEnforcer.subtractTen(15.258)).to.closeTo(5.25, 0.01, 'Incorrect result with floating-point parameter!');
        });

        it('should return correct result with negative floating-point parameter', function () {
            expect(mathEnforcer.subtractTen(-5.252)).to.closeTo(-15.25, 0.01, 'Incorrect result with negative floating-point parameter!');
        });
    });

    describe('sum', function () {
        it('should return correct result with a non-number first parameter', function () {
            expect(mathEnforcer.sum('12', 12)).to.equal(undefined, 'Incorrect first parameter type!');
        });

        it('should return correct result with a non-number first parameter', function () {
            expect(mathEnforcer.sum({ number: 12 }, 12)).to.equal(undefined, 'Incorrect first parameter type!');
        });

        it('should return correct result with a non-number second parameter', function () {
            expect(mathEnforcer.sum(12, '12')).to.equal(undefined, 'Incorrect secont parameter type!');
        });

        it('should return correct result with a non-number second parameter', function () {
            expect(mathEnforcer.sum(12, { number: 12 })).to.equal(undefined, 'Incorrect second parameter type!');
        });

        it('should return correct result with integer parameters', function () {
            expect(mathEnforcer.sum(5, 15)).to.equal(20, 'Incorrect result!');
        });

        it('should return correct result with negative integer parameters', function () {
            expect(mathEnforcer.sum(-5, -15)).to.equal(-20, 'Incorrect result!');
        });

        it('should return correct result with floating parameters', function () {
            expect(mathEnforcer.sum(2.252, 1.502)).to.closeTo(3.75, 0.01, 'Incorrect result!');
        });

        it('should return correct result with negative floating parameters', function () {
            expect(mathEnforcer.sum(-2.558, -1.502)).to.closeTo(-4.05, 0.01, 'Incorrect result!');
        });
    });
});