$(function () {
    $("#sign-up-form").validate({
        rules: {
            signUpUserName: "required",
            signUpPassWord: "required",
            signUpEmail:"email"
        },
        messages: {
            signUpUserName: "Vui lòng nhập tên tài khoản",
            signUpPassWord: "Vui lòng nhập mật khẩu",
            signUpEmail:"Địa chỉ email không hợp lệ"
        }
    });
    $('#sign-in-form').validate({
        rules: {
            signInUsername: "required",
            signInPassword: "required"
        },
        messages: {
            signUpUserName: "Vui lòng nhập tên tài khoản",
            signUpPassWord: "Vui lòng nhập mật khẩu"
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
                    if (data.dictricts) {
                        $('#district-signup').append($('<option>'));
                        $.each(data.dictricts, function(i, o) {
                            $('#district-signup').append($('<option value=' + o.LocationId + '>' + o.LocationName + '</option>'));
                        });
                    }
                }
            });
        }
       
    });
});