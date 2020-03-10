
var TypeMessage = {
    Success: 1,
    Error: 2
};

var TypeConfirm = {
    Warning: 1,
    Danger: 2
};

var MethodType = {
    Get: "GET",
    Post: "POST"
};

var mydnn = (function () {
    var myconfig = {
        activePageUrl: $("#dnn-active-page-full-url").val(),
        moduleId: $("#dnn-module-id").val(),
        moduleTabId: $("#dnn-module-tab-id").val()
    };



    //var formPreventDefault = function () {
    //    $(document).on("keypress", function (e) {
    //        if (e.which == 13) {
    //            $("#Form").on("submit", function (e) {
    //                e.preventDefault();
    //            });
    //        }
    //    });
    //}

    return {
        myconfig: myconfig//,
        //   formPreventDefault: formPreventDefault
    };
});

function ShowMessage(msgText, type) {

    if (type === TypeMessage.Success) {
        $.confirm({
            title: 'عملیات موفق',
            content: msgText,
            useBootstrap: false,
            type: 'green',
            typeAnimated: true,
            buttons: {

                close: {
                    text: 'بستن',
                    btnClass: 'btn-blue',
                    action: function () {
                    }
                }

            }
        });
    }
    else if (type === TypeMessage.Error) {
        $.confirm({
            title: 'عملیات ناموفق',
            content: msgText,
            useBootstrap: false,
            type: 'red',
            typeAnimated: true,
            buttons: {

                close: {
                    text: 'بستن',
                    btnClass: 'btn-red',
                    action: function () {
                    }
                }
            }
        });
    }
}

function ShowConfirm(msgText, type) {
    if (type === TypeConfirm.Danger) {
        $.confirm({
            title: 'اخطار',
            content: msgText,
            type: 'red',
            typeAnimated: true,
            buttons: {
                //tryAgain: {
                //    text: 'Try again',
                //    btnClass: 'btn-red',
                //    action: function () {
                //    }
                //},
                ok: {
                    text: 'حذف',
                    btnClass: 'btn-red',
                    action: function () {


                        return true;
                    }
                },
                close: {
                    text: 'بستن',
                    btnClass: 'btn-blue',
                    action: function () {

                        //return false;
                    }
                }


            }
        });
    }

}

function closeWindows(selector) {

    return $(selector).closest(".k-window-content").data("kendoWindow").close();
}

function KendoComboBind(inputName, controller, action) {

    $(inputName).kendoComboBox({
        dataTextField: "text",
        dataValueField: "value",
        filter: "contains",
        suggest: true,
        minLength: 1,
        //delay:100,
        dataSource: {
            serverFiltering: true,
            transport: {
                read: {
                    url: "/" + controller + "/" + action + "/",
                    type: "POST",
                    contentType: "application/json",
                    data: function () {
                        return {
                            text: $(inputName).data('kendoComboBox').input.val()
                        };
                    }
                },
                parameterMap: function (data, operation) { return JSON.stringify(data); }
            }
        }
    });

}
function SendAndUpdate(formSelector) {
    debugger;

    var ajaxConfig = {

        type: $(formSelector).attr("method"), //'get',
        url: $(formSelector).attr("action"),

        success: function (data) {

            if (data.MessageType === TypeMessage.Success) {
                ShowMessage(data.Message, TypeMessage.Success);
            }
            else {
                ShowMessage(data.Message, TypeMessage.Error);
            }

            if (data.Script) {
                eval(data.Script);
            }

        },
        error: function (data) {
            debugger;
            ShowMessage("خطایی رخ داده است", TypeMessage.error);
            console.error('An error occurred.');
            console.error(data);

        }
    }

    //if (methodType === MethodType.Get) {
    //    ajaxConfig["data"] = params;
    //}

    
        var dataToSend = $(formSelector).get(0);
        ajaxConfig["data"] = new FormData(dataToSend);
    


    ajaxConfig["contentType"] = false;
    ajaxConfig["processData"] = false;

    if ($(formSelector).attr('enctype') == "multipart/form-data") {
        ajaxConfig["contentType"] = false;

    }



    $.ajax(ajaxConfig);

}
function myGetJson(controllerName, actionName, params, formNameInPost, methodType) {

    var result;
    var ajaxConfig = {
        type: methodType,//'get',
        async: false,
        url: "/DesktopModules/MVC/Notary/" + controllerName + "/" + actionName,

        headers: {
            "ModuleId": $("#dnn-module-id").val(),
            "TabId": $("#dnn-module-tab-id").val(),
            "RequestVerificationToken": $("input[name='__RequestVerificationToken']").val()
        },
        success: function (data1) {

            // return data1;
            result = data1;
        },
        error: function (data) {
            ShowMessage("خطایی رخ داده است", TypeMessage.error);
            //console.log('An error occurred.');
            //console.log(data);
            console.error('An error occurred.');
            console.error(data);
        }
    }
    if (methodType === MethodType.Get) {
        ajaxConfig["data"] = params;
    }
    else {//POST
        if (formNameInPost) {

            var formin = $(formNameInPost).get(0);
            ajaxConfig["contentType"] = false;
            ajaxConfig["processData"] = false;
            debugger;
            if (params) {

                ajaxConfig["data"] = new FormData(formin) + "&" + params;
            }
            else {
                ajaxConfig["data"] = new FormData(formin);
            }
            if ($(formNameInPost).attr('enctype') == "multipart/form-data") {
                ajaxConfig["contentType"] = false;

            }
        }
        else {
            ajaxConfig["data"] = params;
        }
    }
    $.ajax(ajaxConfig);
    return result;
}
//function refreshAddNewTab(resetUrl) {
//    $.ajax({
//        type: 'GET',
//        url: resetUrl,
//        success: function (response) {
//            ShowMessage(response.Text, TypeMessage.Success);
//            //$("#secondTab").html(response);
//            //$('ul.nav.nav-tabs a:eq(1)').html('Add New');
//            //if (showViewTab)
//            //    $('ul.nav.nav-tabs a:eq(0)').tab('show');
//        },
//        error: function (response) {


