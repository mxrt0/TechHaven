enum ToastType {
    Cart,
    Wishlist,
    Order
}
type ToastOptions = (WishlistToastOptions | CartToastOptions | OrderToastOptions) & { delay?: number };

type CartToastOptions = {
    type: ToastType.Cart,
}
type OrderToastOptions = {
    type: ToastType.Order,
}

type WishlistToastOptions = {
    type: ToastType.Wishlist,
    added: boolean
}
function showToast(message: string, options: ToastOptions) {

    const toastEl = document.getElementById('wishlistToast') as HTMLElement;
    const body = document.getElementById('wishlistToastBody') as HTMLElement;
    body.textContent = message;

    const timer = toastEl.querySelector('.neon-timer') as HTMLElement;
    timer.style.transition = "none";
    timer.style.width = "100%";

    void timer.offsetWidth;

    const displayDelay = options.delay || 2000;
    timer.style.transition = `width ${displayDelay}ms linear`;
    timer.style.width = "0%";

    const cyan = "#0ff";
    const softBlue = "#38bdf8";
    const rose = "#f43f5e";

    switch (options.type) {
        case ToastType.Wishlist:
            const isAdded = options.added;
            const color = isAdded ? cyan : rose;

            toastEl.style.borderColor = color;
            toastEl.style.color = color;

            toastEl.style.textShadow = `0 0 2px ${color}, 0 0 8px ${isAdded ? 'rgba(0,255,255,0.5)' : 'rgba(244,63,94,0.5)'}`;
            toastEl.style.boxShadow = `0 0 15px ${isAdded ? 'rgba(0,255,255,0.15)' : 'rgba(244,63,94,0.15)'}`;

            break;

        case ToastType.Cart:
            toastEl.style.borderColor = cyan;
            toastEl.style.color = softBlue;

            toastEl.style.textShadow = `0 0 2px ${softBlue}, 0 0 10px rgba(56,189,248,0.6)`;
            toastEl.style.boxShadow = `0 0 15px rgba(0,255,255,0.15)`;
            break;

        case ToastType.Order:
            toastEl.style.borderColor = "#00ff9e";
            toastEl.style.color = "#00ff9e";
            toastEl.style.textShadow = `0 0 2px #00ff9e, 0 0 10px rgba(0,255,158,0.5)`;
            toastEl.style.boxShadow = `0 0 15px rgba(0,255,158,0.15)`;
            break;
    }
    const toast = new bootstrap.Toast(toastEl, { delay: displayDelay });
    toast.show();
}