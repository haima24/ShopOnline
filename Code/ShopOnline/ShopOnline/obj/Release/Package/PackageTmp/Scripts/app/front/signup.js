$(function () {
    $("#sign-up-form").validate({
        rules: {
            signUpUserName: "required",
            signUpPassWord: "required",
            signUpEmail: "email",
            signUpPassWordConfirm: {
                equalTo: "#signUpPassWord"
            }
        },
        messages: {
            signUpUserName: "Vui lòng nhập tên tài khoản",
            signUpPassWord: "Vui lòng nhập mật khẩu",
            signUpEmail: "Địa chỉ email không hợp lệ",
            signUpPassWordConfirm:"Mật Khẩu xác nhận không trùng khớp"
        }
    });
    $('#sign-in-form').validate({
        rules: {
            signInUsername: "required",
            signInPassword: "required"
        },
        messages: {
            signInUsername: "Vui lòng nhập tên tài khoản",
            signInPassword: "Vui lòng nhập mật khẩu"
        }
    });
    $('#city-signup').on('change', function () {
        var locationId = parseInt($(this).val());
        if (isNaN(locationId)) {
            locationId = 0;
            $('#district-signup').attr('disabled', 'disabled');
            $('#district-signup').val('');
        }else {

            $.post('/Contact/GetDistricts', { locationId: locationId }, function(data) {
                if (data.result) {
                    $('#district-signup').removeAttr('disabled');
                    $('#district-signup').empty();
                    if (data.dictrictsModel) {
                        $('#district-signup').append($('<option>'));
                        $.each(data.dictrictsModel, function (i, o) {
                            $('#district-signup').append($('<option value=' + o.LocationId + '>' + o.LocationName + '</option>'));
                        });
                    }
                }
            });
        }
       
    });
});