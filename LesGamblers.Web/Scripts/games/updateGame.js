$("#gameDropdown").change(function () {
    var val = this.value;
    if (val != '') {
        $("#GoalscorersList").val('');
        $("#all-goalscorers").children().remove();
    }
});

$("#hostPlayersDropdown").change(function () {
    var val = this.value;
    if (val === '0') {
        return;
    }

    _fillHiddenGoalcorers(val);
    _buildGoalscorerDiv(val);
});

$("#guestPlayersDropdown").change(function () {
    var val = this.value;
    if (val === '0') {
        return;
    }

    _fillHiddenGoalcorers(val);
    _buildGoalscorerDiv(val);
});

function deleteScorer(el) {
    var that = $('#' + el.id);
    var goalscorerName = that.prev().prev().text().trim();
    that.parent().remove();

    var goalscorers = $("#GoalscorersList").val().split(',');
    var updatedGoalscorers = [];
    var removed = false;
    goalscorers.forEach(function (element, index, array) {
        if (!removed && element.localeCompare(goalscorerName) == 0) {
            removed = true;
        } else {
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
    var currentGoalscorers = $('#GoalscorersList').val();
    if (currentGoalscorers === '') {
        $('#GoalscorersList').val(val);
        return;
    }
    $('#GoalscorersList').val(currentGoalscorers + "," + val);
}