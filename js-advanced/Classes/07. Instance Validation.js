class CheckingAccount {
    constructor(clientId, email, firstName, lastName) {
        if (RegExp('(^[0-9]{6}$)').test(clientId)) {
            this.clientId = clientId;
        } else {
            throw TypeError('Client ID must be a 6-digit number');
        }

        if (RegExp('(^[a-zA-Z0-9]+@[a-zA-Z.]+$)').test(email)) {
            this.email = email;
        } else {
            throw TypeError('Invalid e-mail');
        }

        if (firstName.length < 3 || firstName.length > 20) {
            throw TypeError('First name must be between 3 and 20 characters long');
        }

        if (lastName.length < 3 || lastName.length > 20) {
            throw TypeError('Last name must be between 3 and 20 characters long');
        }

        let nameRegex = RegExp('(^[a-zA-Z]{3,20}$)');
        if (nameRegex.test(firstName)) {
            this.firstName = firstName;
        } else {
            throw TypeError('First name must contain only Latin characters');
        }

        if (nameRegex.test(lastName)) {
            this.lastName = lastName;
        } else {
            throw TypeError('Last name must contain only Latin characters');
        }
    }
}



