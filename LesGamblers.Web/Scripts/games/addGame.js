$(function () {
    var today = new Date();
    $('#datetimepicker1').datetimepicker({
        minDate: today.setDate(today.getDate()),
        format: "D.MM.YYYY HH:mm"
    });

    $("#datetimepicker1").mouseover(function () {
        $(this).css('cursor', 'pointer');
    });
});