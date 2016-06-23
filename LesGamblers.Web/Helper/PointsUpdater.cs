namespace LesGamblers.Web.Helper
{
    using System;
    using System.Linq;

    using LesGamblers.Services.Contracts;
    using LesGamblers.Web.Models.Games;
    using LesGamblers.Web.Models.Gamblers;

    public static class PointsUpdater
    {
        public static void CorrectPredictionsCheck(UpdateFinishedGameViewModel model, IPredictionsService predictions, IGamblersService gamblers)
        {
            var realFinalResult = model.FinalResult.Split(new char[] { ' ', ':', '-' }).ToArray();
            var homeTeamGoals = int.Parse(realFinalResult[0]);
            var guestTeamGoals = int.Parse(realFinalResult[1]);

            var currentGamePredictions = predictions.GetAll().Where(p => p.GameId == model.Id).ToList();

            foreach (var prediction in currentGamePredictions)
            {
                var currentPredictionPoints = 0;
                var finalResult = prediction.FinalResult.Split(new char[] { ' ', ':', '-' }).ToArray();
                var homeTeamGoalsPrediction = int.Parse(finalResult[0]);
                var guestTeamGoalsPrediction = int.Parse(finalResult[1]);

                if (homeTeamGoals == homeTeamGoalsPrediction && guestTeamGoals == guestTeamGoalsPrediction)
                {
                    currentPredictionPoints += LesGamblers.Common.GlobalConstants.ExactFinalResultPredictionPoints;
                }
                else if (homeTeamGoals == guestTeamGoals && homeTeamGoalsPrediction == guestTeamGoalsPrediction)
                {
                    currentPredictionPoints += LesGamblers.Common.GlobalConstants.SignFinalResultOrGoalscorerPredictionPoints;
                }
                else if (homeTeamGoals > guestTeamGoals && homeTeamGoalsPrediction > guestTeamGoalsPrediction)
                {
                    currentPredictionPoints += LesGamblers.Common.GlobalConstants.SignFinalResultOrGoalscorerPredictionPoints;
                }
                else if (homeTeamGoals < guestTeamGoals && homeTeamGoalsPrediction < guestTeamGoalsPrediction)
                {
                    currentPredictionPoints += LesGamblers.Common.GlobalConstants.SignFinalResultOrGoalscorerPredictionPoints;
                }

                var goalscorerPredictedCorrectly = model.GoalscorersList.Split(new string[] { ", " }, StringSplitOptions.None).ToArray().Contains(prediction.Goalscorer);
                if (goalscorerPredictedCorrectly)
                {
                    currentPredictionPoints += LesGamblers.Common.GlobalConstants.SignFinalResultOrGoalscorerPredictionPoints;
                }

                var currentGambler = gamblers.GetById(prediction.GamblerId);
                var updatedGambler = new UpdateGamblerViewModel();
                updatedGambler.TotalPoints += currentPredictionPoints;
            }
        }
    }
}