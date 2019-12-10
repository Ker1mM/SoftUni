

export function getVenue(venue) {
    let div = document.createElement('div');
    div.setAttribute('class', 'venue');
    div.setAttribute('id', venue._id);
    let text = `<span class="venue-name"><input class="info" id="more" type="button" value="More info">${venue.name}</span>` +
        `<div class="venue-details" style="display: none;">` +
        '<table><tr><th>Ticket Price</th><th>Quantity</th><th></th></tr><tr>' +
        `<td class="venue-price">${venue.price} lv</td>` +
        `<td><select class="quantity">` +
        `<option value="1">1</option>` +
        `<option value="2">2</option>` +
        `<option value="3">3</option>` +
        `<option value="4">4</option>` +
        `<option value="5">5</option>` +
        `</select></td>` +
        `<td><input id="buy" class="purchase" type="button" value="Purchase"></td>` +
        `</tr></table>` +
        `<span class="head">Venue description:</span>` +
        `<p class="description">${venue.description}</p>` +
        `<p class="description">Starting time: ${venue.startingHour}</p>` +
        `</div>`;

    div.innerHTML = text;
    return div;
}

export function getPurchase(name, qty, price) {
    let text = `<span class="head">Confirm purchase</span>` +
        `<div class="purchase-info">` +
        `<span>${name}</span>` +
        `<span>${qty} x ${price}</span>` +
        `<span>Total: ${Number(qty) * Number(price)} lv</span>` +
        `<input id="confirm" type="button" value="Confirm"></div>`;

    return text;
}