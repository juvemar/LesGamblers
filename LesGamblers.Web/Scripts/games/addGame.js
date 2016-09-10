$(function () {
    debugger;
    var today = new Date();
    var date = new Date(today.getYear(), today.getMonth(), today.getDate(), today.getHours(), today.getMinutes(), today.getSeconds());
    $('#datetimepicker1').datetimepicker({
        minDate: date,
        format: "D.MM.YYYY HH:mm"
    });
    $("#datetimepicker1").mouseover(function () {
        $(this).css('cursor', 'pointer');
    });
});

//$(function () {
//    debugger;
//    var today = new Date(new Date().getTime() + 24 * 60 * 60 * 1000);
//    var strDate = today.getDate() + '.' + (today.getMonth() + 1) + '.' + today.getFullYear() + ' ' + today.getHours() + ':' + today.getMinutes();
//    $('#matchDate-input').val(strDate);
//});