function getArticleGenerator(articles) {
    let content = document.getElementById('content');
    let nextIndex = 0;

    let nextArticle = function () {
        if (nextIndex < articles.length) {
            let text = document.createElement('article');
            text.innerHTML = articles[nextIndex++];
            content.appendChild(text);
        }
    }

    return nextArticle;
}
