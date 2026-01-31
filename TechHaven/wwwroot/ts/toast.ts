enum ToastType {
    Cart,
    Wishlist
}
type ToastOptions = WishlistToastOptions | CartToastOptions; // Extendable for other toast types in the future

type CartToastOptions = {
    type: ToastType.Cart,
    cartItems: string[]
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
    const toast = new bootstrap.Toast(toastEl, { delay: 2000 });
    toast.show();
}