//            ShowMessage(response.Text, TypeMessage.error);
//        },
//        headers: {
//            "ModuleId": @Dnn.ModuleContext.ModuleId,

//    "TabId": @Dnn.ModuleContext.TabId,

//    "RequestVerificationToken": $("input[name='__RequestVerificationToken']").val()

//}

//    });
//}

//######################################## KendoFunction ####################################
//function minKendovalidate(input) {
//    var minAttr = $(input).attr('min');


//    if (minAttr != null) {
//        if (input.val().length < minAttr) {
//            return false;
//        }
//        else {
//            return true;
//        }
//    }

//    return true;
//}

//function maxKendovalidate(input) {
//    var maxAttr = $(input).attr('max');


//    if (maxAttr != null) {
//        if (input.val().length > maxAttr) {
//            return false;
//        }
//        else {
//            return true;
//        }
//    }

//    return true;
//}

//function IsEnglishInput(input) {
//    var IsenglishInput = $(input).attr('isenglish');
//    if (IsenglishInput)
//    {
//        debugger;
//        var textinput = $(input).val();
//        var re = new RegExp("^([a-zA-Z0-9]{5,})$");
//        if (re.test(textinput)) {
//            return true;
//        }
//        else
//        {
//            return false;
//        }
//        //for (var i = 0; i < textinput.length; i++) {
//        //    if( !textinput.charAt(i).key.match(/[a-zA-Z0-9]/))
//        //    {
//        //        return false;
//        //    }
//        //}
//        return true;
//    }
//    return true;
//}

//function GetGlobalpopupFormValidator(popupName) {
//    var popupFormValidator = $(popupName).kendoValidator({
//        rules: {
//            min: function (input) { return minKendovalidate(input); },
//            max: function (input) { return maxKendovalidate(input); },
//            isenglish: function (input) { return IsEnglishInput(input); }
//        }

//    }).data("kendoValidator");
//    return popupFormValidator;
//}

//function GlobalFormIsValid()
//{
//    var Globvalidator= $("#Form").kendoValidator({
//        rules: {
//            min: function (input) { return minKendovalidate(input); },
//            max: function (input) { return maxKendovalidate(input); },
//            isenglish: function (input) { return IsEnglishInput(input); }
//        }

//    }).data("kendoValidator");
//    return Globvalidator.validate();
//}

//function ConvertDateMiladitoPersian(datein) {
//    var ff = moment(datein).format('jYYYY/jMM/jDD');
//    return ff;
//}

//$(document).ready(function () {
//    $("#Form").kendoValidator({
//        rules: {
//            min: function (input) { return minKendovalidate(input); },
//            max: function (input) { return maxKendovalidate(input); },
//            isenglish: function (input) { return IsEnglishInput(input); }
//        }

//    }).data("kendoValidator");


//});
