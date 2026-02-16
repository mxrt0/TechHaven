const tempMsgDiv = document.getElementById('temp-message');
const tempMsg = tempMsgDiv?.dataset.tempMsg;
const typeData = tempMsgDiv?.dataset.type;

if (tempMsg) {

    if (typeData)
    {
        switch (typeData) {
            case 'cart':
                showToast(tempMsg, {
                    type: ToastType.Cart,
                    delay: 2000
                });
                break;

            case 'order':
                showToast(tempMsg, {
                    type: ToastType.Order,
                    delay: 2000
                });
                break;
        }
    }
    else {
        showToast(tempMsg, {
            type: ToastType.Cart,
            delay: 2000
        });
    }
    
};