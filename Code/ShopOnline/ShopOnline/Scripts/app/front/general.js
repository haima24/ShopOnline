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
});