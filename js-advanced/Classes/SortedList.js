class List {

    constructor() {
        this.elements = [];
        this.size = 0;
    }

    add(element) {
        this.elements.push(element);
        this.elements.sort(function (a, b) {
            return a - b;
        })
        this.size++;
    }

    remove(index) {
        if (index >= 0 && index < this.elements.length) {
            this.elements.splice(index, 1);
            this.size--;
        }
    }

    get(index) {
        if (index >= 0 && index < this.elements.length) {
            return this.elements[index];
        }
    }
}