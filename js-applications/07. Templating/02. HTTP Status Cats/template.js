(() => {
    renderCatTemplate();

    function renderCatTemplate() {
        let tmplt = document.getElementById('cat-template').innerText;
        let tmpltScript = Handlebars.compile(tmplt);

        document.getElementById('allCats').innerHTML = tmpltScript(cats);
        document.addEventListener('click', action);
    }

    function action(e) {
        if (e.target.className === 'showBtn') {
            let panel = e.target.parentNode.getElementsByClassName('status')[0];
            if (panel.style.display === 'none') {
                panel.style.display = 'block';
                e.target.innerHTML = 'Hide status code';
            } else {
                e.target.innerHTML = 'Show status code';
                panel.style.display = 'none';
            }
        }
    }



})();
