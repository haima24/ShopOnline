$(function () {
    var table = $('#orders-history-table').DataTable({
        "order": [[5, 'desc'], [4, 'desc']],
        "oLanguage": oLanguage,
        reponsive: true,
        ajax: '/Contact/OrderHistoryList',
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
                 "data": "OrderStatus",
                 "render": function (status) {
                     var label = '';
                     switch (status) {
                         case 0:
                             label = '<button type="button" class="btn btn-outline btn-success">Mới</button>';
                             break;
                         case 1:
                             label = '<button type="button" class="btn btn-outline btn-primary">Đang Xử Lý</button>';
                             break;
                         case 2:
                             label = '<button type="button" class="btn btn-outline btn-info">Đã Xử Lý</button>';
                             break;
                         case 3:
                             label = '<button type="button" class="btn btn-outline btn-danger">Đã Hủy</button>';
                             break;
                         default:
                             label = '';
                             break;
                     }
                     return label;

                 }
             },
             {
                 "data": "OrderId",
                 "render": function (id, o, obj) {
                     var action = '';
                     if (obj.OrderStatus == 0) {
                         action = '<button type="button" order-id=' + id + ' class="btn btn-primary btn-edit-order"> <i class="fa fa-wrench"></i> Sửa</button>' +
                             '  <button type="button" order-id="' + id + '" class="btn btn-danger btn-disable-order"> <i class="fa fa-remove"></i> Hủy</button>';
                     }
                     return action;
                 }
             }
        ],
        drawCallback: function () {
            $(".btn-view-order").on('click', function () {
                var orderId = $(this).attr('order-id');
                $.post('/Contact/ViewOrderDetails', { orderId: orderId }, function (data) {
                    if (data.result) {
                        var $html = $(data.html);
                        $html.modal('show');
                    }
                });
            });
            $(".btn-edit-order").on('click', function () {
                var btnEdit = $(this);
                var orderId = btnEdit.attr('order-id');
                $.post('/Contact/ViewEditOrderInfo', { orderId: orderId }, function (data) {
                    if (data.result) {
                        var $html = $(data.html);
                        $html.modal('show');
                        var addressObj = $('.order-address', $html);
                        var phoneObj = $('.order-phone', $html);
                        var city = $('#city', $html);
                        var district = $('#district', $html);
                        city.on('change', function () {
                            var locationId = parseInt($(this).val());
                            if (isNaN(locationId)) {
                                locationId = 0;
                                district.attr('disabled', 'disabled');
                                district.val('');
                            } else {

                                $.post('/Contact/GetDistricts', { locationId: locationId }, function (data) {
                                    if (data.result) {
                                        district.removeAttr('disabled');
                                        district.empty();
                                        if (data.dictrictsModel) {
                                            district.append($('<option>'));
                                            $.each(data.dictrictsModel, function (i, o) {
                                                district.append($('<option value=' + o.LocationId + '>' + o.LocationName + '</option>'));
                                            });
                                        }
                                    }
                                });
                            }

                        });
                        var btnSave = $('#saveEditOrderInfo', $html);
                        btnSave.on('click', function () {
                            var addressValue = addressObj.val();
                            var phoneValue = phoneObj.val();
                            var cityValue = city.val();
                            var districtValue = district.val();
                            $.post('/Contact/SaveEditOrderInfo', { orderId: orderId,cityId:cityValue,districtId:districtValue, address: addressValue, phone: phoneValue }, function (data) {
                                if (data.result) {
                                    $html.modal('hide');
                                    table.ajax.reload();
                                }
                            });
                        });
                        $html.on('hidden.bs.modal', function (e) {
                            btnSave.off('click');
                        });
                    }
                });
            });
            $('.btn-disable-order').on('click', function () {
                var orderId = $(this).attr('order-id');
                $.post('/Contact/OrderToDisabled', { orderId: orderId }, function (data) {
                    if (data.result) {
                        alert('Đã hủy đơn hàng');
                        table.ajax.reload();
                    }
                });
            });

        }
    });
});