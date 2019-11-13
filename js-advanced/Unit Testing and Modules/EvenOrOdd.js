var chai = require('chai');
var assert = require('assert');
var expect = chai.expect;

isOddOrEven = function (string) {
    if (typeof(string) !== 'string') {
        return undefined;
    }
    if (string.length % 2 === 0) {
        return "even";
    }

    return "odd";
};

describe('isOddOrEven', function () {
    it('should return undefined with a number parameter', function () {
        expect(isOddOrEven(13)).to.equal(undefined,
            'Function did not return correct result!')
    });

    it('should return undefined with an object parameter', function () {
        expect(isOddOrEven({ name: 'George' })).to.equal(undefined,
            "Function did not return correct resul!")
    });

    it('should return correct result with an even length', function () {
        assert.equal(isOddOrEven("roar"), "even",
            "Function did not return correct result!")
    });

    it('should return correct result with an odd length', function () {
        expect(isOddOrEven("Peter")).to.equal("odd",
            "Function did not return correct result!")
    });

    it('should return correct result with an odd length', function () {
        expect(isOddOrEven("cat")).to.equal("odd",
            "Function did not return correct result!")
        expect(isOddOrEven("pet")).to.equal("odd",
            "Function did not return correct result!")
        expect(isOddOrEven("bird")).to.equal("even",
            "Function did not return correct result!")
    })
});