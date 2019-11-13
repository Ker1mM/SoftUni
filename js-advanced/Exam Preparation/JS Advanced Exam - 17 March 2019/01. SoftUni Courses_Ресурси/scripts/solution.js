function solve() {
   document.getElementsByTagName('button')[0].addEventListener('click', function () {
      calculate();
   })

   function calculate() {

      let fundamentalsPrice = 170;
      let advancedPrice = 180;
      let applicationsPrice = 190;
      let webPrice = 490;
      let totalPrice = 0;
      let fundamentals = document.getElementsByName('js-fundamentals')[0].checked;
      let advanced = document.getElementsByName('js-advanced')[0].checked;
      let applications = document.getElementsByName('js-applications')[0].checked;
      let web = document.getElementsByName('js-web')[0].checked;
      let bonusCourse = false;

      if (fundamentals && advanced) {
         advancedPrice *= 0.90;
      }
      let body = document.createElement('ul');
      if (fundamentals && advanced && applications) {
         totalPrice = (fundamentalsPrice + advancedPrice + applicationsPrice) * 0.94;
         if (web) {
            bonusCourse = true;
            totalPrice += webPrice;
         }
      } else {
         if (fundamentals) {
            totalPrice += fundamentalsPrice;
         }
         if (advanced) {
            totalPrice += advancedPrice;
         }
         if (applications) {
            totalPrice += applicationsPrice;
         }
         if (web) {
            totalPrice += webPrice;
         }
      }

      if (isOnline()) {
         totalPrice *= 0.94;
      }
      totalPrice = Math.floor(totalPrice);
      addCourses(fundamentals, advanced, applications, web, bonusCourse);

      let priceField = document.getElementsByClassName('courseFoot')[1];
      priceField.childNodes[1].innerHTML = `Cost: ${totalPrice.toFixed(2)} BGN`

   }

   function isOnline() {
      let types = document.getElementsByName('educationForm');
      if (types[0].checked) {
         return false;
      }

      return true;
   }

   function addCourses(fundamentals, advanced, applications, web, bonusCourse) {
      let body = document.createElement('ul');
      if(fundamentals){
         let element = document.createElement('li');
         element.innerHTML = 'JS-Fundamentals';
         body.appendChild(element);
      }
      if(advanced){
         let element = document.createElement('li');
         element.innerHTML = 'JS-Advanced';
         body.appendChild(element);
      }
      if(applications){
         let element = document.createElement('li');
         element.innerHTML = 'JS-Applications';
         body.appendChild(element);
      }
      if(web){
         let element = document.createElement('li');
         element.innerHTML = 'JS-Web';
         body.appendChild(element);
      }
      if(bonusCourse){
         let element = document.createElement('li');
         element.innerHTML = 'HTML and CSS';
         body.appendChild(element);
      }

      let resultBody = document.getElementsByClassName('courseBody')[1];
      resultBody.innerHTML = '';
      resultBody.appendChild(body);
   }
}

solve();