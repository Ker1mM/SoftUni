function attachEvents() {
    html.submit().addEventListener('click', forecast)
}

async function forecast(e) {
    let forecastInfo;
    html.forecast().style.display = 'block';
    try {
        //debugger;
        forecastInfo = await fetchActions.getLocationInfo();
        var locationInfo = getLocationInfo(forecastInfo);
        var currentForecastInfo = await fetchActions.getCurrent(locationInfo);
        var upcomingForecastInfo = await fetchActions.getUpcoming(locationInfo);
        displayCurrent(getCurrentForecast(currentForecastInfo));
        displayUpcoming(getUpcomingForecast(upcomingForecastInfo));
    }
    catch (er) {
        displayError(er);
    }
}

const html = {
    submit: () => document.getElementById('submit'),
    location: () => document.getElementById('location'),
    forecast: () => document.getElementById('forecast'),
    current: () => document.getElementById('current'),
    upcoming: () => document.getElementById('upcoming'),
}

attachEvents();

////////
//
////////

function getCurrentForecast(info) {
    let frag = document.createDocumentFragment();
    frag.appendChild(getSpanWithClassAndText('condition symbol', weatherSymbol[info.forecast.condition]));

    var conditionSpan = getSpanWithClassAndText('condition');
    var temps = `${info.forecast.low}${weatherSymbol.Degrees}/${info.forecast.high}${weatherSymbol.Degrees}`
    conditionSpan.appendChild(getSpanWithClassAndText('forecast-data', info.name));
    conditionSpan.appendChild(getSpanWithClassAndText('forecast-data', temps));
    conditionSpan.appendChild(getSpanWithClassAndText('forecast-data', info.forecast.condition));

    frag.appendChild(conditionSpan);

    return frag;
}

function getUpcomingForecast(info) {
    let frag = document.createDocumentFragment();

    for (const day of info.forecast) {
        var upcomingSpan = getSpanWithClassAndText('upcoming');
        var temps = `${day.low}${weatherSymbol.Degrees}/${day.high}${weatherSymbol.Degrees}`;
        upcomingSpan.appendChild(getSpanWithClassAndText('symbol', weatherSymbol[day.condition]));
        upcomingSpan.appendChild(getSpanWithClassAndText('forecast-data', temps));
        upcomingSpan.appendChild(getSpanWithClassAndText('forecast-data', day.condition));
        frag.appendChild(upcomingSpan);
    }

    return frag;
}

function getSpanWithClassAndText(className, text = '') {
    let span = document.createElement('span');
    span.setAttribute('class', className);
    span.innerHTML = text;

    return span;
}

function getLocationInfo(info) {
    var forecastLocation = html.location().value;
    if (!forecastLocation) { throw new Error('Error'); }

    let locInfo = info.find(x => x.name === forecastLocation);
    if (!locInfo) {
        throw new Error('Error');
    }

    return locInfo;
}

////////
//
////////


function displayCurrent(child) {
    let forcastDiv = html.current().getElementsByClassName('forecasts');
    if (forcastDiv.length > 0) {
        forcastDiv[0].innerHTML = '';
        forcastDiv[0].appendChild(child);
    } else {
        forcastDiv = document.createElement('div');
        forcastDiv.setAttribute('class', 'forecasts');
        forcastDiv.appendChild(child);
        html.current().appendChild(forcastDiv);
    }
}

function displayUpcoming(child) {
    let forcastDiv = html.upcoming().getElementsByClassName('forecast-info');
    if (forcastDiv.length > 0) {
        forcastDiv[0].innerHTML = '';
        forcastDiv[0].appendChild(child);
    } else {
        forcastDiv = document.createElement('div');
        forcastDiv.setAttribute('class', 'forecast-info');
        forcastDiv.appendChild(child);
        html.upcoming().appendChild(forcastDiv);
    }
}

function displayError(er) {
    var errorSpan = document.createElement('span');
    errorSpan.innerHTML = er.message;
    displayCurrent(errorSpan);
    displayUpcoming(errorSpan.cloneNode(true));

}


////////
//
////////

const weatherSymbol = {
    'Sunny': '&#x2600',
    'Partly sunny': '&#x26C5',
    'Overcast': '&#x2601',
    'Rain': '&#x2614',
    'Degrees': '&#176'
}

const URLTemplates = {
    infoURL: () => 'https://judgetests.firebaseio.com/locations.json',
    currentURL: (code) => `https://judgetests.firebaseio.com/forecast/today/${code}.json`,
    upcomingURL: (code) => `https://judgetests.firebaseio.com/forecast/upcoming/${code}.json`
}

const fetchActions = {
    getLocationInfo: () => fetchData(undefined, URLTemplates.infoURL()),
    getCurrent: ({ code }) => fetchData(undefined, URLTemplates.currentURL(code)),
    getUpcoming: ({ code }) => fetchData(undefined, URLTemplates.upcomingURL(code))
}

////////
//my fetch function
///////
function handleError(x) {
    if (!x.ok) {
        throw new Error('Error');
    }
    return x;
}

function fetchData(errorHandler = handleError, URL) {
    return fetch(URL)
        .then(x => errorHandler(x))
        .then(x => x.json());
}