const SkiResort = require('./solution.js');
let chai = require('chai');
let expect = chai.expect;



describe('SkiResort', function () {

    describe('constructor', function () {
        it('Correct instantiation with valid parameters', function () {
            expect(new SkiResort('Valid') instanceof SkiResort).to.be.equal(true);
            let moq = new SkiResort('Moq');
            expect(moq.name).to.be.equal('Moq');
            expect(moq.voters).to.be.equal(0);
            expect(moq.hotels instanceof Array).to.be.equal(true);
            expect(moq.hotels.length).to.be.equal(0);
        });
    });

    describe('Properties', function () {

        it('Should have all properties', function () {
            let moq = new SkiResort('test');
            expect(moq.hasOwnProperty('name')).to.be.equal(true);
            expect(moq.hasOwnProperty('voters')).to.be.equal(true);
            expect(moq.hasOwnProperty('hotels')).to.be.equal(true);
            expect(Object.getPrototypeOf(moq).hasOwnProperty('build')).to.be.equal(true);
            expect(Object.getPrototypeOf(moq).hasOwnProperty('book')).to.be.equal(true);
            expect(Object.getPrototypeOf(moq).hasOwnProperty('leave')).to.be.equal(true);
            expect(Object.getPrototypeOf(moq).hasOwnProperty('averageGrade')).to.be.equal(true);
            expect(Object.getPrototypeOf(moq).hasOwnProperty('bestHotel')).to.be.equal(true);
        });

    });

    describe('bestHotel', function () {
        it('Should return proper message if no votes', function () {
            let moq = new SkiResort('Bansko');
            expect(moq.bestHotel).to.be.equal('No votes yet');
        });

        it('Should return best hotel', function () {
            let moq = new SkiResort('Bansko');
            moq.voters = 5;
            moq.build('Rila', 4);
            moq.build('Pirin', 6);
            moq.hotels[0].points = 9;
            moq.hotels[1].points = 7;

            expect(moq.bestHotel).to.equal('Best hotel is Rila with grade 9. Available beds: 4')
        });
    });

    describe('build', function () {
        it('Should throw error with invalid input', function () {
            let moq = new SkiResort('Test');
            expect(() => moq.build('', 0)).to.throw('Invalid input');
            expect(() => moq.build('St', 0)).to.throw('Invalid input');
            expect(() => moq.build('', 2)).to.throw('Invalid input');
        })

        it('Should add the motel and return correct result', function () {
            let moq = new SkiResort('Test');
            expect(moq.build('Rila', 5)).to.be.equal('Successfully built new hotel - Rila');
            expect(moq.hotels.length).to.equal(1);
            expect(moq.hotels[0]).to.be.deep.equal({ name: 'Rila', beds: 5, points: 0 });
        });
    });

    describe('book', function () {

        it('Should throw error with invalid input', function () {
            let moq = new SkiResort('Test');
            expect(() => moq.book('', 0)).to.throw('Invalid input');
            expect(() => moq.book('St', 0)).to.throw('Invalid input');
            expect(() => moq.book('', 2)).to.throw('Invalid input');
        });

        it('If there is no hotel with that name should throw error', function () {
            let moq = new SkiResort('Test');
            moq.build('Pirin', 5);
            expect(() => moq.book('No', 2)).to.throw('There is no such hotel');
        });

        it('Should throw if not enough beds', function () {
            let moq = new SkiResort('Test');
            moq.build('Rila', 4);
            expect(() => moq.book('Rila', 5)).to.throw('There is no free space');
        });

        it('Should lower bed count with valid input', function () {
            let moq = new SkiResort('Test');
            moq.build('Rila', 4);
            expect(moq.book('Rila', 2)).to.be.equal('Successfully booked');
            expect(moq.hotels[0].beds).to.equal(2);
        });
    });

    describe('leave', function () {
        it('Should throw error with invalid input', function () {
            let moq = new SkiResort('Test');
            expect(() => moq.leave('', 0)).to.throw('Invalid input');
            expect(() => moq.leave('St', 0)).to.throw('Invalid input');
            expect(() => moq.leave('', 2)).to.throw('Invalid input');
        });

        it('If there is no hotel with that name should throw error', function () {
            let moq = new SkiResort('Test');
            expect(() => moq.leave('No', 2)).to.throw('There is no such hotel');
        });

        it('Should return correct output with valid parameters', function () {
            let moq = new SkiResort('Bansko');
            moq.build('Rila', 10);
            moq.book('Rila', 4);
            expect(moq.leave('Rila', 2, 3)).to.be.equal('2 people left Rila hotel');
            expect(moq.hotels[0].beds).to.equal(8);
            expect(moq.hotels[0].points).to.equal(6);
            expect(moq.voters).to.equal(2);
        });
    });

    describe('averageGrade', function () {
        it('Should return correct output with no voters', function () {
            let moq = new SkiResort('Bansko');
            expect(moq.averageGrade()).to.equal('No votes yet');
        });


        it('Should return correct output with valid parameters', function () {
            let moq = new SkiResort('Bansko');
            moq.voters = 6;
            moq.build('Rila', 5);
            moq.build('Pirin', 5);
            moq.hotels[0].points = 5;
            moq.hotels[1].points = 5;

            expect(moq.averageGrade()).to.equal(`Average grade: ${(10 / 6).toFixed(2)}`);
        });

    });
});
