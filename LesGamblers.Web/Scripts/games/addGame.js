$(function () {
    var today = new Date();
    $('#datetimepicker1').datetimepicker({
        minDate: today.setDate(today.getDate() + 1),
        format: "D.MM.YYYY HH:mm"
    });

    $("#datetimepicker1").mouseover(function () {
        $(this).css('cursor', 'pointer');
    });
});