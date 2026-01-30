/// <reference types="bootstrap" />
type ToggleJsonResponse = {
    added: boolean,
    message: string
};
const wishlistBtns = document.querySelectorAll<HTMLButtonElement>('.wishlist-btn');
wishlistBtns.forEach(wishlistBtn => {
    wishlistBtn.addEventListener('click', async () => {

        const productId = wishlistBtn.dataset.productId;

        if (!productId) {
            return;
        }
        wishlistBtn.disabled = true;

        const response = await fetch(`/Wishlist/Toggle?productId=${productId}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'RequestVerificationToken':
                    (document.querySelector('input[name="__RequestVerificationToken"]') as HTMLInputElement)?.value
            }
        })

        if (!response.ok) {
            wishlistBtn.disabled = false;
            return;
        }

        const result: ToggleJsonResponse = await response.json();

        const icon = wishlistBtn.querySelector('i');
        icon?.classList.toggle("bi-heart");
        icon?.classList.toggle("bi-heart-fill");
        icon?.classList.toggle("text-danger");
        if (icon) {
            icon.title = result.added ? "Remove From Wishlist" : "Add To Wishlist";
        }

        if (!result.added) {
            const productCard = document.querySelector(`#wc${productId}`);
            if (productCard) {
                productCard.remove();
            }
        }
        icon?.classList.add('toggle-animation');
        setTimeout(() => icon?.classList.remove('toggle-animation'), 300);
        showWishlistToast(result.message, result.added);

        wishlistBtn.disabled = false;

        const container = document.querySelector(".wishlist-page .row");
        if (!container?.querySelector(".wishlist-card")) {

            const emptyTemplate = document.querySelector(".wishlist-empty") as HTMLElement;
            if (emptyTemplate) {
                emptyTemplate.style.display = "block";
                container?.parentElement?.appendChild(emptyTemplate);
            }
        }
    });
})


function showWishlistToast(message: string, added: boolean = true) {
    const toastEl = document.getElementById('wishlistToast') as HTMLElement;
    const body = document.getElementById('wishlistToastBody') as HTMLElement;
    const timer = toastEl.querySelector('.neon-timer') as HTMLElement;

    body.textContent = message;

    timer.style.transition = "none";
    timer.style.width = "100%";

    void timer.offsetWidth;

    timer.style.transition = `width 2ms linear`;
    timer.style.width = "0%";

    toastEl.style.borderColor = added ? "#0ff" : "#f43f5e";
    toastEl.style.color = added ? "#0ff" : "#f43f5e";
    toastEl.style.textShadow = added
        ? "0 0 2px #0ff, 0 0 5px #0ff, 0 0 10px #0ff"
        : "0 0 2px #f43f5e, 0 0 4px #f43f5e, 0 0 6px #f43f5e";


    const toast = new bootstrap.Toast(toastEl, { delay: 2000 });
    toast.show();
}