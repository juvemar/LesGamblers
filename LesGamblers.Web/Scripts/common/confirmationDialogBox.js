$(function () {
    var item_to_delete;
    $("#delete-btn").click(function (e) {
        item_to_delete = $(this).data('id');
    });
})