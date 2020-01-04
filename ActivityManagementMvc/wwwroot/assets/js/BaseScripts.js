
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


$.ajaxSetup({
    data: {
        ModuleId: $("#dnn-module-id").val(),
        TabId: $("#dnn-module-tab-id").val(),
        RequestVerificationToken: $("input[name='__RequestVerificationToken']").val()
    }
});


function ShowMessage(msgText, Type) {
    if (Type === TypeMessage.Success) {
        $.confirm({
            title: 'عملیات موفق',
            content: msgText,
            type: 'green',
            typeAnimated: true,
            buttons: {
                //tryAgain: {
                //    text: 'Try again',
                //    btnClass: 'btn-red',
                //    action: function () {
                //    }
                //},
                close: {
                    text: 'بستن',
                    btnClass: 'btn-blue',
                    action: function () {
                    }
                }
                ,

                //close: function () {
                //}
            }
        });
    }
    else if (Type === TypeMessage.Error) {
        $.confirm({
            title: 'عملیات ناموفق',
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

function ShowConfirm(msgText, Type, funcToExec) {
    if (Type === TypeConfirm.Danger) {
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
                        debugger;
                        funcToExec();
                        return true;
                    }
                },
                close: {
                    text: 'بستن',
                    btnClass: 'btn-blue',
                    action: function () {
                        debugger;
                        //return false;
                    }
                }
                ,

                //close: function () {
                //}
            }
        });
    }

}
function SendAndUpdate(controllerName, actionName, params, formNameInPost, methodType, updateElementId) {


    var ajaxConfig = {
        type: methodType,//'get',

        url: "/ + controllerName + "/" + actionName",

      
        success: function (data) {

            if (data.Text) {
                ShowMessage(data.Text, data.TypeMessage);
            }
            if (updateElementId) {
                if (data.TypeMessage === TypeMessage.Success) {

                    $(updateElementId).html(data.Html);
                }
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

    if (methodType === MethodType.Get) {
        ajaxConfig["data"] = params;
    }
    else {//POST
        if (formNameInPost) {

            var formin = $(formNameInPost).get(0);
            ajaxConfig["contentType"] = false;
            ajaxConfig["processData"] = false;
            
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
function minKendovalidate(input) {
    var minAttr = $(input).attr('min');


    if (minAttr != null) {
        if (input.val().length < minAttr) {
            return false;
        }
        else {
            return true;
        }
    }

    return true;
}
function maxKendovalidate(input) {
    var maxAttr = $(input).attr('max');


    if (maxAttr != null) {
        if (input.val().length > maxAttr) {
            return false;
        }
        else {
            return true;
        }
    }

    return true;
}
function IsEnglishInput(input) {
    var IsenglishInput = $(input).attr('isenglish');
    if (IsenglishInput)
    {
        debugger;
        var textinput = $(input).val();
        var re = new RegExp("^([a-zA-Z0-9]{5,})$");
        if (re.test(textinput)) {
            return true;
        }
        else
        {
            return false;
        }
        //for (var i = 0; i < textinput.length; i++) {
        //    if( !textinput.charAt(i).key.match(/[a-zA-Z0-9]/))
        //    {
        //        return false;
        //    }
        //}
        return true;
    }
    return true;
}
function GetGlobalpopupFormValidator(popupName) {
    var popupFormValidator = $(popupName).kendoValidator({
        rules: {
            min: function (input) { return minKendovalidate(input); },
            max: function (input) { return maxKendovalidate(input); },
            isenglish: function (input) { return IsEnglishInput(input); }
        }

    }).data("kendoValidator");
    return popupFormValidator;
}
function GlobalFormIsValid()
{
    var Globvalidator= $("#Form").kendoValidator({
        rules: {
            min: function (input) { return minKendovalidate(input); },
            max: function (input) { return maxKendovalidate(input); },
            isenglish: function (input) { return IsEnglishInput(input); }
        }

    }).data("kendoValidator");
    return Globvalidator.validate();
}
function ConvertDateMiladitoPersian(datein) {
    var ff = moment(datein).format('jYYYY/jMM/jDD');
    return ff;
}

$(document).ready(function () {
    $("#Form").kendoValidator({
        rules: {
            min: function (input) { return minKendovalidate(input); },
            max: function (input) { return maxKendovalidate(input); },
            isenglish: function (input) { return IsEnglishInput(input); }
        }

    }).data("kendoValidator");

    //$("input").each(function () {
    //    var cb = $(this).data("kendoDropDownList");
    //    if (cb) {
    //        // attach handler to cb
    //        cb.bind("open", handler)
    //    }
    //});
});
