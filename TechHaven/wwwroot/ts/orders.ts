const tempMsgContainer = document.getElementById('temp-message');
const tempMsg = tempMsgContainer?.dataset.tempMsg;
if (tempMsg) {
    showToast(tempMsg, {
        type: ToastType.Order,
        delay: 1800
    });
};

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

