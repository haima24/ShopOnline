$(function () {
    var pageSize = 3;
    var filters = new Object();
    var filterCategory = $('#FilterCategoryId').val();
    if (filterCategory) {
        filters.categoryId = filterCategory;
    }
    var filterColor = $('#FilterColorIds').val();
    if (filterColor) {
        filters.colorIds = filterColor;
    }
    var filterBrands = $('#FilterBrandIds').val();
    if (filterBrands) {
        filters.brandIds = filterBrands;
    }
    var pagingData = function (pSize, passedObj) {
        $('#products').empty();
        $('#products').scrollPagination({
            'usejson': true,
            'isclick': true,
            'contentPage': '/ProductList/GetProducts', // the url you are fetching the results
            'contentData': { page: 0, pageSize: pageSize }, // these are the variables you can pass to the request, for example: children().size() to know which page you are
            'scrollTarget': $('#btn-load-more'), // who gonna scroll? in this example, the full window
            'heightOffset': 10, // it gonna request when scroll is 10 pixels before the page ends
            'beforeLoad': function(opts, obj) { // before load function, you can display a preloader div
                var pagerObj={ page: Math.ceil(obj.children().size() / pSize), pageSize: pSize };
                opts.contentData = $.extend(pagerObj, passedObj);
                //$('#loading').fadeIn();
            },
            'afterLoad': function(elementsLoaded, isLastPage) { // after loading content, you can use this function to animate your new elements
                //$('#loading').fadeOut();
                //var i = 0;
                //$(elementsLoaded).fadeInWithDelay();
                //if ($('#content').children().size() > 100){ // if more than 100 results already loaded, then stop pagination (only for testing)
                //    $('#nomoreresults').fadeIn();
                //    $('#content').stopScrollPagination();
                //}
                if (isLastPage) {
                    $('#btn-load-more').attr('disabled', 'disabled');
                } else {
                    $('#btn-load-more').removeAttr('disabled');
                }
            }
        });
    };
    var searchProducts = function () {
        pagingData(pageSize, filters);
    };
    searchProducts();
    $('#btn-filter-color-clear,#btn-filter-brand-clear').on('click', function (e) {
        e.preventDefault();
        var inputs = $(this).closest('.panel.sidebar-menu').find('input');
        inputs.prop('checked', false);
    });
    $('#category-menu .category-sub').on('click', function (e) {
        e.preventDefault();
        var categoryId = $(this).attr('child-category-id');
        filters.parentCategoryId = null;
        filters.categoryId = categoryId;
        searchProducts();
    });
    $('#btn-filter-brand').on('click', function (e) {
        e.preventDefault();
        var brandIds = '';
        $('#form-filter-brand .filter-brand:checked').each(function (i,o) {
            if (i == 0) {
                brandIds += $(this).attr('brand-id');
            } else {
                brandIds += "," + $(this).attr('brand-id');
            }
            
        });
        filters.brandIds = brandIds;
        searchProducts();
    });
    $('#btn-filter-color').on('click', function (e) {
        e.preventDefault();
        var colorIds = '';
        $('#form-filter-color .filter-color:checked').each(function (i, o) {
            if (i == 0) {
                colorIds += $(this).attr('color-id');
            } else {
                colorIds += "," + $(this).attr('color-id');
            }
           
        });
        filters.colorIds = colorIds;
        searchProducts();
    });
    $('#category-menu .category-parent').on('click', function (e) {
        e.preventDefault();
        var categoryId = $(this).attr('parent-category-id');
        filters.parentCategoryId = categoryId;
        filters.categoryId = null;
        searchProducts();
    });
    $('#category-all').on('click', function (e) {
        e.preventDefault();
        filters.parentCategoryId = null;
        filters.categoryId = null;
        searchProducts();
    });
});