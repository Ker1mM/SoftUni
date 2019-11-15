function FunctionalSum(num) {
    let sum = num;

    function result(num) {
        sum += num;
        return result;
    }

    result.toString = () => sum.toString();
    return result
};