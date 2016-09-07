$(function () {
    $('#datetimepicker1').datetimepicker({
        format: "D.MM.YYYY HH:mm"
    });

    $("#datetimepicker1").mouseover(function () {
        $(this).css('cursor', 'pointer');
    });
});