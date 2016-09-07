$(function () {
    debugger;
    console.log("function zaredi");
    var today = new Date();
    var date = new Date(today.getYear(), today.getMonth(), today.getDate(), today.getHours(), today.getMinutes(), today.getSeconds());
    $('#datetimepicker1').datetimepicker({
        minDate: date,
        format: "D.MM.YYYY HH:mm"
    });
    console.log('datepicker loaded');
    $("#datetimepicker1").mouseover(function () {
        $(this).css('cursor', 'pointer');
    });
    console.log('datepicker cssed');
});

$(document).ready(function () {
    console.log("page ready!");
});