$("#btnCheckPrediction").click(function () {
    _loadPredictionDetails();
});

function _loadPredictionDetails() {
    var gameId = $('#gamesDropdown').val();
    var gamblerUsername = $('#gamblersDropdown').val();
    if (!gameId || !gamblerUsername || gameId === '' || gamblerUsername === '') {
        return;
    }
    var prediction = {
        gameId: gameId,
        gamblerUsername: gamblerUsername
    };

    $("#LoadingImage").show();
    $.ajax({
        type: "GET",
        url: "/Predictions/_CheckPredictionDetails",
        data: prediction,
        success: function (view) {
            $("#LoadingImage").hide();
            $("#predictionDetails").html(view);
        },
        error: function (errorData) {
            debugger;
            onError(errorData);
        }
    });
};