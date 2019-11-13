function attachEventsListeners() {
    document.getElementById('convert').addEventListener('click', function () {
        let inputType = document.getElementById('inputUnits').selectedIndex;
        let outputType = document.getElementById('outputUnits').selectedIndex;
        let value = document.getElementById('inputDistance').value;
        let result = convert(inputType, outputType, value);
        document.getElementById('outputDistance').value = result;
    })

    function convert(from, to, value) {
        let valueInMeters = convertFrom(from, value);
        let result = convertTo(to, valueInMeters);

        return result;
    }

    function convertFrom(type, value) {
        let result = 0;
        switch (type) {
            case 0:
                result = value * 1000;
                break;
            case 1:
                result = value;
                break;
            case 2:
                result = value * 0.01;
                break;
            case 3:
                result = value * 0.001;
                break;
            case 4:
                result = value * 1609.34;
                break;
            case 5:
                result = value * 0.9144;
                break;
            case 6:
                result = value * 0.3048;
                break;
            case 7:
                result = value * 0.0254;
                break;
            default:
                break;
        }

        return result;
    }

    function convertTo(type, value) {
        let result = 0;
        switch (type) {
            case 0:
                result = value / 1000;
                break;
            case 1:
                result = value;
                break;
            case 2:
                result = value / 0.01;
                break;
            case 3:
                result = value / 0.001;
                break;
            case 4:
                result = value / 1609.34;
                break;
            case 5:
                result = value / 0.9144;
                break;
            case 6:
                result = value / 0.3048;
                break;
            case 7:
                result = value / 0.0254;
                break;
            default:
                break;
        }

        return result;
    }
}