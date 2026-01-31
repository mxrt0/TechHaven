"use strict";
const addBtn = document.querySelector('.btn-cart');
addBtn.addEventListener('click', async () => await addToCart()); // base whether to delete or add on btn state
async function addToCart() {
    const quantityInput = document.querySelector('input[name="quantity"]');
    const productIdInput = document.querySelector('input[name="productId"]');
    if (!productIdInput) {
        return;
    }
    addBtn.disabled = true;
    const response = await fetch(`/Cart/Add?productId=${productIdInput.value}&quantity=${quantityInput.value}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value
        }
    });
    if (!response.ok) {
        addBtn.disabled = false;
        return;
    }
    const result = await response.json();
    if (result.success) {
        showToast(result.message, {
            type: ToastType.Cart,
            cartItems: [] // Future implementation can add actual cart items here
        });
        addBtn.textContent = "Remove From Cart";
        addBtn.disabled = false;
        quantityInput.disabled = true;
    }
}
async function removeFromCart() {
    // Future implementation for removing from cart
}
