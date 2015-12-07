$.formatDate = function(date) {
    var d = new Date(parseInt(date.substr(6)));
    return moment(d).format( "DD-MM-YYYY-H:mm:ss");
};
$.convertJqueryArrayToJSArray = function(obj) {
    var arr = [];
    for (var i = 0; i < obj.length; i++) {
        arr[i] = obj[i];
    }
    return arr;
};

var oLanguage = {
    "sSearch": "Tìm Kiếm:"
};
var toolbar = [
    //[groupname, [button list]]

    ['style', ['bold', 'italic', 'underline', 'clear']],
    ['font', ['strikethrough', 'superscript', 'subscript']],
    ['fontsize', ['fontsize']],
    ['color', ['color']],
    ['para', ['ul', 'ol', 'paragraph']],
    ['height', ['height']]
];