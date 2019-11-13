function solve() {
   let tbody = document.getElementsByTagName('tbody')[0];
   let trs = tbody.getElementsByTagName('tr');
   let selected;
   Array
      .from(trs)
      .map(x => x.addEventListener('click', function () {
         if (this.style.backgroundColor !== '') {
            this.style.backgroundColor = '';
         } else {
            this.style.backgroundColor = "#413f5e";
         }
         if (selected && selected !== this) {
            selected.style.backgroundColor = '';
         }
         selected = this;
      }))
}
