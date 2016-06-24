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

$("#hostPlayersDropdown").change(function () {
    var val = this.value;
    if (val === '0') {
        return;
    }

    var currentView = $('#viewTitle').text();
    if (currentView == "Add your predictions") {
        $('#goalscorer').val(val.toString());
    } else if (currentView == "Update Game") {
        var currentGoalscorers = $('#GoalscorersList').val();
        if (currentGoalscorers === '') {
            $('#GoalscorersList').val(val);
            return;
        }
        $('#GoalscorersList').val(currentGoalscorers + ", " + val);
    }
});

$("#hostPlayersDropdown").click(function () {
    $('#hostPlayersDropdown').val("0");
});

$("#guestPlayersDropdown").change(function () {
    debugger;
    var val = this.value;
    if (val === '0') {
        return;
    }

    var currentView = $('#viewTitle').text();
    if (currentView == "Add your predictions") {
        $('#goalscorer').val(val.toString());
    } else if (currentView == "Update Game") {
        var currentGoalscorers = $('#GoalscorersList').val();
        if (currentGoalscorers === '') {
            $('#GoalscorersList').val(val);
            return;
        }
        $('#GoalscorersList').val(currentGoalscorers + ", " + val);
    }
});

$("#guestPlayersDropdown").change(function () {
    debugger;
    $('#guestPlayersDropdown').val("0");
});