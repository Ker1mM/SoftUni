function ArrayWithDelimeter(input){
    let result = input.slice(0, input.length-1).join(input[input.length-1]);
    console.log(result);
}