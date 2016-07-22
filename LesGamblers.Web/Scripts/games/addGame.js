$(function () {
    debugger;
    var today = new Date();
    var date = new Date(today.getYear(), today.getMonth(), today.getDate(), today.getHours(), today.getMinutes(), today.getSeconds());
    $('#datetimepicker1').datetimepicker({
        minDate: today.toUTCString(),
        format: "D.MM.YYYY HH:mm"
    });

    $("#datetimepicker1").mouseover(function () {
        $(this).css('cursor', 'pointer');
    });
});