let PaymentPackage = require('./PaymentPackage')
var chai = require('chai');
var expect = chai.expect;

describe("PaymentPackage", function () {

    it("Can be instantiated with a string name and number value", function () {
        let result = new PaymentPackage('Test', 123);
        expect(result instanceof PaymentPackage).to.be.equal(true , 'Class is not instantiated correctly!');
    });

    it("Class is instantiated correctly", function () {
        let result = new PaymentPackage('Test', 123);

        expect(result.name).to.equal('Test', 'Property name is not set correctly!');
        expect(result.value).to.equal(123, 'Property value is not set correctly!');
        expect(result.VAT).to.equal(20, 'Default property VAT is not set correctly!');
        expect(result.active).to.equal(true, 'Default property active is not set correctly!');

        expect(() => new PaymentPackage(123, 123)).to.throw('Name must be a non-empty string', 'Class is not instantiated correctly!');
        expect(() => new PaymentPackage('', 123)).to.throw('Name must be a non-empty string', 'Class is not instantiated correctly!');
        expect(() => new PaymentPackage('123', '123')).to.throw('Value must be a non-negative number', 'Class is not instantiated correctly!');
        expect(() => new PaymentPackage('123', -12)).to.throw('Value must be a non-negative number', 'Class is not instantiated correctly!');
    });

    it('Accessor name works correctly', function () {
        let moq = new PaymentPackage('Test', 123);

        expect(moq.name).to.equal('Test', 'Get for accessor name does not work correctly!')
        moq.name = 'Edit';
        expect(moq._name).to.equal('Edit', 'Set for accessor name does not work correctly!')

        expect(() => moq.name = '').to.throw('Name must be a non-empty string', 'Set for accessor name does not work correctly!');
        expect(() => moq.name = 12).to.throw('Name must be a non-empty string', 'Set for accessor name does not work correctly!');
    });

    it('Accessor value works correctly', function () {
        let moq = new PaymentPackage('Test', 123);

        expect(moq.value).to.equal(123, 'Get for accessor value does not work correctly!')
        moq.value = 5;
        expect(moq._value).to.equal(5, 'Set for accessor value does not work correctly!')

        expect(() => moq.value = "10").to.throw('Value must be a non-negative number', 'Set for accessor value does not work correctly!');
        expect(() => moq.value = -10).to.throw('Value must be a non-negative number', 'Set for accessor value does not work correctly!');
    });

    it('Accessor VAT works correctly', function () {
        let moq = new PaymentPackage('Test', 123);

        expect(moq.VAT).to.equal(20, 'Get for accessor VAT does not work correctly!')
        moq.VAT = 5.2;
        expect(moq._VAT).to.equal(5.2, 'Set for accessor VAT does not work correctly!')

        expect(() => moq.VAT = "10").to.throw('VAT must be a non-negative number', 'Set for accessor VAT does not work correctly!');
        expect(() => moq.VAT = -10).to.throw('VAT must be a non-negative number', 'Set for accessor VAT does not work correctly!');
    });

    it('Accessor active works correctly', function () {
        let moq = new PaymentPackage('Test', 123);

        expect(moq.active).to.equal(true, 'Get for accessor active does not work correctly!')
        moq.active = false;
        expect(moq._active).to.equal(false, 'Set for accessor active does not work correctly!')

        expect(() => moq.active = "10").to.throw('Active status must be a boolean', 'Set for accessor active does not work correctly!');
        expect(() => moq.active = 12).to.throw('Active status must be a boolean', 'Set for accessor active does not work correctly!');
        expect(() => moq.active = { status: false }).to.throw('Active status must be a boolean', 'Set for accessor active does not work correctly!');
    });

    it('toString works correctly', function () {
        let moq = new PaymentPackage('Test', 123);
        let activeResult = [
            `Package: Test`,
            `- Value (excl. VAT): 123`,
            `- Value (VAT 20%): ${123 * (1 + 20 / 100)}`
        ].join('\n');

        let activeResultWithZero = [
            `Package: Test`,
            `- Value (excl. VAT): 0`,
            `- Value (VAT 20%): 0`
        ].join('\n');

        let inactiveResult = [
            `Package: Test (inactive)`,
            `- Value (excl. VAT): 123`,
            `- Value (VAT 20%): ${123 * (1 + 20 / 100)}`
        ].join('\n');

        expect(moq.toString()).to.equal(activeResult, 'ToString does not return correct result!');
        moq.active = false;
        expect(moq.toString()).to.equal(inactiveResult, 'ToString does not return correct result!');
        moq.value = 0;
        moq.active = true;
        expect(moq.toString()).to.equal(activeResultWithZero, 'ToString does not return correct result!');
    });

    it('Class has all properties', function () {
        let moq = new PaymentPackage('Test', 123);
        expect(moq.hasOwnProperty('_name')).to.equal(true, 'Property name is missing!');
        expect(moq.hasOwnProperty('_value')).to.equal(true, 'Property value is missing!');
        expect(moq.hasOwnProperty('_VAT')).to.equal(true, 'Property VAT is missing!');
        expect(moq.hasOwnProperty('_active')).to.equal(true, 'Property active is missing!');
        expect(Object.getPrototypeOf(moq).hasOwnProperty('toString')).to.equal(true, 'Property toString is missing!');
    });
});