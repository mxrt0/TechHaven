type CartResponse = {
    success: boolean,
    message: string
};

const addBtn = document.querySelector<HTMLButtonElement>('.btn-cart');
const removeBtns = document.querySelectorAll<HTMLButtonElement>('.btn-remove'); 
const updateBtns = document.querySelectorAll<HTMLButtonElement>('.btn-update');

addBtn?.addEventListener('click', async () => await addToCart());

removeBtns.forEach(removeBtn => {
    if (removeBtn && !removeBtn.classList.contains('btn-cart')) {
        removeBtn.addEventListener('click', async () => {
            const productIdInput = removeBtn.closest('.cart-item-actions')?.querySelector<HTMLInputElement>('input[name="productId"]');
            if (productIdInput) {
                await removeFromCart(productIdInput.value);
                const subtotalText = removeBtn.closest('.cart-item-card')?.querySelector<HTMLElement>('.cart-item-subtotal strong');
                const totalText = document.querySelector<HTMLElement>('.cart-total strong');
                if (subtotalText && totalText) {
                    const removedSubtotal = parseFloat(subtotalText.textContent!.trim().split(' ')[0]!.replace(',', '.'));
                    const currentTotal = parseFloat(totalText.textContent!.trim().split(' ')[0]!.replace(',', '.'));

                    totalText.textContent = (currentTotal - removedSubtotal).toFixed(2) + ' €';
                }
                removeBtn.closest('.cart-item-card')?.remove();
                if (totalText?.textContent === '0.00 €') {
                    window.location.reload();
                }
            }           
        });
    }
});
updateBtns.forEach(updateBtn => {
    if (updateBtn) {
        updateBtn.disabled = true;
        const quantityInput = updateBtn.closest('.cart-item-actions')?.querySelector<HTMLInputElement>('input[name="quantity"]') as HTMLInputElement;
        let initialQuantity = quantityInput.value.trim();

        quantityInput?.addEventListener('change', () => {
            if (quantityInput?.value !== initialQuantity) {
                updateBtn.disabled = false;
            }
            else {
                updateBtn.disabled = true;
            }
        });

        updateBtn.addEventListener('click', async () => {

            const productIdInput = updateBtn.closest('.cart-item-actions')?.querySelector<HTMLInputElement>('input[name="productId"]');

            if (productIdInput) {

                await updateCart(productIdInput.value, quantityInput.value)

                const subtotalText = updateBtn.closest('.cart-item-card')?.querySelector<HTMLElement>('.cart-item-subtotal strong');
                const totalText = document.querySelector<HTMLElement>('.cart-total strong');

                if (subtotalText && totalText) {
                    const newQuantity = parseInt(quantityInput.value.trim());
                    if (isNaN(newQuantity) || newQuantity <= 0) {
                        return;
                    }
                    const quantityDiff = newQuantity - parseInt(initialQuantity);

                    const initialSubtotal = parseFloat(subtotalText.textContent!.trim().split(' ')[0]!.replace(',','.'));
                    const unitPrice = initialSubtotal / parseInt(initialQuantity);

                    subtotalText.textContent = (initialSubtotal + quantityDiff * unitPrice).toFixed(2) + ' €';

                    const currentTotal = parseFloat(totalText.textContent!.trim().split(' ')[0]!.replace(',', '.'));
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
    
    const quantityInput = document.querySelector<HTMLInputElement>('input[name="quantity"]');
    const productIdInput = document.querySelector<HTMLInputElement>('input[name="productId"]');

    if (!productIdInput) {
        return;
    }

    addBtn.disabled = true;

    if (addBtn.classList.contains('btn-remove')) {
        await removeFromCart(productIdInput.value);
        addBtn.textContent = "Add To Cart";

        addBtn.disabled = false;
        addBtn.classList.toggle('btn-remove');

        quantityInput!.disabled = false;
        quantityInput!.hidden = false;
        return;
    }


    const response = await fetch(`/Cart/Add?productId=${productIdInput.value}&quantity=${quantityInput?.value}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',           
        }
    });

    if (!response.ok) {
        addBtn.disabled = false;
        return;
    }

    const result: CartResponse = await response.json();
    if (result.success) {
        showToast(result.message, {
                        type: ToastType.Cart,
        }); 
        addBtn.textContent = "Remove From Cart";
        addBtn.disabled = false;
        addBtn.classList.toggle('btn-remove');
        quantityInput!.disabled = true;
        quantityInput!.hidden = true;
    }
}

async function removeFromCart(productId: string) {
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

    const result: CartResponse = await response.json();
    if (result.success) {
        showToast(result.message, {
            type: ToastType.Cart,
        });
    }

}

async function updateCart(productId: string, newQuantity: string = '1') {
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

    const result: CartResponse = await response.json();
    if (result.success) {
        showToast(result.message, {
            type: ToastType.Cart,
        });
    } 
}

