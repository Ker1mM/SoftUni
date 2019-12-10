import { getVenue } from "./templates.js";

export function displayVenues(venues) {
    let venueTable = document.getElementById('venue-info');
    venueTable.innerHTML = '';
    for (const venue of venues) {
        venueTable.appendChild(getVenue(venue));
    }
}