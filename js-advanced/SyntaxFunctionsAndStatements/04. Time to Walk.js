function timeToWalk(stepsCount, stepLength, speed) {
    Number.prototype.pad = function (size) {
        var s = String(this);
        while (s.length < (size || 2)) { s = "0" + s; }
        return s;
    }

    let distance = stepsCount * stepLength;
    let totalTime = parseInt(distance / 500) * 60;
    let speedMS = ((speed * 1000) / 3600);

    totalTime += Math.round(distance / speedMS);

    let seconds = parseInt(totalTime % 60);
    let minutes = parseInt((totalTime / 60) % 60, 10);
    let hours = parseInt(totalTime / 3600, 10);

    console.log(hours.pad(2) + ':' + minutes.pad(2) + ':' + seconds.pad(2));
}