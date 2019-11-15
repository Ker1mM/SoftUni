function solve(input) {

    function sort(a, b) {
        if (a.length > b.length) {
            return 1;
        }

        if (a.length < b.length) {
            return -1;
        }

        if (a.toLowerCase() > b.toLowerCase()) {
            return 1;
        }

        if (a.toLowerCase() < b.toLowerCase()) {
            return -1;
        }

        return 0;
    }

    let result = input.sort(sort);
    console.log(result.join('\n'));
}