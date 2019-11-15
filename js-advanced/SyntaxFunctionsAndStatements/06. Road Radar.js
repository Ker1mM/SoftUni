function roadRadar(input) {
    const motorwayLimit = 130;
    const interstateLimit = 90;
    const cityLimit = 50;
    const residentialLimit = 20;

    var speed = input[0];
    var location = input[1];

    var limit = 0;
    switch (location) {
        case 'city':
            limit = cityLimit;
            break;

        case 'motorway':
            limit = motorwayLimit;
            break;

        case 'residential':
            limit = residentialLimit;
            break;

        case 'interstate':
            limit = interstateLimit;
            break;

        default:
            break;
    }

    var aboveSpeedLimit = speed - limit;

    if (aboveSpeedLimit > 40) {
        console.log('reckless driving');
    } else if (aboveSpeedLimit > 20) {
        console.log('excessive speeding');
    } else if (aboveSpeedLimit > 0) {
        console.log('speeding');
    }
}