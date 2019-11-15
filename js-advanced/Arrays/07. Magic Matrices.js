function solve(input) {
    let sum = input[0].reduce((total, current) => { return total + current });
    let result = true;

    for (let i = 0; i < input.length; i++) {
        let col = input.reduce((total, current) => {
            return total + current[i];
        }, 0);

        let row = input[i].reduce((total, current) => { return total + current });

        if( row !== sum || col !== sum){
            result = false;
            break;
        }
    }

    console.log(result);
}