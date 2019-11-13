function solve() {
   let player1 = document.getElementById('player1Div');
   let player2 = document.getElementById('player2Div');

   let leftValue = null;
   let rightValue = null;

   Array
      .from(player1.childNodes)
      .map(x => {
         x.addEventListener('click', function () {
            x.setAttribute('src', 'images/whiteCard.jpg');
            document.getElementById('result').childNodes[1].textContent = x.name;
            leftValue = x;
            let compareResult = compare(x);
         })
      });

   Array
      .from(player2.childNodes)
      .map(x => {
         x.addEventListener('click', function () {
            x.setAttribute('src', 'images/whiteCard.jpg');
            document.getElementById('result').childNodes[5].textContent = x.name;
            rightValue = x;
            let compareResult = compare();
         })
      });

   function compare() {
      if (leftValue === null || rightValue === null) {
         return 0;
      } else if (Number(leftValue.name) > Number(rightValue.name)) {
         leftValue.setAttribute('style', 'border: 2px solid green');
         rightValue.setAttribute('style', 'border: 2px solid red');
      } else if (Number(leftValue.name) < Number(rightValue.name)) {
         leftValue.setAttribute('style', 'border: 2px solid red');
         rightValue.setAttribute('style', 'border: 2px solid green');
      }
      document.getElementById('result').childNodes[1].textContent = '';
      document.getElementById('result').childNodes[5].textContent = '';

      document.getElementById('history').textContent += `[${leftValue.name} vs ${rightValue.name}] `

      leftValue = null;
      rightValue = null;
   }
}