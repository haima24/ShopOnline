$(function () {
    var table = $('#category-datatable').DataTable({
        "oLanguage": oLanguage,
        reponsive: true,
        ajax: '/Admin/GetParentCategories',
        "columns": [
             {
                 "data": "CategoryId",
                 "render": function (id) {
                     return '<button type="button" categoryid="' + id + '" class="btn btn-primary btn-view-sub-category"> <i class="fa fa-search"></i> Chi Tiết Danh Mục</button>';
                 }
             },
             {
                 "data": "CategoryName",
                 "render": function (name) {
                     return '<p class="td-categoryname">' + name + '</p>';
                 }
             },
             {
                 "data": "CategoryId",
                 "render": function (id) {
                     return '<button type="button" categoryid="' + id + '" class="btn btn-primary btn-edit-category"> <i class="fa fa-edit"></i> Sửa</button>' +
                         '  <button type="button" categoryid="' + id + '" class="btn btn-danger btn-delete-category"> <i class="fa fa-remove"></i> Xóa</button>';
                 }
             }
        ],
        drawCallback: function () {
            $(".btn-edit-category").on('click', function () {
                var modal = $('#modalEditCategory');
                var btnEditCategory = $(this);
                var categoryName = $('.categoryname', modal);
                var currentName = btnEditCategory.closest('tr').find('.td-categoryname');
                categoryName.val(currentName.text());
                $('.categoryid', modal).val($(this).attr('categoryid'));

                var saveEditCategory = function () {
                    var id = $('.categoryid', modal).val();
                    var name = categoryName.val();
                    $.post('/Admin/EditParentCategory', { id: id, categoryName: name }, function (data) {
                        modal.modal('hide');
                        if (data.result) {
                            table.ajax.reload();
                        }
                    });
                };
                modal.on('hidden.bs.modal', function(e) {
                    $("#saveEditCategory").off('click');
                });
                $("#saveEditCategory").on('click', saveEditCategory);
                modal.modal('show');
            });
            $(".btn-delete-category").on('click', function () {
                var categoryid = $(this).attr('categoryid');
                $.post('/Admin/DeleteParentCategory', { id: categoryid }, function (data) {
                    if (data.result) {
                        table.ajax.reload();
                    }
                });
            });
            $(".btn-detail-category").on('click', function () {
                $('#panel-detail-category').show();
                $('#panel-detail-category .detail-category-name').text($(this).attr('categoryname'));
                $('#panel-detail-category #detailCategoryId').val($(this).attr('categoryid'));
            });
          
        }
    });
    $('#btnCreateCategory').on('click', function () {
        var modal = $('#modalNewCategory');
        var categoryName = $('.categoryname', modal);
        categoryName.val('');
        modal.modal('show');
        var saveNewCategory = function () {
            var name = categoryName.val();
            $.post('/Admin/CreateParentCategory', { categoryName: name }, function (data) {
                modal.modal('hide');
                if (data.result) {
                    table.ajax.reload();
                }
               
            });
        };
        modal.on('hidden.bs.modal', function (e) {
            $('#saveNewCategory').off('click');
        });
        $('#saveNewCategory').on('click', saveNewCategory);
    });
   

});