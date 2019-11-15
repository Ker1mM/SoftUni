function ticketSystem(input, order) {
    class Ticket {
        constructor(destination, price, status) {
            this.destination = destination;
            this.price = Number(price);
            this.status = status;
        }
    }

    let tickets = [];
    for (let ticketInfo of input) {
        let tokens = ticketInfo.split('|');
        let currentTicket = new Ticket(tokens[0], tokens[1], tokens[2]);
        tickets.push(currentTicket);
    }

    tickets.sort(function (a, b) {
        if (a[order] < b[order]) {
            return -1;
        }

        if (a[order] > b[order]) {
            return 1;
        }

        return 0;
    });

    return tickets;
}