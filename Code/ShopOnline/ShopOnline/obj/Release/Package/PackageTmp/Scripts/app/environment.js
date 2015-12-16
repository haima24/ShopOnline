$.formatDate = function(date) {
    var d = new Date(parseInt(date.substr(6)));
    return moment(d).format( "DD-MM-YYYY H:mm:ss");
};
$.convertJqueryArrayToJSArray = function(obj) {
    var arr = [];
    for (var i = 0; i < obj.length; i++) {
        arr[i] = obj[i];
    }
    return arr;
};

var oLanguage = {
    "sSearch": "Tìm Kiếm:",
    "oPaginate": {
        "sNext": "Trang Kế",
        "sPrevious": "Trang Trước"
    },
    "sInfo": "Đang hiện từ _START_ tới _END_ trong tổng số _TOTAL_",
    "sInfoEmpty": "",
    "sEmptyTable": "Không có dữ liệu",
    "sLengthMenu": "Hiện _MENU_ dòng thông tin",
    "sLoadingRecords": "Đang tải dữ liệu..."
};
var summernoteProductSetting = {
    height: 150,
    toolbar: [
        ['style', ['style']],
        ['font', ['bold', 'italic', 'underline', 'clear']],
        // ['font', ['bold', 'italic', 'underline', 'strikethrough', 'superscript', 'subscript', 'clear']],
        ['fontname', ['fontname']],
        ['fontsize', ['fontsize']],
        ['color', ['color']],
        ['para', ['ul', 'ol', 'paragraph']],
        ['height', ['height']],
        ['table', ['table']],
        ['insert', ['link', 'picture', 'hr']]
    ]
};