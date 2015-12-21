$(function () {
    $('#comments .btn-reply-post').on('click', function (e) {
        var comment = $(this).closest('.comment');
        var replyContainer = comment.find('.reply-container');
        var textObj = replyContainer.find('.message-reply');
        var text = textObj.val();
        var parentId = $(this).attr('parent-id');
        $.post('/Comment/Reply', {parentId:parentId, text: text }, function (data) {
            if(data.result) {
                replyContainer.slideToggle("slow", function () {
                    comment.find('.replys').prepend(data.html);
                    textObj.val('');
                });
            }
        });
      
    });
    $('#comments .btn-reply').on('click', function(e) {
        var replyContainer = $(this).closest('.comment').find('.reply-container');
        replyContainer.slideToggle("slow");
    });
    $('#comments .btn-reply-cancel').on('click', function (e) {
        var replyContainer = $(this).closest('.comment').find('.reply-container');
        replyContainer.slideToggle("slow", function () {
            replyContainer.find('.message-comment').val('');
        });
    });
    $('#btn-post-comment').on('click', function(e) {
        e.preventDefault();
        var btnPost = $(this);
        var commentContainer = $('#comments-container');
        var form = btnPost.closest('form');
        if (form.valid()) {
            var commentDetail = $('#message-comment').val();
            var productId = btnPost.closest('#comments').attr('product-id');
            $.post('/Comment/CreateComment', { detail: commentDetail, productId: productId }, function (data) {
                if (data.result) {
                    commentContainer.prepend(data.html);
                    $('#comment-count').html(data.commentCount);
                    commentDetail.val('');
                }
            });
        }
       
    });
    $('#btn-filter-brand').on('click', function (e) {
        e.preventDefault();
        var brandIds = '';
        $('#form-filter-brand .filter-brand:checked').each(function (i,o) {
            if (i == 0) {
                brandIds += 'FilterBrandIds='+$(this).attr('brand-id');
            } else {
                brandIds += "&FilterBrandIds=" + $(this).attr('brand-id');
            }

        });
        window.location.href = "/ProductList/Index?" + brandIds;
       
    });
    $('#btn-filter-color').on('click', function (e) {
        e.preventDefault();
        var colorIds = '';
        $('#form-filter-color .filter-color:checked').each(function (i, o) {
            if (i == 0) {
                colorIds += 'FilterColorIds='+$(this).attr('color-id');
            } else {
                colorIds += "&FilterColorIds=" + $(this).attr('color-id');
            }

        });
        window.location.href = "/ProductList/Index?" +  colorIds ;
       
    });
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
                    if (data.dictrictsModel) {
                        $('#district').append($('<option>'));
                        $.each(data.dictrictsModel, function (i, o) {
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