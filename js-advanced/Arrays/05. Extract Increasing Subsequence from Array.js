function IncreasingSubsequence(input) {
    let biggest = input[0];
    let result = input.reduce((total, current) => {
        if(current >= biggest){
            biggest = current;
            total.push(current)
        };

        return total;
    }, [])

    console.log(result.join('\n'));
}