"use strict";
const wishlistBtns = document.querySelectorAll('.wishlist-btn');
wishlistBtns.forEach(wishlistBtn => {
    wishlistBtn.addEventListener('click', async () => {
        var _a, _b;
        const productId = wishlistBtn.dataset.productId;
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
        if (icon) {
            icon.title = result.added ? "Remove From Wishlist" : "Add To Wishlist";
        }
        if (!result.added) {
            const productCard = document.querySelector(`#wc${productId}`);
            if (productCard) {
                productCard.remove();
            }
        }
        icon === null || icon === void 0 ? void 0 : icon.classList.add('toggle-animation');
        setTimeout(() => icon === null || icon === void 0 ? void 0 : icon.classList.remove('toggle-animation'), 300);
        showToast(result.message, {
            type: ToastType.Wishlist,
            added: result.added
        });
        wishlistBtn.disabled = false;
        const container = document.querySelector(".wishlist-page .row");
        if (!(container === null || container === void 0 ? void 0 : container.querySelector(".wishlist-card"))) {
            const emptyTemplate = document.querySelector(".wishlist-empty");
            if (emptyTemplate) {
                emptyTemplate.style.display = "block";
                (_b = container === null || container === void 0 ? void 0 : container.parentElement) === null || _b === void 0 ? void 0 : _b.appendChild(emptyTemplate);
            }
        }
    });
});
