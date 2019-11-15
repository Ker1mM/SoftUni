function SortArray(input, order) {
    var result = input;
    if (order === 'asc') {
        result = input.sort(function (a, b) { return a - b });
    } else if (order === 'desc') {
        result = input.sort(function (a, b) { return b - a });
    }

    return result;
}