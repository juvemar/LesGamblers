$("#hostPlayersDropdown").change(function () {
    debugger;
    var val = this.value;
    if (val === '0') {
        return;
    }
    $('#goalscorer').val(val.toString());
});

$("#guestPlayersDropdown").change(function () {
    var val = this.value;
    if (val === '0') {
        return;
    }
    $('#goalscorer').val(val.toString());
});