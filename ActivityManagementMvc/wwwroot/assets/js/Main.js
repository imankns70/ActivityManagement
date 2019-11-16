$(function() {
    var placeholder = $("#modal-placeholder");
    //get create and edit 
    $(document).on('click',
        ' button[data-toggle="ajax-modal"]',
        function () {
           
            var url = $(this).data('url');
            $.ajax({
                url: url,
                beforeSend: function() { showLoading()},
                complete: function () { hideLoading()},
                error: function() { showSweetErrorAlert()}

            }).done(function(result) {
                placeholder.html(result);
                placeholder.find(".modal").modal("show");
            });
        });

    // post create and edit 
    placeholder.on("click",
        "button[data-save='modal']",
        function () {
            showLoading();
            var form = $(this).parents(".modal").find("form");
            var formUrl = form.attr("action");
        

            var dataToSend = new FormData(form.get(0));
            $.ajax({
                url: formUrl,
                data: dataToSend,
                type: "post",
                processData: false,
                contentType: false,
                beforeSend: function () { showLoading() },
                complete: function () { hideLoading() },
                error: function () {showSweetErrorAlert()}

            }).done(function (data) {
                 
                var newBody = $(".modal-body", data);
                var newFooter = $(".modal-footer", data);
                placeholder.find(".modal-body").replaceWith(newBody);
                placeholder.find(".modal-footer").replaceWith(newFooter);
                debugger;
                var isValid = newBody.find("input[name='IsValid']").val() === "True";
                
                if (isValid) {
                    $.ajax({ url: '/Base/Notification', error: function () { showSweetErrorAlert(); } }).done(function (notification) {
                        debugger;
                        showSweetSuccessAlert(notification);
                       
                        $table.bootstrapTable('refresh');
                        placeholder.find(".modal").modal('hide');
                    });
                }
            });
           
        });

    function showLoading() {
        $("body").preloader({ text: 'لطفا صبر کنید ...' });

    } function hideLoading() {
        $("body").preloader('remove');
    }

    function showSweetSuccessAlert(message) {
        Swal.fire({
            position: 'top-middle',
            type: 'success',
            title: message,
            confirmButtonText: 'بستن',
        });
    }
    function showSweetErrorAlert() {
        Swal.fire({
            type: 'error',
            title: 'خطایی رخ داده است !!!',
            text: 'لطفا تا برطرف شدن خطا شکیبا باشید.',
            confirmButtonText: 'بستن'
        });
    }
});