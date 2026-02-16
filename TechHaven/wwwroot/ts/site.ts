/// <reference types="bootstrap" />

const errorDiv = document.querySelector('div.alert');

if (errorDiv) {
    const closeBtn = document.createElement('button');
    closeBtn.innerHTML = '&times;';
    closeBtn.className = 'close-btn';


    closeBtn.onclick = function () {
        errorDiv.remove();
    };

    errorDiv.appendChild(closeBtn);
}
