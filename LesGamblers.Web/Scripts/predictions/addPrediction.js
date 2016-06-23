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

$("#btnChooseGoalscorer").click(function () {
    _loadDropdownPlayers();
});

function _loadDropdownPlayers() {
    var selectedGame = $('#gameDropdown').val();
    if (!selectedGame || selectedGame === "") {
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
            $('#hostPlayersDropdown').append($("<option />").val('').text(' - Choose Goalscorer - '));
            $.each(data.hostPlayers, function () {
                //this.Name = this.Name.replace(/([a-z])([A-Z])/g, '$1 $2');
                $('#hostPlayersDropdown').append($("<option />").val(this.Name).text(this.Name + ' (' + this.Club + ')'));
            });
            $('#guestPlayersDropdown').empty();
            $('#guestPlayersDropdown').append($("<option />").val('').text(' - Choose Goalscorer - '));
            $.each(data.guestPlayers, function () {
                $('#guestPlayersDropdown').append($("<option />").val(this.Name).text(this.Name + ' (' + this.Club + ')'));
            });
        },
        error: function (errorData) { onError(errorData); }
    });
};

$("#hostPlayersDropdown").change(function () {
    var val = this.value;
    var currentView = $('#viewTitle').text();
    if (currentView == "Add your predictions") {
        $('#goalscorer').val(val.toString());
    } else if (currentView == "Update Game") {
        var currentGoalscorers = $('#Goalscorers').val();
        if (currentGoalscorers == '') {
            $('#Goalscorers').val(val);
            return;
        }
        $('#Goalscorers').val(currentGoalscorers + ", " + val);
    }
});

$("#guestPlayersDropdown").change(function () {
    var val = this.value;
    var currentView = $('#viewTitle').text();
    if (currentView == "Add your predictions") {
        $('#goalscorer').val(val.toString());
    } else if (currentView == "Update Game") {
        var currentGoalscorers = $('#Goalscorers').val();
        if (currentGoalscorers == '') {
            $('#Goalscorers').val(val);
            return;
        }
        $('#Goalscorers').val(currentGoalscorers + ", " + val);
    }
});