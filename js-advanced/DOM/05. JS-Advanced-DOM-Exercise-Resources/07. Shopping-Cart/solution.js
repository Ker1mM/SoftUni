function solve() {
   let products = document.getElementsByClassName('product');
   let addedProducts = [];
   let totalPrice = 0;


   Array
      .from(products)
      .map(x => {
         let name = x.querySelector('[class="product-title"]').firstChild.nodeValue;
         let price = x.querySelector('[class="product-line-price"]').firstChild.nodeValue;
         x.querySelector('[class="add-product"]').addEventListener('click', function () {
            addedProducts.push(name);
            totalPrice += Number(price);
            add(name, price);
         });
      })

   console.log(document.getElementsByClassName('checkout')[0]);
   document.getElementsByClassName('checkout')[0].addEventListener('click', function () {
      let uniqueAdded = [...new Set(addedProducts)]
      document.getElementsByTagName('textarea')[0].textContent += `You bought ${uniqueAdded.join(', ')} for ${totalPrice.toFixed(2)}.`;
      Array
      .from(document.getElementsByClassName('add-product'))
      .map(x => x.setAttribute('disabled', ''));

      document.getElementsByClassName('checkout')[0].setAttribute('disabled', '');
   });

   function add(name, price) {
      document.getElementsByTagName('textarea')[0].textContent += `Added ${name} for ${price
         } to the cart.\n`
   }


}