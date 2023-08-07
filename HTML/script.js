const spinningImage = document.getElementById('spinningImage');

spinningImage.addEventListener('click', () => {
    spinningImage.classList.toggle('rotate');

    setTimeout(() => {
        spinningImage.classList.remove('rotate');
    }, 1000);
});

const shakingText = document.getElementById('shakingText');

shakingText.addEventListener('click', () => {
    shakingText.textContent = "I! AM! SHAKING!";

    shakingText.classList.add('shake-animation');

    setTimeout(() => {
        shakingText.classList.remove('shake-animation');

        shakingText.textContent = "(Click me to shake again!) About me:";
    }, 500);
});