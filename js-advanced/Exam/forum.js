class Forum {
    constructor() {
        this._users = [];
        this._questions = [];
        this._id = 1;
    }

    register(username, password, repeatPassword, email) {
        if (username === '' || password === '' || repeatPassword === '' || email === '') {
            throw new Error('Input can not be empty');
        }

        if (password !== repeatPassword) {
            throw new Error('Passwords do not match');
        }

        let user = this._users.find(x => x.username === username || x.email === email);
        if (user) {
            throw new Error('This user already exists!');
        }

        let newUser = { username, password, email, logged: false };
        this._users.push(newUser);

        return `${username} with ${email} was registered successfully!`;
    }

    login(username, password) {

        if (!this._users.find(x => x.username === username)) {
            throw new Error('There is no such user');
        }

        let user = this._users.find(x => x.username === username && x.password === password);
        if (user) {
            user.logged = true;
            return 'Hello! You have logged in successfully';
        }
    }

    logout(username, password) {
        if (!this._users.find(x => x.username === username)) {
            throw new Error('There is no such user');
        }

        let user = this._users.find(x => x.username === username && x.password === password);
        if (user) {
            user.logged = false;
            return 'You have logged out successfully';
        }
    }

    postQuestion(username, question) {
        let user = this._users.find(x => x.username === username);
        if (!user || user.logged === false) {
            throw new Error('You should be logged in to post questions');
        }

        if (question === '') {
            throw new Error('Invalid question');
        }

        let newQuestion = { question, username, id: this._id, answers: [] };
        this._questions.push(newQuestion);
        this._id++;
        return 'Your question has been posted successfully';
    }

    postAnswer(username, questionId, answer) {
        let user = this._users.find(x => x.username === username);
        if (!user || user.logged === false) {
            throw new Error('You should be logged in to post answers');
        }

        if (answer === '') {
            throw new Error('Invalid answer');
        }

        let question = this._questions.find(x => x.id === questionId);
        if (!question) {
            throw new Error('There is no such question');
        }

        let ann = { answer, username };
        question.answers.push(ann);
        return 'Your answer has been posted successfully';
    }

    showQuestions() {
        let result = [];
        for (let q of this._questions) {
            result.push(`Question ${q.id} by ${q.username}: ${q.question}`);
            for (let a of q.answers) {
                result.push(`---${a.username}: ${a.answer}`);
            }
        }

        return result.join('\n');
    }
}

let forum = new Forum();

forum.register('Jonny', '12345', '12345', 'jonny@abv.bg');
forum.register('Peter', '123ab7', '123ab7', 'peter@gmail@.com');
forum.login('Jonny', '12345');
forum.login('Peter', '123ab7');

forum.postQuestion('Jonny', "Do I need glasses for skiing?");
forum.postAnswer('Peter', 1, "Yes, I have rented one last year.");
forum.postAnswer('Jonny', 1, "What was your budget");
forum.postAnswer('Peter', 1, "$50");
forum.postAnswer('Jonny', 1, "Thank you :)");

console.log(forum.showQuestions());

