$(function () {
    var table = $('#brand-new-datatable').DataTable({
        "oLanguage": oLanguage,
        reponsive: true,
        ajax: '/Admin/BrandList',
        "columns": [
             {
                 "data": "BrandName"
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
                 "data": "BrandId",
                 "render": function (id,o,obj) {
                     return '<button type="button" brand-name="' + obj.BrandName + '" brand-id="' + id + '" class="btn btn-primary btn-edit-brand"> <i class="fa fa-edit"></i> Sửa</button>' +
                         '  <button type="button" brand-id="' + id + '" class="btn btn-danger btn-delete-brand"> <i class="fa fa-remove"></i> Xóa</button>';
                 }
             }
        ],
        drawCallback: function () {
            $(".btn-edit-brand").on('click', function () {
                var modal = $('#modalEditBrand');
                var btnEditBrand = $(this);
                var brandId = btnEditBrand.attr('brand-id');
                var brandName = $('.brand-name', modal);
                var form = brandName.closest('form');
                var currentName = btnEditBrand.attr('brand-name');
                brandName.val(currentName);
              

                var saveEditBrand= function () {
                    var name = brandName.val();
                    if (form.valid()) {
                        $.post('/Admin/EditBrand', { brandId: brandId, brandName: name }, function (data) {
                            modal.modal('hide');
                            if (data.result) {
                                table.ajax.reload();
                            }
                        });
                    }

                };
                modal.on('hidden.bs.modal', function (e) {
                    $("#saveEditBrand").off('click');
                });
                $("#saveEditBrand").on('click', saveEditBrand);
                modal.modal('show');
            });
            $(".btn-delete-brand").on('click', function () {
                var brandId = $(this).attr('brand-id');
                $.post('/Admin/DeleteBrand', { brandId: brandId }, function (data) {
                    if (data.result) {
                        table.ajax.reload();
                    }
                });
            });
        }
    });
    $('#btnCreateBrand').on('click', function () {
        var modal = $('#modalNewBrand');

        var brandName = $('.brand-name', modal);
        var form = brandName.closest('form');
        brandName.val('');
        modal.modal('show');
        var saveNewBrand = function () {
            if (form.valid()) {
                var name = brandName.val();
                $.post('/Admin/CreateBrand', { brandName: name }, function (data) {
                    modal.modal('hide');
                    if (data.result) {
                        table.ajax.reload();
                    }

                });
            }

        };
        modal.on('hidden.bs.modal', function (e) {
            $('#saveNewBrand').off('click');
        });
        $('#saveNewBrand').on('click', saveNewBrand);
    });


});