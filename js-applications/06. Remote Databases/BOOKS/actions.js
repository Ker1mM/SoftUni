import { fetchOperations } from "../src/fetchData.js";
import { elements } from "./domElements.js";

const username = 'guest';
const password = 'guest';
const appId = 'kid_SJ8WUOz3S';
let currentbookId = undefined;

const urlTemplates = {
    books: () => `https://baas.kinvey.com/appdata/${appId}/books`,
    book: (id) => `https://baas.kinvey.com/appdata/${appId}/books/` + id,
}

export const actions = {
    loadBooks: async () => {
        try {
            let books = await fetchOperations.get(urlTemplates.books(), username, password);
            let table = elements.getBooksTable(books);
            html.booksTable().innerHTML = '';
            html.booksTable().appendChild(table);
        }
        catch (er) {
            window.alert(`Error has occured: ${er.message}`);
        }
    },
    submit: async () => {
        try {
            let newBook = elements.getBookInfo();
            if (currentbookId) {
                await fetchOperations.put(urlTemplates.book(currentbookId), username, password, newBook);
                currentbookId = undefined;
            } else {
                await fetchOperations.post(urlTemplates.books(), username, password, newBook);
            }
            html.bookForm().reset();
        }
        catch (er) {
            window.alert(`Error has occured: ${er.message}`);
        }
        reload();
    },
    edit: async (el) => {
        try {
            let bookId = el.parentNode.getAttribute('book-id');
            let book = await fetchOperations.get(urlTemplates.book(bookId),
                username,
                password);
            elements.setBookInfo(book);
            currentbookId = bookId;
        }
        catch (er) {
            window.alert(`Error has occured: ${er.message}`);
        }
        reload();
    },
    delete: async (el) => {
        try {
            let bookId = el.parentNode.getAttribute('book-id');
            await fetchOperations.delete(urlTemplates.book(bookId),
                username,
                password
            )
        }
        catch (er) {
            window.alert(`Error has occured: ${er.message}`);
        }
        reload();

    },
};

var reload = () => actions.loadBooks();

const html = {
    booksTable: () => document.getElementById('booksTable'),
    bookForm: () => document.getElementById('book-form'),
}


