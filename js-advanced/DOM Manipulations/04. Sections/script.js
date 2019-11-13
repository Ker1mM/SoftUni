function create(words) {
   let content = document.getElementById('content');

   for (let section of words) {
      let newSection = document.createElement('div');
      let paragraph = document.createElement('p');
      paragraph.setAttribute('style', 'display: none')
      paragraph.textContent = section;

      newSection.addEventListener('click', function (e) {
         e.target.children[0].setAttribute('style', 'display: block');
      });

      newSection.appendChild(paragraph);
      content.appendChild(newSection);
   }
}