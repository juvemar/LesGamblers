$("#gameDropdown").change(function () {
    var val = this.value;
    if (val != '') {
        $('#chooseGoalscorer').css("display", "");
        $("#btnChooseGoalscorer").attr("disabled", false);
        $("#btnChooseGoalscorer").attr("data-toggle", "collapse");
        _loadDropdownPlayers();
    } else {
        $("#btnChooseGoalscorer").attr("disabled", true);
        $("#btnChooseGoalscorer").attr("data-toggle", "");
        $('#chooseGoalscorer').hide();
    }
});

$("#btnChooseGoalscorer").click(function () {
    _loadDropdownPlayers();
});

function _loadDropdownPlayers() {
    var selectedGame = $('#gameDropdown').val().split(' ');
    var firstTeam = selectedGame[0].replace(/_/g, " ");
    var secondTeam = selectedGame[1].replace(/_/g, " ");
    var teams = {
        firstTeam: firstTeam,
        secondTeam: secondTeam
    };

    $.ajax({
        type: "GET",
        url: "/Predictions/_PlayersDropdownPartial",
        data: teams,
        success: function (data) {
            debugger;
            $('#hostTeam').text(firstTeam);
            $('#guestTeam').text(secondTeam);

            $('#hostPlayersDropdown').empty();
            $('#hostPlayersDropdown').append($("<option />").val('').text(' - Choose Goalscorer - '));
            $.each(data.hostPlayers, function () {
                this.Name = this.Name.replace(/([a-z])([A-Z])/g, '$1 $2');
                $('#hostPlayersDropdown').append($("<option />").val(this.Name).text(this.Name + ' (' + this.Club + ')'));
            });
            $('#guestPlayersDropdown').empty();
            $('#guestPlayersDropdown').append($("<option />").val('').text(' - Choose Goalscorer - '));
            $.each(data.guestPlayers, function () {
                $('#guestPlayersDropdown').append($("<option />").val(this).text(this.Name + ' (' + this.Club + ')'));
            });
        },
        error: function (errorData) { onError(errorData); }
    });
};

$("#hostPlayersDropdown").change(function () {
    var val = this.value;
    $('#goalscorer').val(val);
});

$("#guestPlayersDropdown").change(function () {
    var val = this.value;
    $('#goalscorer').val(val);
});