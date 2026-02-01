"use strict";
const msgDiv = document.getElementById('temp-message');
const msg = msgDiv === null || msgDiv === void 0 ? void 0 : msgDiv.dataset.msg;
if (msg) {
    showToast(msg, {
        type: ToastType.Cart,
    });
}
;
const addBtn = document.querySelector('.btn-cart');
const removeBtns = document.querySelectorAll('.btn-remove');
const updateBtns = document.querySelectorAll('.btn-update');
addBtn === null || addBtn === void 0 ? void 0 : addBtn.addEventListener('click', async () => await addToCart());
removeBtns.forEach(removeBtn => {
    if (removeBtn && !removeBtn.classList.contains('btn-cart')) {
        removeBtn.addEventListener('click', async () => {
            var _a, _b, _c;
            const productIdInput = (_a = removeBtn.closest('.cart-item-actions')) === null || _a === void 0 ? void 0 : _a.querySelector('input[name="productId"]');
            if (productIdInput) {
                await removeFromCart(productIdInput.value);
                const subtotalText = (_b = removeBtn.closest('.cart-item-card')) === null || _b === void 0 ? void 0 : _b.querySelector('.cart-item-subtotal strong');
                const totalText = document.querySelector('.cart-total strong');
                if (subtotalText && totalText) {
                    const removedSubtotal = parseFloat(subtotalText.textContent.trim().split(' ')[0].replace(',', '.'));
                    const currentTotal = parseFloat(totalText.textContent.trim().split(' ')[0].replace(',', '.'));
                    totalText.textContent = (currentTotal - removedSubtotal).toFixed(2) + ' €';
                }
                (_c = removeBtn.closest('.cart-item-card')) === null || _c === void 0 ? void 0 : _c.remove();
                if ((totalText === null || totalText === void 0 ? void 0 : totalText.textContent) === '0.00 €') {
                    window.location.reload();
                }
            }
        });
    }
});
updateBtns.forEach(updateBtn => {
    var _a;
    if (updateBtn) {
        updateBtn.disabled = true;
        const quantityInput = (_a = updateBtn.closest('.cart-item-actions')) === null || _a === void 0 ? void 0 : _a.querySelector('input[name="quantity"]');
        let initialQuantity = quantityInput.value.trim();
        quantityInput === null || quantityInput === void 0 ? void 0 : quantityInput.addEventListener('change', () => {
            if ((quantityInput === null || quantityInput === void 0 ? void 0 : quantityInput.value) !== initialQuantity) {
                updateBtn.disabled = false;
            }
            else {
                updateBtn.disabled = true;
            }
        });
        updateBtn.addEventListener('click', async () => {
            var _a, _b;
            const productIdInput = (_a = updateBtn.closest('.cart-item-actions')) === null || _a === void 0 ? void 0 : _a.querySelector('input[name="productId"]');
            if (productIdInput) {
                await updateCart(productIdInput.value, quantityInput.value);
                const subtotalText = (_b = updateBtn.closest('.cart-item-card')) === null || _b === void 0 ? void 0 : _b.querySelector('.cart-item-subtotal strong');
                const totalText = document.querySelector('.cart-total strong');
                if (subtotalText && totalText) {
                    const newQuantity = parseInt(quantityInput.value.trim());
                    if (isNaN(newQuantity) || newQuantity <= 0) {
                        return;
                    }
                    const quantityDiff = newQuantity - parseInt(initialQuantity);
                    const initialSubtotal = parseFloat(subtotalText.textContent.trim().split(' ')[0].replace(',', '.'));
                    const unitPrice = initialSubtotal / parseInt(initialQuantity);
                    subtotalText.textContent = (initialSubtotal + quantityDiff * unitPrice).toFixed(2) + ' €';
                    const currentTotal = parseFloat(totalText.textContent.trim().split(' ')[0].replace(',', '.'));
                    totalText.textContent = (currentTotal + quantityDiff * unitPrice).toFixed(2) + ' €';
                }
                initialQuantity = quantityInput.value.trim();
                updateBtn.disabled = true;
            }
        });
    }
});
async function addToCart() {
    if (!addBtn) {
        return;
    }
    const quantityInput = document.querySelector('input[name="quantity"]');
    const productIdInput = document.querySelector('input[name="productId"]');
    if (!productIdInput) {
        return;
    }
    addBtn.disabled = true;
    if (addBtn.classList.contains('btn-remove')) {
        await removeFromCart(productIdInput.value);
        addBtn.textContent = "Add To Cart";
        addBtn.disabled = false;
        addBtn.classList.toggle('btn-remove');
        quantityInput.disabled = false;
        quantityInput.hidden = false;
        return;
    }
    const response = await fetch(`/Cart/Add?productId=${productIdInput.value}&quantity=${quantityInput === null || quantityInput === void 0 ? void 0 : quantityInput.value}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
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
        });
        addBtn.textContent = "Remove From Cart";
        addBtn.disabled = false;
        addBtn.classList.toggle('btn-remove');
        quantityInput.disabled = true;
        quantityInput.hidden = true;
    }
}
async function removeFromCart(productId) {
    if (!productId) {
        return;
    }
    const response = await fetch(`/Cart/Remove?productId=${productId}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        }
    });
    if (!response.ok) {
        return;
    }
    const result = await response.json();
    if (result.success) {
        showToast(result.message, {
            type: ToastType.Cart,
        });
    }
}
async function updateCart(productId, newQuantity = '1') {
    if (!productId || parseInt(newQuantity) <= 0) {
        alert('Please enter a non-negative quantity!');
        return;
    }
    const response = await fetch(`/Cart/UpdateQuantity?productId=${productId}&quantity=${newQuantity}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        }
    });
    if (!response.ok) {
        return;
    }
    const result = await response.json();
    if (result.success) {
        showToast(result.message, {
            type: ToastType.Cart,
        });
    }
}
