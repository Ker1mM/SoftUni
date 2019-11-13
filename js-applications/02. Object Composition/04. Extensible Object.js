function solve() {
    let myObj = {
        extend: function (obj) {
            for (let key in obj) {
                console.log(typeof obj[key]);
                if (typeof obj[key] === 'function') {
                    let proto = Object.getPrototypeOf(myObj);
                    proto[key] = obj[key];
                } else {
                    this[key] = obj[key];
                }
            }
        }
    }

    return myObj;
}