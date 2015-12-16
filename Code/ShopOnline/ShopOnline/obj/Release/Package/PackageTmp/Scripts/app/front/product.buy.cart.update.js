$(function() {
    $('#form-checkout-step1 .quantity').on('change', function () {
        var line = $(this);
        var productId = line.attr('product-id');
        var quantity = line.val();
        $.post('/Cart/UpdateQuantity', { productId: productId, quantity: quantity }, function (data) {
            if (data.result) {
                line.closest('tr').find('.line-total').html(data.totalLine);
                $('#tableTotal').html(data.totalAll);
                $('#cart-counter').html('(' + data.itemsCount + ')');
            }
        });
    });
    $('#form-checkout-step1 .remove-line-cart').on('click', function () {
        var line = $(this);
        var productId = line.attr('product-id');
        $.post('/Cart/RemoveLineCart', { productId: productId }, function (data) {
            if (data.result) {
                line.closest('tr').remove();
                $('#tableTotal').html(data.totalAll);
                $('#cart-counter').html('(' + data.itemsCount + ')');
            }
        });
    });
});