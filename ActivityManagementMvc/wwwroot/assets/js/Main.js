$(function() {
    var placeholder = $("#modal-placeholder");
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

    placeholder.on("click",
        "button[data-save='modal']",
        function () {
            showLoading();
            var form = $(this).parents(".modal").find("form");
            var formUrl = form.attr("action");
            debugger;

            var dataToSend = new FormData(form.get(0));
            $.ajax({
                url: formUrl,
                data: dataToSend,
                type: "post",
                processData: false,
                contentType: false,

                error: function () {showSweetErrorAlert()}

            }).done(function (result) {

                var newBody = $(".modal-body", result);
                var newFooter = $(".modal-footer", result);
                placeholder.find(".modal-body").replaceWith(newBody);
                placeholder.find(".modal-footer").replaceWith(newFooter);

                var isValid = newBody.find("input [name=IsValid]").val === "True";
                if (isValid) {
                    $.ajax({ url: '/Base/Notification', error: function () { showSweetErrorAlert(); } }).done(function (notification) {
                        showSweetSuccessAlert(notification);
                       
                        $table.bootstrapTable('refresh');
                        placeholder.find(".modal").modal('hide');
                    });
                }
            });
            hideLoading();
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