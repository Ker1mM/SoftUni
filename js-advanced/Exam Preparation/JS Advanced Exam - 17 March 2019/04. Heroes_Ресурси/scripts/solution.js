function solve() {
   let buttons = document.getElementsByTagName('button');
   buttons[0].addEventListener('click', function () { rebuildKingdom() });
   buttons[1].addEventListener('click', function () { joinKingdom() });
   buttons[2].addEventListener('click', function () { fight() });

   let kingdomNames = ['CASTLE', 'DUNGEON', 'FORTRESS', 'INFERNO', 'NECROPOLIS', 'RAMPART', 'STRONGHOLD', 'TOWER', 'CONFLUX'];

   function rebuildKingdom() {
      let kingdomName = document.querySelector('input[placeholder="Kingdom..."]').value;
      let kingName = document.querySelector('input[placeholder="King..."]').value;
      let validKing = kingName.length >= 2;
      let vlaidKingdom = kingdomNames.includes(kingdomName.toUpperCase());
      if (!validKing || !vlaidKingdom) {
         if (!validKing) {
            document.querySelector('input[placeholder="King..."]').value = '';
         }
         if (!vlaidKingdom) {
            document.querySelector('input[placeholder="Kingdom..."]').value = '';
         }
      } else {
         let kingdomElement = document.getElementById(kingdomName.toLowerCase());
         kingdomElement.innerHTML = '';
         kingdomElement.style.display = 'block';

         let kingdomNameElement = document.createElement('h1');
         kingdomNameElement.innerHTML = kingdomName.toUpperCase();

         let castleElement = document.createElement('div');
         castleElement.setAttribute('class', 'castle');

         let kingNameElement = document.createElement('h2');
         kingNameElement.innerHTML = kingName.toUpperCase();

         let fieldSetElement = document.createElement('fieldset');
         let legend = document.createElement('legend');
         legend.innerHTML = 'Army';
         let tanks = document.createElement('p');
         tanks.innerHTML = 'TANKS - 0';
         let fighters = document.createElement('p');
         fighters.innerHTML = 'FIGHTERS - 0';
         let mages = document.createElement('p');
         mages.innerHTML = 'MAGES - 0';
         let armyOutput = document.createElement('div');
         armyOutput.setAttribute('class', 'armyOutput');
         fieldSetElement.appendChild(legend);
         fieldSetElement.appendChild(tanks);
         fieldSetElement.appendChild(fighters);
         fieldSetElement.appendChild(mages);
         fieldSetElement.appendChild(armyOutput);


         kingdomElement.appendChild(kingdomNameElement);
         kingdomElement.appendChild(castleElement);
         kingdomElement.appendChild(kingNameElement);
         kingdomElement.appendChild(fieldSetElement);
      }
   }

   function joinKingdom() {
      let characterTypes = document.querySelectorAll('input[type="radio"]');
      let character = false;
      for (let char of characterTypes) {
         if (char.checked) {
            character = char.value;
            break;
         }
      }

      let characterName = document.querySelector('input[placeholder="Character..."]').value;
      let kingdomName = document.querySelectorAll('input[placeholder="Kingdom..."]')[1].value;

      let validCharacterName = characterName.length > 2;
      let kingdom = document.getElementById(kingdomName.toLowerCase());
      let validKingdom = (kingdom !== null && kingdom.style.display === 'block');


      if (validCharacterName && character && validKingdom) {
         let army = kingdom.getElementsByTagName('fieldset')[0].childNodes;
         let index = 0;
         switch (character.toLowerCase()) {
            case 'tank':
               index = 1;
               break;
            case 'fighter':
               index = 2;
               break;
            case 'mage':
               index = 3;
               break;
            default:
               break;
         }

         let selected = army[index].innerHTML;
         let tokens = selected.split(' - ');
         kingdom.getElementsByTagName('fieldset')[0].childNodes[index].innerHTML = `${tokens[0]} - ${Number(tokens[1]) + 1}`;

         let armyName = army[4].innerHTML;
         armyName += `${characterName} `;
         kingdom.getElementsByTagName('fieldset')[0].childNodes[4].innerHTML = armyName;
      } else {
         if (!validKingdom) {
            document.querySelectorAll('input[placeholder="Kingdom..."]')[1].value = '';
         }

         if (!validCharacterName) {
            document.querySelector('input[placeholder="Character..."]').value = '';
         }
      }
   }

   function fight() {

      let attacker = document.querySelector('input[placeholder="Attacker..."]').value;
      let defender = document.querySelector('input[placeholder="Defender..."]').value;

      let attackerKingdom = document.getElementById(attacker.toLowerCase());
      let defenderKingdom = document.getElementById(defender.toLowerCase());

      let validAttacker = (attackerKingdom !== null && attackerKingdom.style.display === 'block');
      let validDefender = (defenderKingdom !== null && defenderKingdom.style.display === 'block');
      if (validAttacker && validDefender) {
         let attackerArmy = attackerKingdom.getElementsByTagName('fieldset')[0].childNodes;
         let defenderArmy = defenderKingdom.getElementsByTagName('fieldset')[0].childNodes;

         let attackerPower = getPower(attackerArmy);
         let defenderPower = getPower(defenderArmy);

         if (attackerPower.attack > defenderPower.defense) {
            let attackerKing = attackerKingdom.getElementsByTagName('h2')[0].innerHTML;
            defenderKingdom.getElementsByTagName('h2')[0].innerHTML = attackerKing;
         }
      } else {
         if (!validAttacker) {
            document.querySelector('input[placeholder="Attacker..."]').value = '';
         }

         if (!validDefender) {
            document.querySelector('input[placeholder="Defender..."]').value = '';
         }
      }
   }

   let soldierPowers = {
      mages: { attack: 70, defense: 30 },
      fighters: { attack: 50, defense: 50 },
      tanks: { attack: 20, defense: 80 }
   };

   function getPower(army) {
      let totalAttack = 0;
      let totalDefense = 0;
      for (let i = 1; i <= 3; i++) {
         let tokens = army[i].innerHTML.split(' - ');

         totalAttack += Number(tokens[1]) * soldierPowers[tokens[0].toLowerCase()].attack;
         totalDefense += Number(tokens[1]) * soldierPowers[tokens[0].toLowerCase()].defense;
      }

      return { attack: totalAttack, defense: totalDefense };
   }
}


solve();
