function findGCD(num1, num2) {
    function GCD(num1, num2) {
        if (num1 == 0) {
            return num2;
        }
        return GCD(num2 % num1, num1);
    }

    console.log(GCD(num1, num2))
}