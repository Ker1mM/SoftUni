var chai = require('chai');
var assert = require('assert');
var expect = chai.expect;

function lookupChar(string, index) {
    if (typeof (string) !== 'string' || !Number.isInteger(index)) {
        return undefined;
    }
    if (string.length <= index || index < 0) {
        return "Incorrect index";
    }

    return string.charAt(index);
}

describe('lookupChar', function () {

    it('should return undefined if first parameter is not string', function () {
        expect(lookupChar(12, 12)).to.equal(undefined,
            "Incorrect first parameter type!");
    });

    it('should return undefined if first parameter is not string', function () {
        expect(lookupChar({ name: 'Tony' }, 12)).to.equal(undefined,
            "Incorrect first parameter type!");
    });

    it('should return undefined if second parameter is not number', function () {
        expect(lookupChar('String', '12')).to.equal(undefined,
            "Incorrect second parameter type!");
    });

    it('should return undefined if second parameter is not number', function () {
        expect(lookupChar('String', 2.2)).to.equal(undefined,
            "Incorrect second parameter type!");
    });

    it('should return undefined if second parameter is not number', function () {
        expect(lookupChar('String', { name: 'Pesho' })).to.equal(undefined,
            "Incorrect second parameter type!");
    });

    it('should return correct error if index is invalid', function () {
        expect(lookupChar('String', 6)).to.equal('Incorrect index',
            "Incorrect index!");
    });

    it('should return correct error if index is invalid', function () {
        expect(lookupChar('String', -2)).to.equal('Incorrect index',
            "Incorrect index!");
    });

    it('should return correct result with correct parameters', function () {
        expect(lookupChar('String', 0)).to.equal('S',
            "Incorrect result!");
    });

    it('should return correct result with correct parameters', function () {
        expect(lookupChar('String', 5)).to.equal('g',
            "Incorrect result!");
    });

    it('should return correct result with correct parameters', function () {
        expect(lookupChar('String', 2)).to.equal('r',
            "Incorrect result!");
    });
});