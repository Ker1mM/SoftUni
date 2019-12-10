import { fetchOperations } from "../src/fetchData.js";
import { displayVenues } from "./elements.js";
import { getPurchase } from "./templates.js";

const username = 'guest';
const password = 'pass';
const appId = 'kid_BJ_Ke8hZg';
let selectedMovie = undefined;

const urlTemplates = {
    getVenues: (input) => `https://baas.kinvey.com/rpc/${appId}/custom/calendar?query=` + input,
    purchase: (id, qty) => `https://baas.kinvey.com/rpc/kid_BJ_Ke8hZg/custom/purchase?venue=${id}&qty=${qty}`,
    venue: (id) => `https://baas.kinvey.com/appdata/${appId}/venues/` + id,
}

export const actions = {
    getVenues: async () => {

        try {
            let venueIds = await fetchOperations.post(urlTemplates.getVenues(html.venueDates()), username, password);
            let venues = [];
            for (let i = 0; i < venueIds.length; i++) {
                venues.push(await fetchOperations.get(urlTemplates.venue(venueIds[i]), username, password));
            }
            displayVenues(venues);
            selectedMovie = undefined;
        }
        catch (er) {
            window.alert('Error: ' + er.message);
        }
    },
    more: (e) => {
        let id = e.parentNode.parentNode.id;
        let displayEl = document.getElementById(id).getElementsByClassName('venue-details')[0];
        if (displayEl.style.display === 'none') {
            displayEl.style.display = 'block';
            e.value = 'Hide';
        } else {
            displayEl.style.display = 'none';
            e.value = 'More info';
        }
    },
    buy: (e) => {
        let id = e.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.id;
        selectedMovie = id;
        let displayEl = document.getElementById(id);
        let name = displayEl.getElementsByClassName('venue-name')[0].childNodes[1].nodeValue;
        let quantity = displayEl.getElementsByClassName('quantity')[0].value;
        let price = displayEl.getElementsByClassName('venue-price')[0].innerHTML.split(' ')[0];
        document.getElementById('venue-info').innerHTML = getPurchase(name, quantity, price);
    },
    confirm: async (e) => {
        let qty = document.getElementsByClassName('purchase-info')[0].childNodes[1].innerHTML.split(' x ')[0];
        let respo = await fetchOperations.post(urlTemplates.purchase(selectedMovie, qty), username, password);

        document.getElementById('venue-info').innerHTML = respo.html;
    }
};

const html = {
    venueDates: () => {
        let dates = document.getElementById('venueDate').value;
        if (!dates) {
            throw new Error('Enter date range!');
        }

        return dates;
    },

}