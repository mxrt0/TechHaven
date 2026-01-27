type ToggleJsonResponse = {
    added: boolean,
    message: string
};
const wishlistBtn = document.querySelector('.wishlist-btn') as HTMLButtonElement;
wishlistBtn.addEventListener('click', async () => {

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
            'RequestVerificationToken':
                (document.querySelector('input[name = "__RequestVerificationToken"]') as HTMLInputElement)?.value
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

    if (text) {
        text.textContent = result.added ? "Remove From Wishlist" : "Add To Wishlist";
    }

    icon?.classList.add('toggle-animation');
    setTimeout(() => icon?.classList.remove('toggle-animation'), 300);
    showWishlistToast(result.message, result.added);

    wishlistBtn.disabled = false;
});

function showWishlistToast(message: string, added: boolean = true) {
    const toastEl = document.getElementById('wishlistToast') as HTMLElement;
    const body = document.getElementById('wishlistToastBody') as HTMLElement;
    const timer = toastEl.querySelector('.neon-timer') as HTMLElement;

    body.textContent = message;

    // Reset timer
    timer.style.transition = "none";
    timer.style.width = "100%";

    // Force reflow
    void timer.offsetWidth;

    // Animate shrink
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