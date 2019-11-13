class Hotel {

    constructor(name, capacity) {
        this.name = name;
        this.capacity = capacity;
        this.bookings = [];
        this.currentBookingNumber = 1;
        this._availableRoomTypes = this.rooms;
    }

    get rooms() {
        let single = Math.floor(this.capacity * 0.50);
        let double = Math.floor(this.capacity * 0.30);
        let maisonette = Math.floor(this.capacity * 0.20);

        return { single: single, double: double, maisonette: maisonette };
    }

    get roomsPricing() {
        let prices = { single: 50, double: 90, maisonette: 135 };
        return prices;
    }

    get servicesPricing() {
        let services = { food: 10, drink: 15, housekeeping: 25 };
        return services;
    }


    rentARoom(clientName, roomType, nights) {

        let output;
        if (this._availableRoomTypes[roomType] > 0) {
            let currentBooking = {};
            currentBooking.clientName = clientName;
            currentBooking.roomType = roomType;
            currentBooking.nights = nights;
            currentBooking.roomNumber = this.currentBookingNumber;
            this.bookings.push(currentBooking);

            this._availableRoomTypes[roomType]--;

            output = `Enjoy your time here Mr./Mrs. ${clientName}. Your booking is ${this.currentBookingNumber++}.`;
        } else {
            output = `No ${roomType} rooms available!`;
            for (let key of Object.keys(this._availableRoomTypes)) {
                if (this._availableRoomTypes[key] > 0) {
                    output += ` Available ${key} rooms: ${this._availableRoomTypes[key]}.`
                }
            }
        }

        return output;
    }

    roomService(bookingNumber, serviceType) {
        let currentBooking = this.bookings.find(x => x.roomNumber === bookingNumber);
        let output;
        if (currentBooking) {
            if (this.servicesPricing.hasOwnProperty(serviceType)) {

                if (currentBooking.hasOwnProperty('services')) {
                    currentBooking.services.push(serviceType);
                } else {
                    currentBooking.services = [serviceType];
                }

                output = `Mr./Mrs. ${currentBooking.clientName}, Your order for ${serviceType} service has been successful.`
            } else {
                output = `We do not offer ${serviceType} service.`;
            }
        } else {
            output = `The booking ${bookingNumber} is invalid.`;
        }

        return output;
    }

    checkOut(currentBookingNumber) {
        let booking = this.bookings.find(x => x.roomNumber === currentBookingNumber);
        let output;
        let totalPrice = 0;
        if (booking) {
            this._availableRoomTypes[booking.roomType]++;
            totalPrice += (booking.nights * this.roomsPricing[booking.roomType]);
            if (booking.hasOwnProperty('services')) {
                let totalServicesPrice = 0;
                for (let service of booking.services) {
                    totalServicesPrice += this.servicesPricing[service];
                }

                output = `We hope you enjoyed your time here, Mr./Mrs. ${booking.clientName}. The total amount of money you have to pay is ${totalPrice + totalServicesPrice} BGN. You have used additional room services, costing ${totalServicesPrice} BGN.`;
            } else {
                output = `We hope you enjoyed your time here, Mr./Mrs. ${booking.clientName}. The total amount of money you have to pay is ${totalPrice} BGN.`;
            }

            let index = this.bookings.indexOf(booking);
            this.bookings.splice(index, 1);
        } else {
            output = `The booking ${currentBookingNumber} is invalid.`;
        }

        return output;
    }

    report() {
        let result = [];
        result.push(`${this.name.toUpperCase()} DATABASE:`)
        result.push('--------------------');
        if (this.bookings.length > 0) {
            let first = true;
            for (let book of this.bookings) {
                if (!first) {
                    result.push('----------');
                }
                result.push(`bookingNumber - ${book.roomNumber}`);
                result.push(`clientName - ${book.clientName}`);
                result.push(`roomType - ${book.roomType}`)
                result.push(`nights - ${book.nights}`);
                if (book.hasOwnProperty('services')) {
                    result.push(`services: ${book.services.join(', ')}`);
                }
                first = false;
            }
        } else {
            result.push('There are currently no bookings.');
        }

        return result.join('\n');
    }
}

let hotel = new Hotel('HotUni', 10);

hotel.rentARoom('Peter', 'single', 4);
hotel.rentARoom('Robert', 'double', 4);
hotel.rentARoom('Geroge', 'maisonette', 6);

console.log(hotel.roomService(3, 'housekeeping'));
console.log(hotel.roomService(3, 'drink'));
console.log(hotel.roomService(2, 'room'));
console.log(hotel.checkOut(3));

console.log(hotel.report());


