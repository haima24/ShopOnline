$(function () {
    var multipleUploadConfig = {
        url: '/Admin/CreateProduct',
        addRemoveLinks: true,
        parallelUploads: 4,
        paramName: "files",
        uploadMultiple: true,
        autoProcessQueue: false,
        thumbnailWidth: 250,
        thumbnailHeight: 100
    };
    Dropzone.autoDiscover = false;
    var dropNewProduct = new Dropzone("#modalNewProduct .product-images", multipleUploadConfig);
    var dropEditProduct = new Dropzone("#modalEditProduct .product-images", multipleUploadConfig);
    var table = $('#product-datatable').DataTable({
        "oLanguage": oLanguage,
        reponsive: true,
        ajax: '/Admin/GetProductList',
        "columns": [
             {
                 "data": "ProductImages",
                 "render": function (images) {
                     var div = $("<div class='carousel slide' data-ride=carousel></div>");
                     var imgs = '';
                     var count = 0;
                     $(images).each(function () {
                         if (count == 0) {
                             imgs += '<div product-image-id='+this.ProductImageId+' class="item active"><img src=' + this.ProductImageUrl + ' width=200 height=100></div>';
                         } else {
                             imgs += '<div product-image-id=' + this.ProductImageId + ' class=item><img src=' + this.ProductImageUrl + ' width=200 height=100></div>';
                         }
                         count++;
                     });
                     return $('<div>').html(div.html($('<div class="carousel-inner">').html(imgs))).html();
                 }
             },
             {
                 "data": "ProductName"
             },
             {
                 "data": "ProductCode"
             },
             {
                 "data": "Price"
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
                 "data": "ProductId",
                 "render": function (id, o, obj) {
                     var imgs = '';
                     $(obj.ProductImages).each(function () {
                         imgs += '<div class=dz-image product-image-id='+this.ProductImageId+'><img src=' + this.ProductImageUrl + ' width=250 height=100><a product-image-id=' + this.ProductImageId + ' class="dz-remove exist" product-image-id=' + ' href="#" data-dz-remove="">Remove file</a></div>';
                     });
                     return '<button type="button" product-categories=' + obj.ProductCategories.join(',') + ' product-code=' + obj.ProductCode + ' product-price=' + obj.Price + ' product-name="' + obj.ProductName + '" productid="' + id + '" class="btn btn-primary btn-edit-product"> <i class="fa fa-edit"></i> Sửa</button>' +
                         '  <button type="button" productid="' + id + '" class="btn btn-danger btn-delete-product"> <i class="fa fa-remove"></i> Xóa</button>' +
                         '<div style=display:none class=product-shortdescription>' + obj.ProductShortDescription + '</div>' +
                          '<div style=display:none class=product-detaildescription>' + obj.ProductDetailDescription + '</div>' +
                         '<div style=display:none class=product-images>' + imgs + '</div>';
                 }
             }
        ],
        drawCallback: function () {
            $('.carousel').carousel({
                interval: 1000
            });
            $('#modalEditProduct .product-shortdescription').summernote({ toolbar: toolbar });
            $('#modalEditProduct .product-detaildescription').summernote({ toolbar: toolbar });
            $('#modalEditProduct .product-categories').multiselect();

            $(".btn-edit-product").on('click', function () {
                var modal = $('#modalEditProduct');
                var btnEditCategory = $(this);
                $('.productid', modal).val($(this).attr("productid"));
                var productName = $('.product-name', modal);
                productName.val(btnEditCategory.attr('product-name'));
                var productCode = $('.product-code', modal);
                productCode.val(btnEditCategory.attr('product-code'));
                var productPrice = $('.product-price', modal);
                productPrice.val(btnEditCategory.attr('product-price'));
                var shortDescription = btnEditCategory.closest('tr').find('.product-shortdescription').html();
                var detailDescription = btnEditCategory.closest('tr').find('.product-detaildescription').html();
                var shortDesc = $('.product-shortdescription', modal);
                shortDesc.code(shortDescription);
                var detailDesc = $('.product-detaildescription', modal);
                detailDesc.code(detailDescription);
                var categories = $('.product-categories', modal);
                categories.val($(this).attr('product-categories').split(','));
                var productImages = btnEditCategory.closest('tr').find('.product-images').html();
                $('.product-images', modal).html(productImages);
                categories.multiselect("refresh");
                var removeProductImage = function () {
                    var btnRemove = $(this);
                    var productImageId = btnRemove.attr('product-image-id');
                    $.post('/Admin/DeleteProductImage', { productImageId: productImageId }, function (data) {
                        if (data.result) {
                            var image = btnRemove.closest('.dz-image');
                            image.remove();
                            btnEditCategory.closest('tr').find('.dz-image[product-image-id=' + productImageId + ']').remove();
                            btnEditCategory.closest('tr').find('.carousel .item[product-image-id=' + productImageId + ']').remove();
                        }
                    });
                };
                var saveEditProduct = function () {
                    var id = $('.productid', modal).val();
                    var name = productName.val();
                    var code = productCode.val();
                    var price = productPrice.val();
                    var shortDescValue = shortDesc.code();
                    var detailDescValue = detailDesc.code();
                    var categoriesValues = categories.val();
                    var categoryIdArray = [];
                    var categoryIds = '';
                    if (categoriesValues && categoriesValues.length > 0) {
                        categoryIdArray = categoriesValues.map(function (o, i) {
                            return parseInt(o);
                        });
                        categoryIds = $.convertJqueryArrayToJSArray(categoryIdArray).join(',');
                    }

                   
                    var transformer = dropEditProduct;
                    transformer.options.url = '/Admin/UpdateProduct';
                    transformer.options.params = { id: id, categoryIds: categoryIds, code: code, name: name, price: price, shortDescription: shortDescValue, detailDescription: detailDescValue },
                    transformer.options.customcompletemultiplefiles = function (files, data) {
                        modal.modal('hide');
                        if (data.result) {
                            table.ajax.reload();
                        }
                        $("#saveEditProduct").off('click', saveEditProduct);
                        $('.dz-remove.exist').off('click', removeProductImage);
                    };
                    transformer.processQueue();
                };
                $('.dz-remove.exist').on('click', removeProductImage);
                $("#saveEditProduct").on('click', saveEditProduct);
                modal.modal('show');
            });
            $(".btn-delete-product").on('click', function () {
                var productid = $(this).attr('productid');
                $.post('/Admin/DeleteProduct', { id: productid }, function (data) {
                    if (data.result) {
                        table.ajax.reload();
                    }
                });
            });

        }
    });
    $('#btnCreateProduct').on('click', function () {
        var modal = $('#modalNewProduct');
        var productName = $('.product-name', modal);
        productName.val('');
        var productCode = $('.product-code', modal);
        productCode.val('');
        var productPrice = $('.product-price', modal);
        productPrice.val('');
        var categories = $('.product-categories', modal);
        categories.val('');
        categories.multiselect('refresh');
        $('.product-images', modal).empty();
        modal.modal('show');
        var shortdescriptionObj = $('.product-shortdescription', modal);
        shortdescriptionObj.code('<p><br></p>');
        var detaildescriptionObj = $('.product-detaildescription', modal);
        detaildescriptionObj.code('<p><br></p>');
        var saveNewProduct = function () {
            var shortdescription = shortdescriptionObj.code();
            var detaildescription = detaildescriptionObj.code();
            var name = productName.val();
            var code = productCode.val();
            var price = productPrice.val();
            var categoriesValues = categories.val();
            var categoryIdArray = [];
            var categoryIds = '';
            if (categoriesValues && categoriesValues.length > 0) {
                categoryIdArray = categoriesValues.map(function (o, i) {
                    return parseInt(o);
                });
                categoryIds = $.convertJqueryArrayToJSArray(categoryIdArray).join(',');
            }
            var transformer = dropNewProduct;
            transformer.options.params = { categoryIds: categoryIds, code: code, name: name, price: price, shortDescription: shortdescription, detailDescription: detaildescription },
            transformer.options.customcompletemultiplefiles = function (files, data) {
                modal.modal('hide');
                if (data.result) {
                    table.ajax.reload();
                }
                $('#saveNewProduct').off('click', saveNewProduct);
            };

            transformer.processQueue();

        };
        $('#saveNewProduct').on('click', saveNewProduct);
    });
    $('#modalNewProduct .product-shortdescription').summernote({ toolbar: toolbar });
    $('#modalNewProduct .product-detaildescription').summernote({ toolbar: toolbar });
    $('#modalNewProduct .product-categories').multiselect();

});