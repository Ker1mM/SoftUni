function render() {
    let template = $('#monkey-template').html();
    let templateScript = Handlebars.compile(template);

    $('.monkeys').html(templateScript(monkeys));

    $(document).on('click', toggle);
}

function toggle(e) {
    if (e.target.tagName === 'BUTTON') {
        let button = e.target;
        let p = $('#' + e.target.dataset.id);
        if (p.css('display') === 'none') {
            button.innerText = 'HIDE';
            p.css('display', 'block');
        } else {
            button.innerText = 'INFO';
            p.css('display', 'none');
        }
    }
}

render();