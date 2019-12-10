export const elements = {
    getBooksTable: (books) => getBooksTable(books),
    getBookInfo: () => getBookInfo(),
    setBookInfo: (book) => setBookInfo(book),
}



function getBooksTable(books) {
    let fragment = document.createDocumentFragment();
    for (let book of books) {
        if (Object.values(book).length < 7) {
            continue;
        }
        let trElement = document.createElement('tr');
        let text = `<td>${book.title}</td>` +
            `<td>${book.author}</td>` +
            `<td>${book.isbn}</td>` +
            `<td>${book.tags.join(', ')}</td>` +
            `<td><div book-id="${book._id}"><button>Edit</button>` +
            `<button>Delete</button></div></td>`;
        trElement.innerHTML = text;
        fragment.appendChild(trElement);
    }

    return fragment;
}

function getBookInfo() {
    let title = document.getElementById('title').value;
    let author = document.getElementById('author').value;
    let isbn = document.getElementById('isbn').value;
    let tags = document.getElementById('tags').value.split(/\s*,\s*/).filter(x => x);

    return { title, author, isbn, tags };
}

function setBookInfo(book) {
    document.getElementById('title').value = book.title;
    document.getElementById('author').value = book.author;
    document.getElementById('isbn').value = book.isbn;
    document.getElementById('tags').value = book.tags.join(', ');
}