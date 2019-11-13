function solve() {
  let sections = document.getElementsByTagName('section');
  let answers = [];

  sections[0].querySelector('[data-quizIndex="2"]').addEventListener('click', function () {
    answers.push(2);
    sections[0].setAttribute('style', 'display: none');
    sections[1].setAttribute('style', 'display: block');
  });

  sections[0].querySelector('[data-quizIndex="4"]').addEventListener('click', function () {
    answers.push(4);
    sections[0].setAttribute('style', 'display: none');
    sections[1].setAttribute('style', 'display: block');
  });

  sections[1].querySelector('[data-quizIndex="2"]').addEventListener('click', function () {
    answers.push(2);
    sections[1].setAttribute('style', 'display: none');
    sections[2].setAttribute('style', 'display: block');
  });

  sections[1].querySelector('[data-quizIndex="4"]').addEventListener('click', function () {
    answers.push(4);
    sections[1].setAttribute('style', 'display: none');
    sections[2].setAttribute('style', 'display: block');
  });

  sections[2].querySelector('[data-quizIndex="2"]').addEventListener('click', function () {
    answers.push(2);
    sections[2].setAttribute('style', 'display: none');
    displayResult();
  });

  sections[2].querySelector('[data-quizIndex="4"]').addEventListener('click', function () {
    answers.push(4);
    sections[2].setAttribute('style', 'display: none');
    displayResult();
  });


  function displayResult() {
    let expected = [2, 4, 2];

    let score = 0;
    for (let i = 0; i < 3; i++) {
      if (answers[i] === expected[i]) {
        score++;
      }
    }

    let resultList = document.getElementById('results');
    resultList.setAttribute('style', 'display: block');
    let result = resultList.getElementsByTagName('h1')[0];
    if (score === 3) {
      result.textContent = 'You are recognized as top JavaScript fan!';
    } else {
      result.textContent = `You have ${score} right answers`;
    }
    console.log(result);

  }
}
