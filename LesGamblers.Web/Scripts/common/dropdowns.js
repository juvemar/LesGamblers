$("#gameDropdown").change(function () {
    var val = this.value;
    if (val != '') {
        $('#chooseGoalscorer').css("display", "");
        $("#btnChooseGoalscorer").attr("disabled", false);
        $("#btnChooseGoalscorer").attr("data-toggle", "collapse");
        $("#GoalscorersList").text = "";
        $("#all-goalscorers").children().remove();
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
            //var goalscorers = $("#GoalscorersList").val().split(',');
            //if (goalscorers.length > 0 && goalscorers[0].trim() != "") {
            //    goalscorers.forEach(function (element, index, array) {
            //        _buildGoalscorerDiv(element);
            //    })
            //}
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
        _fillHiddenGoalcorers(val);
        _buildGoalscorerDiv(val);
    }
});

$("#hostPlayersDropdown").click(function () {
    $('#hostPlayersDropdown').val("0");
});

$("#guestPlayersDropdown").change(function () {
    var val = this.value;
    if (val === '0') {
        return;
    }
    var currentView = $('#viewTitle').text();
    if (currentView == "Add your predictions") {
        $('#goalscorer').val(val.toString());
    } else if (currentView == "Update Game") {
        _fillHiddenGoalcorers(val);
        _buildGoalscorerDiv(val);
    }
});

$("#guestPlayersDropdown").change(function () {
    $('#guestPlayersDropdown').val("0");
});

function deleteScorer(el) {
    var that = $('#' + el.id);
    var goalscorerName = that.prev().prev().text().trim();
    that.parent().remove();

    debugger;
    var goalscorers = $("#GoalscorersList").val().split(',');
    var updatedGoalscorers = [];
    goalscorers.forEach(function (element, index, array) {
        if (element.localeCompare(goalscorerName) != 0) {
            updatedGoalscorers.push(element);
        }
    });

    $("#GoalscorersList").val(updatedGoalscorers.join());
}

function _buildGoalscorerDiv(val) {
    var date = new Date(),
        idDate = date.getHours().toString() + date.getMinutes().toString() + date.getSeconds().toString(),
        newDiv = document.createElement("div"),
        newSpan = document.createElement("span"),
        newSpanSeparator = document.createElement("span"),
        newHref = document.createElement("a");

    newSpan.style.fontWeight = 'bold';
    newSpan.style.fontSize = '16px';
    newSpan.style.color = "#044E96";
    newSpan.innerHTML = "&nbsp" + val;
    newDiv.appendChild(newSpan);

    newSpanSeparator.innerHTML = " | ";
    newDiv.appendChild(newSpanSeparator);

    newHref.className = "glyphicon glyphicon-trash";
    newHref.id = idDate;
    newHref.setAttribute("onclick", "deleteScorer(this);");
    newHref.style.textDecoration = 'none';
    newHref.style.color = "#ff0000"
    newHref.style.cursor = "pointer";
    newDiv.appendChild(newHref);

    $('#all-goalscorers').append(newDiv);
}

function _fillHiddenGoalcorers(val) {
    debugger;
    var currentGoalscorers = $('#GoalscorersList').val();
    if (currentGoalscorers === '') {
        $('#GoalscorersList').val(val);
        return;
    }
    $('#GoalscorersList').val(currentGoalscorers + "," + val);
}