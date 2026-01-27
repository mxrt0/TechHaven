"use strict";
const wishlistBtn = document.querySelector('.wishlist-btn');
wishlistBtn.addEventListener('click', async () => {
    var _a;
    const productId = wishlistBtn.dataset.productId;
    const text = wishlistBtn.querySelector('.wishlist-text');
    if (!productId) {
        return;
    }
    wishlistBtn.disabled = true;
    const response = await fetch(`/Wishlist/Toggle?productId=${productId}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'RequestVerificationToken': (_a = document.querySelector('input[name="__RequestVerificationToken"]')) === null || _a === void 0 ? void 0 : _a.value
        }
    });
    if (!response.ok) {
        wishlistBtn.disabled = false;
        return;
    }
    const result = await response.json();
    const icon = wishlistBtn.querySelector('i');
    icon === null || icon === void 0 ? void 0 : icon.classList.toggle("bi-heart");
    icon === null || icon === void 0 ? void 0 : icon.classList.toggle("bi-heart-fill");
    icon === null || icon === void 0 ? void 0 : icon.classList.toggle("text-danger");
    if (text) {
        text.textContent = result.added ? "Remove From Wishlist" : "Add To Wishlist";
    }
    icon === null || icon === void 0 ? void 0 : icon.classList.add('toggle-animation');
    setTimeout(() => icon === null || icon === void 0 ? void 0 : icon.classList.remove('toggle-animation'), 300);
    showWishlistToast(result.message, result.added);
    wishlistBtn.disabled = false;
});
function showWishlistToast(message, added = true) {
    const toastEl = document.getElementById('wishlistToast');
    const body = document.getElementById('wishlistToastBody');
    const timer = toastEl.querySelector('.neon-timer');
    body.textContent = message;
    timer.style.transition = "none";
    timer.style.width = "100%";
    void timer.offsetWidth;
    timer.style.transition = `width 2ms linear`;
    timer.style.width = "0%";
    toastEl.style.borderColor = added ? "#0ff" : "#ff0";
    toastEl.style.color = added ? "#0ff" : "#ff0";
    toastEl.style.textShadow = added
        ? "0 0 2px #0ff, 0 0 5px #0ff, 0 0 10px #0ff"
        : "0 0 2px #ff0, 0 0 5px #ff0, 0 0 10px #ff0";
    // @ts-ignore
    const toast = new bootstrap.Toast(toastEl, { delay: 2000 });
    toast.show();
}
