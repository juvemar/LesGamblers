namespace LesGamblers.Web.Helper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using LesGamblers.Services.Contracts;
    using LesGamblers.Web.Models.Games;
    using LesGamblers.Web.Models.Gamblers;

    public static class PointsUpdater
    {
        public static void CheckCorrectPredictions(UpdateFinishedGameViewModel model, IPredictionsService predictions, IGamblersService gamblers)
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
                var updatedGambler = new UpdateGamblerViewModel();

                if (homeTeamGoals == homeTeamGoalsPrediction && guestTeamGoals == guestTeamGoalsPrediction)
                {
                    currentPredictionPoints += LesGamblers.Common.GlobalConstants.ExactFinalResultPredictionPoints;
                    updatedGambler.FinalResultsPredicted++;
                }
                else if (homeTeamGoals == guestTeamGoals && homeTeamGoalsPrediction == guestTeamGoalsPrediction)
                {
                    currentPredictionPoints += LesGamblers.Common.GlobalConstants.SignFinalResultOrGoalscorerPredictionPoints;
                    updatedGambler.SignsPredicted++;
                }
                else if (homeTeamGoals > guestTeamGoals && homeTeamGoalsPrediction > guestTeamGoalsPrediction)
                {
                    currentPredictionPoints += LesGamblers.Common.GlobalConstants.SignFinalResultOrGoalscorerPredictionPoints;
                    updatedGambler.SignsPredicted++;
                }
                else if (homeTeamGoals < guestTeamGoals && homeTeamGoalsPrediction < guestTeamGoalsPrediction)
                {
                    currentPredictionPoints += LesGamblers.Common.GlobalConstants.SignFinalResultOrGoalscorerPredictionPoints;
                    updatedGambler.SignsPredicted++;
                }

                var goalscorerPredictionPoints = CheckCorrectGoalscorer(model, prediction.Goalscorer);
                currentPredictionPoints += goalscorerPredictionPoints;
                if (goalscorerPredictionPoints > 0)
                {
                    updatedGambler.GoalscorersPredicted++;
                }

                var currentGambler = gamblers.GetById(prediction.GamblerId);
                updatedGambler.TotalPoints += currentPredictionPoints;
                var dataModel = AutoMapper.Mapper.Map<UpdateGamblerViewModel, LesGamblers.Models.Gambler>(updatedGambler);

                gamblers.UpdateGambler(dataModel, currentGambler.Id);
            }
        }

        private static int CheckCorrectGoalscorer(UpdateFinishedGameViewModel model, string predictedGoalscorer)
        {
            var actualGoalscorers = model.GoalscorersList.Trim().Split(new string[] { ", " }, StringSplitOptions.None).ToArray();
            var goalscorerPredictedCorrectly = actualGoalscorers.Contains(predictedGoalscorer);
            if (goalscorerPredictedCorrectly)
            {
                var scorersGoalsCount = new Dictionary<string, int>();
                var mostGoals = 0;
                foreach (var scorer in actualGoalscorers)
                {
                    if (!scorersGoalsCount.ContainsKey(scorer))
                    {
                        scorersGoalsCount.Add(scorer, 1);
                        if (mostGoals < 1)
	                    {
		                     mostGoals = 1;
	                    }
                    }
                    else
                    {
                        scorersGoalsCount[scorer]++;
                        mostGoals = scorersGoalsCount[scorer]; 
                    }
                }

                var topScorers = scorersGoalsCount.Where(x => x.Value == mostGoals).Select(x => x.Key).ToList();
                if (topScorers.Contains(predictedGoalscorer))
                {
                    return LesGamblers.Common.GlobalConstants.SignFinalResultOrGoalscorerPredictionPoints;                    
                }
            }

            return 0;
        }
    }
}