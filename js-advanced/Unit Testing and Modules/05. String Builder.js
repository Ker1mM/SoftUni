var chai = require('chai');
var assert = require('assert');
var expect = chai.expect;

class StringBuilder {
    constructor(string) {
        if (string !== undefined) {
            StringBuilder._vrfyParam(string);
            this._stringArray = Array.from(string);
        } else {
            this._stringArray = [];
        }
    }

    append(string) {
        StringBuilder._vrfyParam(string);
        for (let i = 0; i < string.length; i++) {
            this._stringArray.push(string[i]);
        }
    }

    prepend(string) {
        StringBuilder._vrfyParam(string);
        for (let i = string.length - 1; i >= 0; i--) {
            this._stringArray.unshift(string[i]);
        }
    }

    insertAt(string, startIndex) {
        StringBuilder._vrfyParam(string);
        this._stringArray.splice(startIndex, 0, ...string);
    }

    remove(startIndex, length) {
        this._stringArray.splice(startIndex, length);
    }

    static _vrfyParam(param) {
        if (typeof param !== 'string') throw new TypeError('Argument must be string');
    }

    toString() {
        return this._stringArray.join('');
    }
}


describe('StringBuilder', function () {
    it('Can be instantiated without anything', function () {
        let stringBuilderInstance = new StringBuilder();
        expect(stringBuilderInstance).to.be.a('object', 'Error while instantiating without anything!');
        expect(stringBuilderInstance._stringArray).to.have.lengthOf(0, 'Class instantiation is not correct without anything!');
    });

    it('Can be instantiated with a passed in string argument', function () {
        let stringBuilderInstance = new StringBuilder('Test');
        expect(stringBuilderInstance).to.be.a('object', 'Error while instantiating with string argument!');
        expect(stringBuilderInstance._stringArray).to.deep.equal(['T', 'e', 's', 't'], 'Class instantiation is not correct with string argument!');
        expect(stringBuilderInstance._stringArray).to.have.lengthOf(4, 'Class instantiation is not correct with string argument!');
    });

    it('Function append adds string to the end of the array', function () {
        let stringBuilderInstance = new StringBuilder("Front");
        let expected = Array.from("FrontEnd");
        stringBuilderInstance.append('End');
        let result = stringBuilderInstance._stringArray;
        expect(result).to.have.lengthOf(8, 'Append function does not work properly!');
        expect(result).to.deep.equal(expected, 'Append function does not work properly!');
    });

    it('Function prepend adds string to the beginning of the array', function () {
        let stringBuilderInstance = new StringBuilder("End");
        let expected = Array.from("FrontEnd");
        stringBuilderInstance.prepend('Front');
        let result = stringBuilderInstance._stringArray;
        expect(result).to.have.lengthOf(8, 'Prepend function does not work properly!');
        expect(result).to.deep.equal(expected, 'Prepend function does not work properly!');
    });

    it('Function insertAt adds string to the correct index', function () {
        let stringBuilderInstance = new StringBuilder("FrontEnd");
        let expected = Array.from("FrontMiddleEnd");
        stringBuilderInstance.insertAt('Middle', 5);
        let result = stringBuilderInstance._stringArray;
        expect(result).to.have.lengthOf(14, 'InsertAt function does not work properly!');
        expect(result).to.deep.equal(expected, 'InsertAt function does not work properly!');
    });

    it('Function remove removes correct elements', function () {
        let stringBuilderInstance = new StringBuilder("FrontMiddleEnd");
        let expected = Array.from("FrontEnd");
        stringBuilderInstance.remove(5, 6);
        let result = stringBuilderInstance._stringArray;
        expect(result).to.have.lengthOf(8, 'InsertAt function does not work properly!');
        expect(result).to.deep.equal(expected, 'InsertAt function does not work properly!');
    });

    it('Function toString returns correct string', function () {
        let stringBuilderInstance = new StringBuilder("FrontMiddleEnd");
        let expected = "FrontMiddleEnd";
        let result = stringBuilderInstance.toString();
        expect(result).to.equal(expected, 'ToString function does not work properly!');
    });

    it('Input should accept only string as argument', function () {
        expect(() => new StringBuilder(12)).to.throw('Argument must be string', 'Invalid constructor parameter!');
        let stringBuilderInstance = new StringBuilder();
        expect(() => stringBuilderInstance.append(12)).to.throw('Argument must be string', 'Invalid append parameter!');
        expect(() => stringBuilderInstance.prepend(12)).to.throw('Argument must be string', 'Invalid prepend parameter!');
        expect(() => stringBuilderInstance.insertAt(12, 0)).to.throw('Argument must be string', 'Invalid insertAt first parameter!');
    });

    it('Has all properties', function () {
        let stringBuilderInstance = new StringBuilder();
        expect(Object.getPrototypeOf(stringBuilderInstance).hasOwnProperty('append')).to.equal(true, 'Missing append property!');
        expect(Object.getPrototypeOf(stringBuilderInstance).hasOwnProperty('prepend')).to.equal(true, 'Missing prepend property!');
        expect(Object.getPrototypeOf(stringBuilderInstance).hasOwnProperty('insertAt')).to.equal(true, 'Missing insertAt property!');
        expect(Object.getPrototypeOf(stringBuilderInstance).hasOwnProperty('remove')).to.equal(true, 'Missing remove property!');
        expect(Object.getPrototypeOf(stringBuilderInstance).hasOwnProperty('toString')).to.equal(true, 'Missing toString property!');
    });
});