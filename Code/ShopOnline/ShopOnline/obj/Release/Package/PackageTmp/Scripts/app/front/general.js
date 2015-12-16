$(function () {
    $('#btn-login-ajax').on('click', function () {
        var name = $('#login-modal #name_modal').val();
        var pass = $('#login-modal #password_modal').val();
        $.post('/Contact/LoginAjax', { signInUserName: name, signInPassword: pass }, function(data) {
            if (data.result) {
                window.location.href = "/ProductList/Index";
            } else {
                alert('Sai tên đăng nhập hoặc mật khẩu');
            }
        });
    });
    $('#btn-subscribe').on('click', function () {
        alert('Chức năng đang được xây dựng');
    });
    $('#btn-change-pass').on('click', function () {
        var form = $(this).closest('form');
        if (form.valid()) {
            var oldPass = $('#changeOldPass').val();
            var newPass = $('#changeNewPass').val();
            $.post('/Contact/ChangePassword', { oldPass: oldPass, newPass: newPass }, function (data) {
                if (data.result) {
                    alert('Đổi Mật Khẩu Thành Công');
                    $('#user-changepass-modal').modal('hide');
                } else {
                    alert('Mật Khẩu Cũ Không Chính Xác');
                }
            });
        }
    });
});