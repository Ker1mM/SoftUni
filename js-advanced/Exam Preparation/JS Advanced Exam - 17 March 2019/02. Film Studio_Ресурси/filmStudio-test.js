const FilmStudio = require('./filmStudio.js');
var chai = require('chai')
var expect = chai.expect;

describe('beforeEachTest', function () {
    let filmStudio;
    beforeEach(function () {
        filmStudio = new FilmStudio('test');
    });

    it('Should create instance with single parameter string', function () {
        expect(filmStudio instanceof FilmStudio).to.be.equal(true);
    });
});

describe('filmStudio', function () {

    it('Has all properties', function () {
        let moq = new FilmStudio('Test');
        expect(moq.hasOwnProperty('name')).to.be.equal(true, 'Name missing!');
        expect(moq.hasOwnProperty('films')).to.be.equal(true, 'Films missing!');
        expect(Object.getPrototypeOf(moq).hasOwnProperty('makeMovie')).to.be.equal(true, 'MakeMove missing!');
        expect(Object.getPrototypeOf(moq).hasOwnProperty('casting')).to.be.equal(true, 'Casting missing!');
        expect(Object.getPrototypeOf(moq).hasOwnProperty('lookForProducer')).to.be.equal(true, 'LookForProducer missing!');
    });

    describe('Constructor', function () {
        it('Should create instance correectly with valid parameter', function () {
            let moq = new FilmStudio('Test');
            expect(moq.name).to.be.equal('Test');
            expect(moq.films.length).to.be.equal(0);
            expect(moq.films instanceof Array).to.be.equal(true);
        });
    });

    describe('makeMovie', function () {
        it('Should throw error if parameter count is different than 2', function () {
            let moq = new FilmStudio('test');
            expect(() => moq.makeMovie('a', ['a', 'b'], 2)).to.throw('Invalid arguments count', 'More than 2!');
            expect(() => moq.makeMovie('a')).to.throw('Invalid arguments count', 'Less than 2!');
            expect(() => moq.makeMovie()).to.throw('Invalid arguments count', '0!');
        });

        it('Should throw error if first parameter is not string', function () {
            let moq = new FilmStudio('test');
            expect(() => moq.makeMovie(1, [1, 2])).to.throw('Invalid arguments');
            expect(() => moq.makeMovie(['s', 't'], [1, 2])).to.throw('Invalid arguments');
            expect(() => moq.makeMovie({ string: 'a' }, [1, 2])).to.throw('Invalid arguments');
        });

        it('Should throw error if second parameter is not array', function () {
            let moq = new FilmStudio('test');
            expect(() => moq.makeMovie('Test', 'Test 2')).to.throw('Invalid arguments');
            expect(() => moq.makeMovie('Test', 2)).to.throw('Invalid arguments');
            expect(() => moq.makeMovie('Test', { array: [1, 2] })).to.throw('Invalid arguments');
        });

        it('Should create film object with correct parameters', function () {
            let moq = new FilmStudio('Universal');
            let filmObject = moq.makeMovie('Bad Boys', ['Bad guy']);
            let expected = { filmName: "Bad Boys", filmRoles: [{ actor: false, role: 'Bad guy' }] };
            expect(filmObject).to.deep.equal(expected);
        });

        it('Should add number to name if already exists', function () {
            let moq = new FilmStudio('Universal');
            moq.makeMovie('Bad Boys', ['Bad guy']);
            moq.makeMovie('Bad Boys', ['Bad guy']);
            let filmObject = moq.makeMovie('Bad Boys', ['Good guy']);
            let expected = { filmName: "Bad Boys 3", filmRoles: [{ actor: false, role: 'Good guy' }] };
            expect(filmObject).to.deep.equal(expected);
            expect(moq.films.length).to.equal(3);
        });
    });

    describe('casting', function () {

        it('Should return correct output if there are no films', function () {
            let moq = new FilmStudio('Boyana');
            expect(moq.casting('Johnny', 'Cool guy')).to.equal(`There are no films yet in ${moq.name}.`)
        });

        it('Should return correct output if there are no roles for the actor', function () {
            let moq = new FilmStudio('Boyana');
            moq.makeMovie('Cobra', ['Snake']);
            expect(moq.casting('Tony', 'Carpet')).to.be.equal('Tony, we cannot find a Carpet role...');
        });


        it('Should return correct output if there is available role', function () {
            let moq = new FilmStudio('Boyana');
            moq.makeMovie('Cobra', ['Snake']);
            expect(moq.casting('Tony', 'Snake')).to.be.equal('You got the job! Mr. Tony you are next Snake in the Cobra. Congratz!');
        });

    });
    describe('lookForProducer', function () {
        it('Should throw error if such movie does not exist', function () {
            let moq = new FilmStudio('Boyana');
            expect(() => moq.lookForProducer('Solo')).to.throw('Solo do not exist yet, but we need the money...');
        });

        it('Should return correct output if movie exists', function () {
            let moq = new FilmStudio('Boyana');
            moq.makeMovie('Pesho in the forest', ['Pesho']);
            moq.casting('Gosho', 'Pesho');
            let actual = moq.lookForProducer('Pesho in the forest');
            let expected = 'Film name: Pesho in the forest\n' +
                'Cast:\n' +
                'Gosho as Pesho\n';
            expect(actual).to.be.equal(expected);
        });
    });
});