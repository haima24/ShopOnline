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
    $('.product-price').mask('###,###,###,###,###,###,###,##0', { reverse: true });
    var table = $('#product-datatable').DataTable({
        "oLanguage": oLanguage,
        reponsive: true,
        ajax: '/Admin/GetProductList',
        "columns": [
             {
                 "data": "ProductImages",
                 "render": function (images, o, obj) {
                     var div = $("<div class='carousel slide' data-ride=carousel></div>");
                     var imgs = '';
                     if (images.length > 0) {
                         var count = 0;
                         $(images).each(function () {
                             if (count == 0) {
                                 imgs += '<div product-image-id=' + this.ProductImageId + ' class="item active"><img src=' + this.ProductImageUrl + ' width=200 height=100></div>';
                             } else {
                                 imgs += '<div product-image-id=' + this.ProductImageId + ' class=item><img src=' + this.ProductImageUrl + ' width=200 height=100></div>';
                             }
                             count++;
                         });
                     } else {
                         imgs += '<div product-image-id=' + obj.ProductImageDisplay.ProductImageId + ' class="item active"><img src=' + obj.ProductImageDisplay.ProductImageUrl + ' width=200 height=100></div>';
                     }

                     return $('<div>').html(div.html($('<div class="carousel-inner">').html(imgs))).html();
                 }
             },
             {
                 "data": "ProductName"
             },
            {
                "data": "ColorCodes",
                "render": function (codes, o, obj) {
                    var colors = $('<div>');
                    $(codes).each(function () {
                        var color = '<div style="background: ' + this + ';width: 20px;height: 20px;"></div>';
                        colors.append(color);
                    });
                    return $('<div>').append(colors).html();
                }
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
                'data': 'IsNew',
                'render': function (isNew) {
                    if (isNew && isNew == true) {
                        return '<input type=checkbox disabled checked />';
                    } else {
                        return '<input type=checkbox disabled />';
                    }
                }
            },
             {
                 "data": "ProductId",
                 "render": function (id, o, obj) {
                     var imgs = '';
                     $(obj.ProductImages).each(function () {
                         imgs += '<div class=dz-image product-image-id=' + this.ProductImageId + '><img src=' + this.ProductImageUrl + ' width=250 height=100><a product-image-id=' + this.ProductImageId + ' class="dz-remove exist" product-image-id=' + ' href="#" data-dz-remove="">Remove file</a></div>';
                     });
                     return '<button type="button" product-isnew=' + obj.IsNew + ' product-categories="' + obj.ProductCategories.join(',') + '" product-brand="' + obj.BrandId + '" product-colors="' + obj.ProductColors.join(',') + '" product-code=' + obj.ProductCode + ' product-price=' + obj.PriceString + ' product-name="' + obj.ProductName + '" productid="' + id + '" class="btn btn-primary btn-edit-product"> <i class="fa fa-edit"></i> Sửa</button>' +
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
            $('#modalEditProduct .product-shortdescription').summernote(summernoteProductSetting);
            $('#modalEditProduct .product-detaildescription').summernote(summernoteProductSetting);
            $('#modalEditProduct .product-categories').multiselect({
                nonSelectedText: "Không"
            });
            $('#modalEditProduct .product-brand').multiselect({
                nonSelectedText: "Không"
            });
            $('#modalEditProduct .product-colors').multiselect({
                nonSelectedText: "Không",
                enableHTML: true,
                optionLabel: function (element) {
                    var $element = $(element);
                    var color = '<div style="background: ' + $element.attr('color-value') + ';width: 130px;height: 20px;"></div>';
                    return color + $element.html();
                }

            });
            $(".btn-edit-product").on('click', function () {
                var modal = $('#modalEditProduct');
                var btnEditCategory = $(this);
                $('.productid', modal).val($(this).attr("productid"));
                var productIsNew = $('.product-isnew', modal);
                productIsNew.prop('checked', btnEditCategory.attr('product-isnew') == "true");
                var productName = $('.product-name', modal);
                var form = productName.closest('form');
                productName.val(btnEditCategory.attr('product-name'));
                var productCode = $('.product-code', modal);
                productCode.val(btnEditCategory.attr('product-code'));
                var productPrice = $('.product-price', modal);
                productPrice.val(btnEditCategory.attr('product-price'));
                var productBrand = $('.product-brand', modal);
                var brandId = btnEditCategory.attr('product-brand');
                productBrand.val(brandId ? brandId : '');
                productBrand.multiselect("refresh");
                var productColors = $('.product-colors', modal);
                productColors.val(btnEditCategory.attr('product-colors').split(','));
                productColors.multiselect("refresh");
                var shortDescription = btnEditCategory.closest('tr').find('.product-shortdescription').html();
                var detailDescription = btnEditCategory.closest('tr').find('.product-detaildescription').html();
                var shortDesc = $('.product-shortdescription', modal);
                shortDesc.code(shortDescription);
                var detailDesc = $('.product-detaildescription', modal);
                detailDesc.code(detailDescription);
                var categories = $('.product-categories', modal);
                categories.val(btnEditCategory.attr('product-categories').split(','));
                var productImages = btnEditCategory.closest('tr').find('.product-images').html();
                var formProductImages = $('.product-images', modal);
                formProductImages.html(productImages);
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
                    var price = productPrice.cleanVal();
                    var shortDescValue = shortDesc.code();
                    var detailDescValue = detailDesc.code();
                    var categoriesValues = categories.val();
                    var categoryIdArray = [];
                    var brandIdValue = productBrand.val();
                    var categoryIds = '';
                    var colorValues = productColors.val();
                    var colorIdArray = [];
                    var colorIds = '';
                    var isNew = productIsNew.prop('checked');
                    if (categoriesValues && categoriesValues.length > 0) {
                        categoryIdArray = categoriesValues.map(function (o, i) {
                            return parseInt(o);
                        });
                        categoryIds = $.convertJqueryArrayToJSArray(categoryIdArray).join(',');
                    }
                    if (colorValues && colorValues.length > 0) {
                        colorIdArray = colorValues.map(function (o, i) {
                            return parseInt(o);
                        });
                        colorIds = $.convertJqueryArrayToJSArray(colorIdArray).join(',');
                    }

                    if (form.valid()) {
                        var transformer = dropEditProduct;
                        transformer.options.url = '/Admin/UpdateProduct';
                        transformer.options.params = { id: id,brandId:brandIdValue,colorIds:colorIds, categoryIds: categoryIds, code: code, name: name, isNew: isNew, price: price, shortDescription: shortDescValue, detailDescription: detailDescValue },
                        transformer.options.customcompletemultiplefiles = function (files, data) {
                            modal.modal('hide');
                            if (data.result) {
                                table.ajax.reload();
                            }
                            $("#saveEditProduct").off('click');
                            $('.dz-remove.exist').off('click');
                        };
                        transformer.processQueue();
                    }

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
                    } else {
                        alert('Sản phẩm này đã được đặt hàng, không thể xóa.');
                    }
                });
            });

        }
    });
    $('#btnCreateProduct').on('click', function () {
        var modal = $('#modalNewProduct');
        var productName = $('.product-name', modal);
        productName.val('');
        var form = productName.closest('form');
        var productCode = $('.product-code', modal);
        productCode.val('');
        var productPrice = $('.product-price', modal);
        productPrice.val('');
        var brand = $('.product-brand', modal);
        brand.val('');
        var color = $('.product-colors', modal);
        color.val('');
        color.multiselect('refresh');
        var categories = $('.product-categories', modal);
        categories.val('');
        categories.multiselect('refresh');
        var isNewBox = $('.product-isnew', modal);
        isNewBox.removeProp('checked');
        var formProductImages = $('.product-images', modal);
        formProductImages.empty();
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
            var price = productPrice.cleanVal();
            var categoriesValues = categories.val();
            var categoryIdArray = [];
            var categoryIds = '';
            var colorValues = color.val();
            var colorIdArray = [];
            var colorIds = '';
            var brandId = brand.val();
            var isNew = isNewBox.prop('checked');
            if (categoriesValues && categoriesValues.length > 0) {
                categoryIdArray = categoriesValues.map(function (o, i) {
                    return parseInt(o);
                });
                categoryIds = $.convertJqueryArrayToJSArray(categoryIdArray).join(',');
            }
            if (colorValues && colorValues.length > 0) {
                colorIdArray = colorValues.map(function (o, i) {
                    return parseInt(o);
                });
                colorIds = $.convertJqueryArrayToJSArray(colorIdArray).join(',');
            }
            if (form.valid()) {
                var transformer = dropNewProduct;
                transformer.options.params = { categoryIds: categoryIds, brandId: brandId, colorIds: colorIds, code: code, name: name, isNew: isNew, price: price, shortDescription: shortdescription, detailDescription: detaildescription },
                transformer.options.customcompletemultiplefiles = function (files, data) {
                    modal.modal('hide');
                    if (data.result) {
                        table.ajax.reload();
                    }
                    $('#saveNewProduct').off('click');
                };

                transformer.processQueue();
            }


        };
        $('#saveNewProduct').on('click', saveNewProduct);
    });
    $('#modalNewProduct .product-shortdescription').summernote(summernoteProductSetting);
    $('#modalNewProduct .product-detaildescription').summernote(summernoteProductSetting);
    $('#modalNewProduct .product-categories').multiselect({
        nonSelectedText: "Không"
    });
    $('#modalNewProduct .product-brand').multiselect({
        nonSelectedText: "Không"
    });
    $('#modalNewProduct .product-colors').multiselect({
        nonSelectedText: "Không",
        enableHTML: true,
        optionLabel: function (element) {
            var $element = $(element);
            var color = '<div style="background: ' + $element.attr('color-value') + ';width: 130px;height: 20px;"></div>';
            return color + $element.html();
        }

    });

});