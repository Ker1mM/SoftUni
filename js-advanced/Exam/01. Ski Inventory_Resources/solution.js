function solve() {
   let productsElement = document.getElementById('products');
   let addNewElement = document.getElementById('add-new');
   let myProductsElement = document.getElementById('myProducts');
   addNewElement.getElementsByTagName('button')[0].addEventListener('click', addProduct);
   productsElement.getElementsByTagName('button')[0].addEventListener('click', filter);
   myProductsElement.getElementsByTagName('button')[0].addEventListener('click', buy);
   let availableProducts = [];

   function buy(ev) {
      ev.preventDefault();
      document.getElementsByTagName('h1')[1].innerHTML = 'Total Price: 0.00';

      myProductsElement.childNodes[3].innerHTML = '';

   }

   function filter(ev) {
      ev.preventDefault();
      let filterString = productsElement.querySelector('input[id="filter"]').value;
      let items = productsElement.getElementsByTagName('li');

      for (let item of items) {
         let name = item.childNodes[0].innerHTML.toLowerCase();
         if (!name.includes(filterString.toLowerCase())) {
            item.style.display = 'none';
         } else {
            item.style.display = 'block';
         }
      }
   }

   function addProduct(ev) {
      ev.preventDefault();
      let name = addNewElement.querySelector('input[placeholder="Name"]').value;
      let quantity = addNewElement.querySelector('input[placeholder="Quantity"]').value;
      let price = addNewElement.querySelector('input[placeholder="Price"]').value;

      let product = { name: name, quantity: quantity, price: price };
      availableProducts.push(product);

      let newProduct = document.createElement('li');

      let nameElement = document.createElement('span');
      nameElement.innerHTML = name;

      availableProducts.push({ name, price, quantity });

      let priceElement = document.createElement('div');

      let innerPriceElement = document.createElement('strong');
      innerPriceElement.innerHTML = Number(price).toFixed(2);
      let button = document.createElement('button');
      button.innerHTML = 'Add to Client\'s List';
      let availableElement = document.createElement('strong');
      availableElement.innerHTML = `Available: ${quantity}`;
      button.addEventListener('click', function () {
         let myProducts = myProductsElement.childNodes[3];
         let myProductLi = document.createElement('li');
         let textNode = document.createTextNode(name);
         myProductLi.appendChild(textNode);
         let strongPrice = document.createElement('strong');
         strongPrice.innerHTML = Number(price).toFixed(2);
         myProductLi.appendChild(strongPrice);
         myProducts.appendChild(myProductLi);

         let totalPriceElement = document.getElementsByTagName('h1')[1].innerHTML;
         let tokens = totalPriceElement.split(' ');
         document.getElementsByTagName('h1')[1].innerHTML = `Total Price: ${((Number(tokens[2]) + Number(price))).toFixed(2)}`;

         let current = availableProducts.find(x => x.name === name);
         if (current.quantity - 1 > 0) {
            current.quantity--;
            newProduct.getElementsByTagName('strong')[0].innerHTML = `Available: ${current.quantity}`;
         } else {
            productsElement.childNodes[3].removeChild(newProduct);
         }
      })


      priceElement.appendChild(innerPriceElement);
      priceElement.appendChild(button);
      newProduct.appendChild(nameElement);
      newProduct.appendChild(availableElement);
      newProduct.appendChild(priceElement);
      productsElement.childNodes[3].appendChild(newProduct);
   }
}
