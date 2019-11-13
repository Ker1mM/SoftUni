const Console = require('./Console');
var chai = require('chai');
var expect = chai.expect;

describe('Console', function () {
    describe('Single parameter', function () {

        it('Should return string with string parameter', function () {
            let parameter = 'Test input!';
            expect(Console.writeLine(parameter)).to.equal('Test input!');
        })

        it('Should return string with object parameter', function () {
            let parameter = { input: 'Test' };
            expect(Console.writeLine(parameter)).to.equal('{"input":"Test"}');
        })
    });

    it('Should throw error if first argument is not string', function () {
        expect(() => Console.writeLine(2, 0, 1)).to.throw('No string format given!');
        expect(() => Console.writeLine({ input: 'Test' }, 0, 1)).to.throw('No string format given!');
    });

    it('Should throw when argument count does not match placeholder count', function () {
        var string = 'This {0} is {1} test {2}';
        expect(() => Console.writeLine(string, 'string', 'a')).to.throw('Incorrect amount of parameters given!');
        expect(() => Console.writeLine(string, 'string', 'a', 'input', 'invalid')).to.throw('Incorrect amount of parameters given!');
    });

    it('Should throw when placehodler order is not correct', function () {
        var testString1 = 'This {0} is {1} test {3}';
        var testString2 = 'This {1} is {2} test {3}';

        expect(() => Console.writeLine(testString1, 'string', 'a', 'input')).to.throw('Incorrect placeholders given!');
        expect(() => Console.writeLine(testString2, 'string', 'a', 'input')).to.throw('Incorrect placeholders given!');
    });

    it('Should return correct string with valid input', function () {
        var testString1 = 'This {0} is {1} test {2}';
        var testString2 = 'This {0} is {2} test {1}';
        var testString3 = 'This {2} is {1} test {0}';
        var testString4 = '{0} {1} {2} {3} {4} {5} {6} {7} {8} {9} {10} {11}';

        expect(Console.writeLine(testString1, 'string', 'a', 'input')).to.equal('This string is a test input');
        expect(Console.writeLine(testString2, 'string', 'input', 'a')).to.equal('This string is a test input');
        expect(Console.writeLine(testString3, 'input', 'a', 'string')).to.equal('This string is a test input');
        expect(Console.writeLine(testString4, 0, 1 ,2 ,3 ,4, 5, 6, 7, 8, 9, 10, 11)).to.equal('0 1 2 3 4 5 6 7 8 9 10 11');
    });
});