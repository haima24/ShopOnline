$(function () {
    var table = $('#category-sub-datatable').DataTable({
        "oLanguage": oLanguage,
        reponsive: true,
        ajax: '/Admin/GetSubCategories',
        "columns": [
            {
                "data": "ParentCategoryName"
            },

             {
                 "data": "CategoryName",
                 "render": function (name) {
                     return '<p class="td-categoryname">' + name + '</p>';
                 }
             },
             {
                 "data": "CategoryId",
                 "render": function (id,o,obj) {
                     return '<button type="button" parent-categoryid="' + obj.ParentCategoryId + '" categoryid="' + id + '" class="btn btn-primary btn-edit-category"> <i class="fa fa-edit"></i> Sửa</button>' +
                         '  <button type="button" categoryid="' + id + '" class="btn btn-danger btn-delete-category"> <i class="fa fa-remove"></i> Xóa</button>';
                 }
             }
        ],
        drawCallback: function () {
            $(".btn-edit-category").on('click', function () {
                var modal = $('#modalEditCategory');
                var btnEditCategory = $(this);
                var parentSelect = $('#parent-category', modal);
                parentSelect.val(btnEditCategory.attr('parent-categoryid'));
                var categoryName = $('.categoryname', modal);
                var form = categoryName.closest('form');
                var currentName = btnEditCategory.closest('tr').find('.td-categoryname');
                categoryName.val(currentName.text());
                $('.categoryid', modal).val($(this).attr('categoryid'));
                var saveEditCategory = function () {
                    var id = $('.categoryid', modal).val();
                    var name = categoryName.val();
                    if (form.valid()) {
                        $.post('/Admin/EditSubCategory', { id: id, categoryName: name }, function (data) {
                            modal.modal('hide');
                            if (data.result) {
                                table.ajax.reload();
                            }
                            $("#saveEditCategory").off('click');
                        });
                    }
                   
                };
                modal.on('hidden.bs.modal', function (e) {
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
           
          
        }
    });
    $('#btnCreateCategorySub').on('click', function () {
        var modal = $('#modalNewCategory');
        var categoryName = $('.categoryname', modal);
        var form = categoryName.closest('form');
        var parentSelect = $('#parent-category', modal);
        categoryName.val('');
        modal.modal('show');
        var saveNewCategory = function () {
            var name = categoryName.val();
            var parentId = parentSelect.val();
            if (form.valid()) {
                $.post('/Admin/CreateSubCategory', { parentId: parentId, categoryName: name }, function (data) {
                    modal.modal('hide');
                    if (data.result) {
                        table.ajax.reload();
                    }
                });
            }
            
        };
        modal.on('hidden.bs.modal', function (e) {
            $('#saveNewCategory').off('click');
        });
        $('#saveNewCategory').on('click', saveNewCategory);
    });
   

});