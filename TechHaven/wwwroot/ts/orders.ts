const tempMsgContainer = document.getElementById('temp-message');
const tempMsg = tempMsgContainer?.dataset.tempMsg;
if (tempMsg) {
    showToast(tempMsg, {
        type: ToastType.Order,
        delay: 1800
    });
};
