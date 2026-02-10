const tempMsgDiv = document.getElementById('temp-message');
const tempMsg = tempMsgDiv?.dataset.tempMsg;
if (tempMsg) {
    showToast(tempMsg, {
        type: ToastType.Cart,
        delay: 2000
    });
};