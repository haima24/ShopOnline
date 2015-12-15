$(function () {

    var table = $('#color-new-datatable').DataTable({
        "oLanguage": oLanguage,
        reponsive: true,
        ajax: '/Admin/ColorList',
        "columns": [

             {
                 "data": "ColorValue",
                 "render": function (color, o, obj) {
                     return '<div style="background: ' + color + ';width: 130px;height: 20px;"></div>';
                 }
             },
            {
                "data": "ColorName"
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
                 "data": "ColorId",
                 "render": function (id, o, obj) {
                     return '<button type="button" color-value="'+obj.ColorValue+'" color-name="'+obj.ColorName+'" color-id="' + id + '" class="btn btn-primary btn-edit-color"> <i class="fa fa-edit"></i> Sửa</button>' +
                         '  <button type="button" color-id="' + id + '" class="btn btn-danger btn-delete-color"> <i class="fa fa-remove"></i> Xóa</button>';
                 }
             }
        ],
        drawCallback: function () {
            $(".btn-edit-color").on('click', function () {
                var modal = $('#modalEditColor');
                var btnEditColor = $(this);
                var colorId = btnEditColor.attr('color-id');
                var colorName = $('.color-name', modal);
                var form = colorName.closest('form');
                var currentName = btnEditColor.attr('color-name');
                colorName.val(currentName);
                var colorObj = $('.color-value', modal);
                var colorVal = btnEditColor.attr('color-value');
                colorObj.val(colorVal);
                colorObj.closest('.colorValuePlaceHoder').colorpicker('setValue', colorVal);
                var saveEditColor = function () {
                    var name = colorName.val();
                    var value = colorObj.val();
                    if (form.valid()) {
                        $.post('/Admin/EditColor', { colorId: colorId, name: name, value: value }, function (data) {
                            modal.modal('hide');
                            if (data.result) {
                                table.ajax.reload();
                            }
                        });
                    }

                };
                modal.on('hidden.bs.modal', function (e) {
                    $("#saveEditColor").off('click');
                });
                $("#saveEditColor").on('click', saveEditColor);
                modal.modal('show');
            });
            $(".btn-delete-color").on('click', function () {
                var colorId = $(this).attr('color-id');
                $.post('/Admin/DeleteColor', { colorId: colorId }, function (data) {
                    if (data.result) {
                        table.ajax.reload();
                    }
                });
            });
        }
    });
    $('#btnCreateColor').on('click', function () {
        var modal = $('#modalNewColor');

        var colorName = $('.color-name', modal);
        var colorValue = $(".color-value", modal);
        var form = colorName.closest('form');
        form.validate().settings.ignore = '';
        colorName.val('');
        modal.modal('show');
        var saveNewColor = function () {
            if (form.valid()) {
                var name = colorName.val();
                var value = colorValue.val();
                $.post('/Admin/CreateColor', { name: name,value:value }, function (data) {
                    modal.modal('hide');
                    if (data.result) {
                        table.ajax.reload();
                    }

                });
            }

        };
        modal.on('hidden.bs.modal', function (e) {
            $('#saveNewColor').off('click');
        });
        $('#saveNewColor').on('click', saveNewColor);
    });
    $('#modalNewColor .colorValuePlaceHoder').colorpicker().on('changeColor.colorpicker', function (event) {
        $(this).closest('form').valid();
    });
    $('#modalEditColor .colorValuePlaceHoder').colorpicker().on('changeColor.colorpicker', function (event) {
        $(this).closest('form').valid();
    });
});