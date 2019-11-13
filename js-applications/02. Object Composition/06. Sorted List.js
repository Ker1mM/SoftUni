function solve() {
    let result = {
        elements: [],
        size: 0,

        add(element) {
            this.elements.push(element);
            this.sort();
            this.size++;
        },

        get(index) {
            if (index >= 0 && index < this.elements.length) {
                return this.elements[index];
            }
        },

        remove(index) {
            if (index >= 0 && index < this.elements.length) {
                this.elements.splice(index, 1);
                this.size--;
            }
        },

        sort() {
            this.elements.sort((a, b) => { return a - b });
        }
    }

    return result;
}