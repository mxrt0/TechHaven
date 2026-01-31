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
        showToast(result.message, {
            type: ToastType.Wishlist,
            added: result.added
        });

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
