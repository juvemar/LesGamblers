$(function () {
    debugger;
    var today = new Date();
    $('#datetimepicker1').datetimepicker({
        minDate: today,
        format: "D.MM.YYYY HH:mm"
    });

    $("#datetimepicker1").mouseover(function () {
        $(this).css('cursor', 'pointer');
    });
});