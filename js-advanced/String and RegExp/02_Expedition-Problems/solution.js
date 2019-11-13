function solve() {
    let keyword = document.getElementById('string').value;
    let input = document.getElementById('text').value;
    let coordinates = getCoordinates(input);
    let message = getMessage(keyword, input);
    
    let northCoordinateElement = document.createElement('p');
    northCoordinateElement.innerHTML = `${coordinates.north[2]}.${coordinates.north[3]} N`
    let eastCoordinateElement = document.createElement('p');
    eastCoordinateElement.innerHTML = `${coordinates.east[2]}.${coordinates.east[3]} E`
    let messageElement = document.createElement('p');
    messageElement.innerHTML = `Message: ${message}`

    document.getElementById('result').appendChild(northCoordinateElement);
    document.getElementById('result').appendChild(eastCoordinateElement);
    document.getElementById('result').appendChild(messageElement);

    function getMessage(keyword, text){
        let message = text.match(`${keyword}(.+)${keyword}`)[1];
        return message;
    }

    function getCoordinates(text) {
        let coordinates = {};

        let pattern = /(east|north).*?([0-9]{2})[^,]*?,[^,]*?([0-9]{6})/gim;

        let result;
        while((result = pattern.exec(text)) !== null){
            if( result[1].toLowerCase() === "east"){
                coordinates.east = result;
            }else{
                coordinates.north = result;
            }
        }

        return coordinates;
    }
}
