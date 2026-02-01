"use strict";
var ToastType;
(function (ToastType) {
    ToastType[ToastType["Cart"] = 0] = "Cart";
    ToastType[ToastType["Wishlist"] = 1] = "Wishlist";
})(ToastType || (ToastType = {}));
function showToast(message, options) {
    const toastEl = document.getElementById('wishlistToast');
    const body = document.getElementById('wishlistToastBody');
    body.textContent = message;
    const timer = toastEl.querySelector('.neon-timer');
    timer.style.transition = "none";
    timer.style.width = "100%";
    void timer.offsetWidth;
    timer.style.transition = `width 2ms linear`;
    timer.style.width = "0%";
    switch (options.type) {
        case ToastType.Wishlist:
            toastEl.style.borderColor = options.added ? "#0ff" : "#f43f5e";
            toastEl.style.color = options.added ? "#0ff" : "#f43f5e";
            toastEl.style.textShadow = options.added
                ? "0 0 2px #0ff, 0 0 5px #0ff, 0 0 10px #0ff"
                : "0 0 2px #f43f5e, 0 0 4px #f43f5e, 0 0 6px #f43f5e";
        case ToastType.Cart:
            toastEl.style.borderColor = "#0ff";
            toastEl.style.color = "#38bdf8";
            toastEl.style.textShadow = "0 0 4px #38bdf8, 0 0 10px #0ff, 0 0 20px #0ff70";
    }
    const toast = new bootstrap.Toast(toastEl, { delay: options.delay || 2000 });
    toast.show();
}
