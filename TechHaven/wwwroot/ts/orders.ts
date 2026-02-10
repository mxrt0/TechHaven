const modal = document.getElementById('cancelModal');
const modalForm = document.getElementById('cancelModalForm') as HTMLFormElement;

modal?.addEventListener('show.bs.modal', (event: any) => {
    const button = event.relatedTarget as HTMLButtonElement | null;
    if (!button || !modalForm) return;

    const orderId = button.dataset.orderId;
    if (orderId) {
        modalForm.action = `/Orders/Cancel/${orderId}`;
    }
});

