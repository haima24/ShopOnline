$(function () {
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
});