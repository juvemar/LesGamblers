$("#gameDropdown").change(function () {
    var val = this.value;
    if (val != '') {
        $('#chooseGoalscorer').css("display", "");
        $("#btnChooseGoalscorer").attr("disabled", false);
        $("#btnChooseGoalscorer").attr("data-toggle", "collapse");
        if ($('#chooseGoalscorer').hasClass("in")) {
            _loadDropdownPlayers();
        }
    } else {
        $("#btnChooseGoalscorer").attr("disabled", true);
        $("#btnChooseGoalscorer").attr("data-toggle", "");
        $('#chooseGoalscorer').hide();
    }
});

$("#hostPlayersDropdown").click(function () {
    $('#hostPlayersDropdown').val("0");
});

$("#guestPlayersDropdown").click(function () {
    $('#guestPlayersDropdown').val("0");
});

$("#btnChooseGoalscorer").click(function () {
    _loadDropdownPlayers();
});

function _loadDropdownPlayers() {
    var selectedGame = $('#gameDropdown').val();
    debugger;
    var hasAttr = $("#btnChooseGoalscorer").attr("disabled");
    if (!selectedGame || selectedGame === "" || hasAttr == "disabled") {
        return;
    }

    var teams = {
        gameId: selectedGame
    };
    $.ajax({
        type: "GET",
        url: "/Predictions/_PlayersDropdownPartial",
        data: teams,
        success: function (data) {
            $('#hostTeam').text(data.hostPlayers[0].Country);
            $('#guestTeam').text(data.guestPlayers[0].Country);

            $('#hostPlayersDropdown').empty();
            $('#hostPlayersDropdown').append($("<option />").val('0').text(' - Choose Goalscorer - '));
            $.each(data.hostPlayers, function () {
                $('#hostPlayersDropdown').append($("<option />").val(this.Name).text(this.Name + ' (' + this.Club + ')'));
            });
            $('#guestPlayersDropdown').empty();
            $('#guestPlayersDropdown').append($("<option />").val('0').text(' - Choose Goalscorer - '));
            $.each(data.guestPlayers, function () {
                $('#guestPlayersDropdown').append($("<option />").val(this.Name).text(this.Name + ' (' + this.Club + ')'));
            });
        },
        error: function (errorData) { onError(errorData); }
    });
};