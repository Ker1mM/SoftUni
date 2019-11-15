function calorieObject(objects) {
    let result = [];
    for (let i = 0; i < objects.length - 1; i += 2) {
        result.push(objects[i] + ': ' + objects[i + 1]);
    }
    console.log('{ ' + result.join(', ') + ' }');
}