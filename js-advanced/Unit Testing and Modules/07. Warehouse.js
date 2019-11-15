const Warehouse = require('./Warehouse');
var chai = require('chai');
var expect = chai.expect;

describe("Test", function () {
    let warehouse;
    beforeEach(function () {
        warehouse = new Warehouse(5);
    });

    it('should give correct message for empty warehouse', function () {
        expect(warehouse.revision()).to.be.equal('The warehouse is empty')
    })
});

describe('Warehouse', function () {
    describe('Constructor', function () {
        it('Should create instance of class with correct parameters', function () {
            let moq = new Warehouse(10);
            expect(moq instanceof Warehouse).to.equal(true, 'Incorrect class instance!')
            expect(moq._capacity).to.equal(10, 'Incorrect value assigned to capacity!');
            expect(moq.availableProducts).to.deep.equal({ 'Food': {}, 'Drink': {} }, 'Incorrect default value for availableProducts!');
        });

        it('Should throw error with invalid parameters', function () {
            expect(() => new Warehouse('12')).to.throw('Invalid given warehouse space', 'Instance created with string as parameter!');
            expect(() => new Warehouse({ capacity: 12 })).to.throw('Invalid given warehouse space', 'Instance created with object as parameter!');
            expect(() => new Warehouse()).to.throw('Invalid given warehouse space', 'Instance created with no parameter!');
        });
    });

    describe('Class has all properties', function () {

        it('Should have all properties', function () {
            let moq = new Warehouse(10);
            expect(moq.hasOwnProperty('_capacity')).to.equal(true, '_capacity property missing!');
            expect(moq.hasOwnProperty('availableProducts')).to.equal(true, 'availableProducts property missing!');
        });

        it('Should have all methods', function () {
            let moq = new Warehouse(10);
            expect(Object.getPrototypeOf(moq).hasOwnProperty('addProduct')).to.equal(true, 'addProduct method missing!')
            expect(Object.getPrototypeOf(moq).hasOwnProperty('orderProducts')).to.equal(true, 'orderProducts method missing!')
            expect(Object.getPrototypeOf(moq).hasOwnProperty('occupiedCapacity')).to.equal(true, 'occupiedCapacity method missing!')
            expect(Object.getPrototypeOf(moq).hasOwnProperty('revision')).to.equal(true, 'revision method missing!')
            expect(Object.getPrototypeOf(moq).hasOwnProperty('scrapeAProduct')).to.equal(true, 'scrapeAProduct method missing!')
        });
    });

    describe('addProduct', function () {
        it('Should add product with correct parameters', function () {
            let moq = new Warehouse(2);
            moq.addProduct('Drink', 'Cola', 1);
            moq.addProduct('Food', 'Steak', 1);
            expect(moq.availableProducts['Drink'].hasOwnProperty('Cola')).to.equal(true, 'Error while adding product!');
            expect(moq.availableProducts['Food'].hasOwnProperty('Steak')).to.equal(true, 'Error while adding product!');

            expect(moq.availableProducts['Drink']['Cola']).to.equal(1, 'Icorrect amount added!');
            expect(moq.availableProducts['Food']['Steak']).to.equal(1, 'Icorrect amount added!');
        })

        it('Should add same product twice with correct parameters', function () {
            let moq = new Warehouse(4);
            moq.addProduct('Drink', 'Cola', 1);
            moq.addProduct('Drink', 'Cola', 1);
            moq.addProduct('Food', 'Steak', 1);
            moq.addProduct('Food', 'Steak', 1);

            expect(moq.availableProducts['Drink'].hasOwnProperty('Cola')).to.equal(true, 'Error while adding product!');
            expect(moq.availableProducts['Drink']['Cola']).to.equal(2, 'Icorrect amount added!');
            expect(moq.availableProducts['Food'].hasOwnProperty('Steak')).to.equal(true, 'Error while adding product!');
            expect(moq.availableProducts['Food']['Steak']).to.equal(2, 'Icorrect amount added!');
        })

        it('Should throw error when capacity is lower than quantity', function () {
            let moq = new Warehouse(1);
            expect(() => moq.addProduct('Food', 'Steak', 2)).to.throw('There is not enough space or the warehouse is already full', 'Error while adding with no capacity!');
            expect(() => moq.addProduct('Drink', 'Cola', 2)).to.throw('There is not enough space or the warehouse is already full', 'Error while adding with no capacity!');
            expect(moq.occupiedCapacity()).to.equal(0);
        });

        it('Should throw error when adding with capacity reached', function () {
            let moq = new Warehouse(1);
            moq.addProduct('Drink', 'Cola', 1);
            expect(() => moq.addProduct('Food', 'Steak', 1)).to.throw('There is not enough space or the warehouse is already full', 'Error while adding with no capacity!');
            expect(() => moq.addProduct('Drink', 'Cola', 1)).to.throw('There is not enough space or the warehouse is already full', 'Error while adding with no capacity!');
        });

        it('Should throw error when adding with capacity reached', function () {
            let moq = new Warehouse(3);
            moq.addProduct('Food', 'Steak', 1);
            expect(() => moq.addProduct('Food', 'Steak', 3)).to.throw('There is not enough space or the warehouse is already full', 'Error while adding with no capacity!');
            moq.addProduct('Drink', 'Cola', 1);
            expect(() => moq.addProduct('Drink', 'Cola', 2)).to.throw('There is not enough space or the warehouse is already full', 'Error while adding with no capacity!');
        });
    });

    describe('orderProducts', function () {
        it('Should return', function () {
            let moq = new Warehouse(10);
        });
        it('Should sort only Food type in descending order by quantity', function () {
            let moq = new Warehouse(10);
            moq.addProduct('Food', 'Afood', 1);
            moq.addProduct('Food', 'Bfood', 2);
            moq.addProduct('Food', 'Cfood', 3);
            let actual = moq.orderProducts('Food');
            let expected = { Cfood: 3, Bfood: 2, Afood: 1 };
            for (let i = 0; i < Object.keys(expected).length; i++) {
                let actualKey = Object.keys(actual)[i];
                let expectedKey = Object.keys(expected)[i];
                expect(actualKey).to.equal(expectedKey, 'Products are not sorted correctly!');
                expect(actual[actualKey]).to.equal(expected[expectedKey], 'Products are not sorted correctly!');
            }
        });

        it('Should sort only Drink type in descending order by quantity', function () {
            let moq = new Warehouse(10);
            moq.addProduct('Drink', 'ADrink', 1);
            moq.addProduct('Drink', 'BDrink', 1);
            moq.addProduct('Drink', 'CDrink', 1);
            moq.addProduct('Drink', 'CDrink', 1);
            let actual = moq.orderProducts('Drink');
            let expected = { CDrink: 2, ADrink: 1, BDrink: 1 };
            for (let i = 0; i < Object.keys(expected).length; i++) {
                let actualKey = Object.keys(actual)[i];
                let expectedKey = Object.keys(expected)[i];
                expect(actualKey).to.equal(expectedKey, 'Products are not sorted correctly!');
                expect(actual[actualKey]).to.equal(expected[expectedKey], 'Products are not sorted correctly!');
            }
        });
    });

    describe('occupiedCapacity', function () {
        it('Should return correct current occupied capacity for Foods', function () {
            let moq = new Warehouse(11);
            expect(moq.occupiedCapacity()).to.equal(0, 'Does not return correct value when warehouse is empty!');
            moq.addProduct('Food', 'Soup', 2);
            moq.addProduct('Food', 'Musaka', 3);
            moq.addProduct('Food', 'Kyufte', 1);
            moq.addProduct('Food', 'Musaka', 2);
            moq.addProduct('Food', 'Ayran', 3);
            expect(moq.occupiedCapacity()).to.equal(11, 'Does not return correct value when drink is added!');
        });

        it('Should return correct current occupied capacity for Drinks', function () {
            let moq = new Warehouse(20);
            expect(moq.occupiedCapacity()).to.equal(0, 'Does not return correct value when warehouse is empty!');
            moq.addProduct('Drink', 'Fanta', 1);
            moq.addProduct('Drink', 'Coffee', 3);
            moq.addProduct('Drink', 'Tea', 1);
            moq.addProduct('Drink', 'Bulyon', 2);
            moq.addProduct('Drink', 'Tea', 8);
            expect(moq.occupiedCapacity()).to.equal(15, 'Does not return correct value when drink is added!');
        });

        it('Should return correct current occupied capacity for mixed', function () {
            let moq = new Warehouse(200);
            expect(moq.occupiedCapacity()).to.equal(0, 'Does not return correct value when warehouse is empty!');
            moq.addProduct('Drink', 'Tea', 1);
            moq.addProduct('Food', 'Soup', 2);
            moq.addProduct('Food', 'Musaka', 3);
            moq.addProduct('Drink', 'Bulyon', 2);
            moq.addProduct('Drink', 'Tea', 8);
            moq.addProduct('Food', 'Musaka', 3);
            expect(moq.occupiedCapacity()).to.equal(19, 'Does not return correct value when drink is added!');
        });
    });

    describe('revision', function () {
        it('Should return correct string when warehouse is empty', function () {
            let moq = new Warehouse(10);
            expect(moq.revision()).to.equal('The warehouse is empty', 'Incorrect result when warehouse is empty!');
        });

        it('Should return correct string', function () {
            let moq = new Warehouse(10);
            moq.addProduct('Food', 'Soup', 1);
            moq.addProduct('Food', 'Musaka', 1);
            moq.addProduct('Drink', 'Cola', 2);
            let expected = ['Product type - [Food]',
                '- Soup 1',
                '- Musaka 1',
                'Product type - [Drink]',
                '- Cola 2'].join('\n');
            expect(moq.revision()).to.equal(expected, 'Wrong string returned!');
        });
    });

    describe('scrapeAProduct', function () {
        it('Should throw when product does not exist', function () {
            let moq = new Warehouse(10);
            moq.addProduct('Food', 'Musaka', 2);
            expect(() => moq.scrapeAProduct('Supa', 1)).to.throw('Supa do not exists', 'Should throw when products does not exist!');
        });

        it('Should set product quantity to 0 when given quantity is bigger', function () {
            let moq = new Warehouse(10);
            moq.addProduct('Food', 'Musaka', 2);
            let expected = moq.scrapeAProduct('Musaka', 5);
            expect(expected['Musaka']).to.equal(0);
        })

        it('Should reduce product quantity by given number', function () {
            let moq = new Warehouse(10);
            moq.addProduct('Food', 'Musaka', 5);
            let expected = moq.scrapeAProduct('Musaka', 2);
            expect(expected['Musaka']).to.equal(3);
        });
    });
});