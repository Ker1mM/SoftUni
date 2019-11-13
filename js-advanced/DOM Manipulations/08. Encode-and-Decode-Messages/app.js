function encodeAndDecodeMessages() {
    document.getElementsByTagName('button')[0].addEventListener('click', function () {
        let text = document.getElementsByTagName('textarea')[0].value;
        let encodedText = encode(text);
        document.getElementsByTagName('textarea')[1].value = encodedText;
        document.getElementsByTagName('textarea')[0].value = '';
    });

    document.getElementsByTagName('button')[1].addEventListener('click', function () {
        let text = document.getElementsByTagName('textarea')[1].value;
        let decoded = decode(text);
        document.getElementsByTagName('textarea')[1].value = decoded;
    });

    function encode(text) {
        let result = '';

        for (let i = 0; i < text.length; i++) {
            result += String.fromCharCode(text.charCodeAt(i) + 1);
        }

        return result;
    }

    function decode(text) {
        let result = '';

        for (let i = 0; i < text.length; i++) {
            result += String.fromCharCode(text.charCodeAt(i) - 1);
        }

        return result;
    }
}