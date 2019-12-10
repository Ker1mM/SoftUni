import { actions } from "./actions.js"

function attachEvents() {
    document.addEventListener('click', handleAction);
}

function handleAction(e) {
    e.preventDefault();
    if (typeof actions[e.target.id] === 'function') {
        actions[e.target.id](e.target);
    }
}

attachEvents();