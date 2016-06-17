$("#gameDropdown").change(function () {
    debugger;
    var val = this.value;
    if (val != '') {
        $("#btnChooseGoalscorer").attr("disabled", false);
        $("#btnChooseGoalscorer").attr("data-toggle", "collapse");
    } else {
        $("#btnChooseGoalscorer").attr("disabled", true);
        $("#btnChooseGoalscorer").attr("data-toggle", "");
    }
});

$("#btnChooseGoalscorer").click(function () {

});