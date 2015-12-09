$(function () {
    Dropzone.autoDiscover = false;
    $("#logoDropzone").dropzone({
        url: "/admin/ChangeLogo",
        dictDefaultMessage: '',
        thumbnailWidth: 250,
        thumbnailHeight: 100
    });
    $("#configContact").summernote();
    $('#btnSaveContact').on('click', function () {
        var code = $('#configContact').code();
        $.post('/Admin/SaveConfigContact', { value: code }, function (data) {
            if (data.result) {
                alert("Đã Lưu Thành Công");
            }

        });
    });
    $("#configAddress").summernote();
    $('#btnSaveAddress').on('click', function () {
        var code = $('#configAddress').code();
        $.post('/Admin/SaveConfigAddress', { value: code }, function (data) {
            if (data.result) {
                alert("Đã Lưu Thành Công");
            }

        });
    });
    $('#btnSavePhone').on('click', function () {
        var code = $('#configPhone').val();
        $.post('/Admin/SaveConfigPhone', { value: code }, function (data) {
            if (data.result) {
                alert("Đã Lưu Thành Công");
            }

        });
    });
    $('#btnSaveEmail').on('click', function () {
        var code = $('#configEmail').val();
        $.post('/Admin/SaveConfigEmail', { value: code }, function (data) {
            if (data.result) {
                alert("Đã Lưu Thành Công");
            }

        });
    });
    $('#btnSaveIntro').on('click', function () {
        var code = $('#configIntro').val();
        $.post('/Admin/SaveConfigIntro', { value: code }, function (data) {
            if (data.result) {
                alert("Đã Lưu Thành Công");
            }

        });
    });
    $('#btnSaveEmployee').on('click', function () {
        var e1 = $('#configEmployee1').val();
        var e2 = $('#configEmployee2').val();
        var e3 = $('#configEmployee3').val();
        $.post('/Admin/SaveConfigEmployee', { employee1: e1,employee2:e2,employee3:e3 }, function (data) {
            if (data.result) {
                alert("Đã Lưu Thành Công");
            }

        });
    });
});