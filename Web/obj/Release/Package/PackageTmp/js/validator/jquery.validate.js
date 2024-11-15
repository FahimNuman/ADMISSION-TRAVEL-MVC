var specialOneRegEx = /[^a-zA-Z0-9]/;
var numberOneRegEx = /^(?=.*[0-9]).+$/;
var upperCaseRegEx = /^(?=.*[A-Z]).+$/;
var lowerCaseRegEx = /^(?=.*[a-z]).+$/;
var regExpEmail = /^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/;
var regExpMobile = /^07[\s\d]+$/;
var regExpName = /^[a-zA-Z0-9 '\-\\s]{1,90}$/;

$(document).ready(function () {
    $.fn.MyFormValidation = function () {
        var returnValue = true;

        $(".validation-message").remove();

        this.each(function () {
            var id = this.id;
            //console.log(id);
            if ((typeof id !== 'undefined') && id !== null && id !== '') {

                var valueInput = $.trim($("#" + id).val());
                var validationType = $.trim($("#" + id).attr("data-validation"));
                var errorMsg = $.trim($("#" + id).attr("error-msg"));
                var expressionMsg = $.trim($("#" + id).attr("expression-msg"));
                var confirmId = $.trim($("#" + id).attr("confirmation-id"));

                if ((typeof valueInput === 'undefined') || valueInput == null || valueInput == '') {
                    returnValue = false;
                    $("#" + id).after("<span class='validation-message error-msg'>" + errorMsg + "</span>");
                } else {
                    if (validationType == 'name' && !regExpName.test(valueInput) && valueInput != "") {
                        returnValue = false;
                        $("#" + id).after("<span class='validation-message error-msg'>" + expressionMsg + "</span>");
                    }
                    if (validationType == 'email' && !regExpEmail.test(valueInput) && valueInput != "") {
                        returnValue = false;
                        $("#" + id).after("<span class='validation-message error-msg'>" + expressionMsg + "</span>");
                    }
                    if (validationType == 'password' && PasswordErrorMessage(valueInput) && valueInput != "") {
                        returnValue = false;
                        $("#" + id).after("<span class='validation-message error-msg'>" + PasswordErrorMessage(valueInput) + "</span>");
                    }
                    if (validationType == 'confirmation' && valueInput != "") {
                        if (((typeof confirmId === 'undefined') || confirmId != null || confirmId != '')) {
                            var confirmValue = $.trim($("#" + confirmId).val());
                            if (confirmValue != valueInput) {
                                returnValue = false;
                                $("#" + id).after("<span class='validation-message error-msg'>" + expressionMsg + "</span>");
                            }
                        }
                    }

                    if (validationType == 'combo') {
                        debugger;
                        var comboBox = $("#" + id).data("kendoComboBox");
                        if ((comboBox.selectedIndex === -1 && comboBox.value()) || comboBox.value() == "") {
                            returnValue = false;
                            $("#" + id).after("<span class='validation-message error-msg'>" + expressionMsg + "</span>");
                        }
                    }
                }
            }
        });

        return returnValue;
    };


});


function PasswordErrorMessage(valueInput) {
    var msg = null;
    if (!lowerCaseRegEx.test(valueInput)) {
        msg = "Must have at least one lowercase letter";
    }
    else if (!upperCaseRegEx.test(valueInput)) {
        msg = "Must have at least one uppercase letter";
    }
    else if (!specialOneRegEx.test(valueInput)) {
        msg = "Must have at least one symbol";
    }
    else if (!numberOneRegEx.test(valueInput)) {
        msg = "Must have at least one number";
    }

    return msg;

}





function ShowAjaxNotificationMessage(header, message, messageType, dWidh) {
    var mWidth = "300px";
    if (message != "") {
        if (messageType && messageType != "") {
            if (messageType == "warning") {
                $(".dummy_ntm_text").html("<span style='color: red; font-size: 16px;'>" + message + "</span>");
            }
            if (messageType == "success") {
                $(".dummy_ntm_text").html("<span style='color: green; font-size: 16px;'>" + message + "</span>");
            }

        } else {
            $(".dummy_ntm_text").html("<span style='color: #333; font-size: 16px;'>" + message + "</span>");
        }
    }

    if (dWidh) {
        mWidth = dWidh;
    }
    var messaegWindow = $("#dummy_ajax_message"), messageUndo = $("#undo");

    messageUndo.click(function () {
        messaegWindow.data("kendoWindow").open();
        messageUndo.fadeOut();
    });

    messaegWindow.kendoWindow({
        width: mWidth,
        height: "auto",
        title: header,
        visible: false,
        open: onOpenNotificationWindow,
        close: onClosenNotificationForChildWindow,
        deactivate: onClosenNotificationForChildWindow,
        actions: [
            "Close"
        ]
    }).data("kendoWindow").center().open();
}

function onOpenNotificationWindow(e) {
    $(".disable-body").show();
    $(".page-header-fixed .page-container").css({ "margin-top": "45px" });
}

function onClosenNotificationForChildWindow(e) {
    $("#undo").fadeIn();
    $(".disable-body").hide();
}

function onClosenNotificationWindow(e) {
    $("#undo").fadeIn();
    $(".disable-body").hide();
}

function ClosenNotificationWindowFromButton() {
    $(".k-button.k-bare.k-button-icon.k-window-action").click();
}