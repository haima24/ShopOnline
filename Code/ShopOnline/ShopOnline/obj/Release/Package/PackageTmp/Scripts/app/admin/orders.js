$(function () {
    //new
    var table = $('#orders-new-datatable').DataTable({
        "order": [[5, 'desc'], [4, 'desc']],
        "oLanguage": oLanguage,
        reponsive: true,
        ajax: '/Admin/OrdersNew',
        "columns": [
             {
                 "data": "OrderId",
                 "render": function (id) {
                     return id;
                 }
             },
             {
                 "data": "OrderId",
                 "render": function (id) {
                     return '<button type="button" order-id=' + id + ' class="btn btn-success btn-view-order"> <i class="fa fa-search"></i> Chi Tiết</button>';
                 }
             },
             {
                 "data": "ShippingAddress",
                 "render": function (address) {
                     return address;
                 }
             },
            {
                "data": "ShippingTelephone",
                "render": function (phone) {
                    return phone;
                }
            },
             {
                 "data": "CreatedDate",
                 "render": function (date, o, obj) {
                     return $.formatDate(date);
                 }
             },
             {
                 "data": "UpdatedDate",
                 "render": function (date, o, obj) {
                     return $.formatDate(date);
                 }
             },
             {
                 "data": "OrderId",
                 "render": function (id) {
                     return '<button type="button" order-id=' + id + ' class="btn btn-primary btn-process-order"> <i class="fa fa-random"></i> Xử Lý</button>' +
                          '  <button type="button" order-id="' + id + '" class="btn btn-danger btn-disable-order"> <i class="fa fa-remove"></i> Hủy</button>';
                 }
             }
        ],
        drawCallback: function () {
            $(".btn-view-order").on('click', function () {
                var orderId = $(this).attr('order-id');
                $.post('/Admin/ViewOrderDetails', { orderId: orderId }, function (data) {
                    if (data.result) {
                        var $html = $(data.html);
                        $html.modal('show');
                    }
                });
            });
            $('.btn-process-order').on('click', function () {
                var orderId = $(this).attr('order-id');
                $.post('/Admin/OrderToProcessing', { orderId: orderId }, function (data) {
                    if (data.result) {
                        alert('Đã chuyển qua Xử Lý');
                        table.ajax.reload();
                    }
                });
            });
            $('.btn-disable-order').on('click', function () {
                var orderId = $(this).attr('order-id');
                $.post('/Admin/OrderToDisabled', { orderId: orderId }, function (data) {
                    if (data.result) {
                        alert('Đã hủy đơn hàng');
                        table.ajax.reload();
                    }
                });
            });
        }
    });
    ///processing
    var tableProcessing = $('#orders-processing-datatable').DataTable({
        "order": [[5, 'desc'], [4, 'desc']],
        "oLanguage": oLanguage,
        reponsive: true,
        ajax: '/Admin/OrdersProcessing',
        "columns": [
             {
                 "data": "OrderId",
                 "render": function (id) {
                     return id;
                 }
             },
             {
                 "data": "OrderId",
                 "render": function (id) {
                     return '<button type="button" order-id=' + id + ' class="btn btn-success btn-view-order"> <i class="fa fa-search"></i> Chi Tiết</button>';
                 }
             },
             {
                 "data": "ShippingAddress",
                 "render": function (address) {
                     return address;
                 }
             },
            {
                "data": "ShippingTelephone",
                "render": function (phone) {
                    return phone;
                }
            },
             {
                 "data": "CreatedDate",
                 "render": function (date, o, obj) {
                     return $.formatDate(date);
                 }
             },
             {
                 "data": "UpdatedDate",
                 "render": function (date, o, obj) {
                     return $.formatDate(date);
                 }
             },
             {
                 "data": "OrderId",
                 "render": function (id) {
                     return '<button type="button" order-id=' + id + ' class="btn btn-primary btn-process-order"> <i class="fa fa-random"></i> Đã Xử Lý</button>' +
                          '  <button type="button" order-id="' + id + '" class="btn btn-danger btn-disable-order"> <i class="fa fa-remove"></i> Hủy</button>';
                 }
             }
        ],
        drawCallback: function () {
            $(".btn-view-order").on('click', function () {
                var orderId = $(this).attr('order-id');
                $.post('/Admin/ViewOrderDetails', { orderId: orderId }, function (data) {
                    if (data.result) {
                        var $html = $(data.html);
                        $html.modal('show');
                    }
                });
            });
            $('.btn-process-order').on('click', function () {
                var orderId = $(this).attr('order-id');
                $.post('/Admin/OrderToProcessed', { orderId: orderId }, function (data) {
                    if (data.result) {
                        alert('Đã chuyển qua Đã Xử Lý');
                        tableProcessing.ajax.reload();
                    }
                });
            });
            $('.btn-disable-order').on('click', function () {
                var orderId = $(this).attr('order-id');
                $.post('/Admin/OrderToDisabled', { orderId: orderId }, function (data) {
                    if (data.result) {
                        alert('Đã hủy đơn hàng');
                        tableProcessing.ajax.reload();
                    }
                });
            });
        }
    });
    //processed
    var tableProcessed = $('#orders-processed-datatable').DataTable({
        "order": [[5, 'desc'], [4, 'desc']],
        "oLanguage": oLanguage,
        reponsive: true,
        ajax: '/Admin/OrdersProcessed',
        "columns": [
             {
                 "data": "OrderId",
                 "render": function (id) {
                     return id;
                 }
             },
             {
                 "data": "OrderId",
                 "render": function (id) {
                     return '<button type="button" order-id=' + id + ' class="btn btn-success btn-view-order"> <i class="fa fa-search"></i> Chi Tiết</button>';
                 }
             },
             {
                 "data": "ShippingAddress",
                 "render": function (address) {
                     return address;
                 }
             },
            {
                "data": "ShippingTelephone",
                "render": function (phone) {
                    return phone;
                }
            },
             {
                 "data": "CreatedDate",
                 "render": function (date, o, obj) {
                     return $.formatDate(date);
                 }
             },
             {
                 "data": "UpdatedDate",
                 "render": function (date, o, obj) {
                     return $.formatDate(date);
                 }
             }
        ],
        drawCallback: function () {
            $(".btn-view-order").on('click', function () {
                var orderId = $(this).attr('order-id');
                $.post('/Admin/ViewOrderDetails', { orderId: orderId }, function (data) {
                    if (data.result) {
                        var $html = $(data.html);
                        $html.modal('show');
                    }
                });
            });
        }
    });
    
    //disable
    var tableDisabled = $('#orders-disabled-datatable').DataTable({
        "order": [[5, 'desc'], [4, 'desc']],
        "oLanguage": oLanguage,
        reponsive: true,
        ajax: '/Admin/OrdersDisabled',
        "columns": [
             {
                 "data": "OrderId",
                 "render": function (id) {
                     return id;
                 }
             },
             {
                 "data": "OrderId",
                 "render": function (id) {
                     return '<button type="button" order-id=' + id + ' class="btn btn-success btn-view-order"> <i class="fa fa-search"></i> Chi Tiết</button>';
                 }
             },
             {
                 "data": "ShippingAddress",
                 "render": function (address) {
                     return address;
                 }
             },
            {
                "data": "ShippingTelephone",
                "render": function (phone) {
                    return phone;
                }
            },
             {
                 "data": "CreatedDate",
                 "render": function (date, o, obj) {
                     return $.formatDate(date);
                 }
             },
             {
                 "data": "UpdatedDate",
                 "render": function (date, o, obj) {
                     return $.formatDate(date);
                 }
             },
             {
                 "data": "OrderId",
                 "render": function (id) {
                     return '<button type="button" order-id=' + id + ' class="btn btn-primary btn-renew-order"> <i class="fa fa-refresh"></i> Gửi lại đơn hàng</button>';
                 }
             }
            
        ],
        drawCallback: function () {
            $(".btn-view-order").on('click', function () {
                var orderId = $(this).attr('order-id');
                $.post('/Admin/ViewOrderDetails', { orderId: orderId }, function (data) {
                    if (data.result) {
                        var $html = $(data.html);
                        $html.modal('show');
                    }
                });
            });
            $('.btn-renew-order').on('click', function () {
                var orderId = $(this).attr('order-id');
                $.post('/Admin/OrderToNew', { orderId: orderId }, function (data) {
                    if (data.result) {
                        alert('Đã gửi lại đơn hàng');
                        tableDisabled.ajax.reload();
                    }
                });
            });
        }
    });
});