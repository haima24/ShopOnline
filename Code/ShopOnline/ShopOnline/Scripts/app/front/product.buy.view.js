$(function () {
    $('#category-menu .category-sub').on('click', function (e) {
        e.preventDefault();
        var categoryId = $(this).attr('child-category-id');
        window.location.href = "/ProductList/Index?FilterCategoryId=" + categoryId;
    });
    $('#category-all').on('click', function (e) {
        e.preventDefault();
        window.location.href = "/ProductList/Index";
    });
    $('#btn-add-to-cart').on('click', function (e) {
        e.preventDefault();
        var productId = $(this).attr('product-id');
        $.post('/Cart/AddToCart', { productId: productId }, function (data) {
            if (data.result) {
                var itemImg = $("#mainImage");
                flyToElement(itemImg, $('#cart-anchor'));
                $('#cart-counter').html("(" + data.currentCount + ")");
            }
        });
    });
    $('#city').on('change', function () {
        var locationId = parseInt($(this).val());
        if (isNaN(locationId)) {
            locationId = 0;
            $('#district').attr('disabled', 'disabled');
            $('#district').val('');
        } else {

            $.post('/Contact/GetDistricts', { locationId: locationId }, function (data) {
                if (data.result) {
                    $('#district').removeAttr('disabled');
                    $('#district').empty();
                    if (data.dictricts) {
                        $('#district').append($('<option>'));
                        $.each(data.dictricts, function (i, o) {
                            $('#district').append($('<option value=' + o.LocationId + '>' + o.LocationName + '</option>'));
                        });
                    }
                }
            });
        }

    });
    $('#form-checkout-step1').validate({
        rules: {
            name: "required",
            telephone: "required",
            street: "required"
        },
        messages: {
            name: "Vui lòng nhập tên",
            telephone: "Vui lòng nhập số điện thoại",
            street: "Vui lòng nhập địa chỉ"
        }
    });
    $('#form-checkout-step2').validate({
        rules: {
            delivery: "required"
        },
        messages: {
            delivery: "Vui lòng chọn phương thức giao hàng"
        }
    });
    $('#form-checkout-step3').validate({
        rules: {
            payment: "required"
        },
        messages: {
            payment: "Vui lòng chọn phương thức thanh toán"
        }
    });
});