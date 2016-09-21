namespace LesGamblers.Web.Helper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using LesGamblers.Services.Contracts;
    using LesGamblers.Web.Models.Gamblers;
    using LesGamblers.Web.Models.Games;
    using LesGamblers.Web.Models.Predictions;

    public static class PointsUpdater
    {
        private static int currentPredictionPoints = 0;

        public static void CheckCorrectPredictions(UpdateFinishedGameViewModel model, IPredictionsService predictions, IGamblersService gamblers)
        {
            var realFinalResult = model.FinalResult.Split(new char[] { ':', '-' }).ToArray();
            var homeTeamGoals = int.Parse(realFinalResult[0]);
            var guestTeamGoals = int.Parse(realFinalResult[1]);

            var currentGamePredictions = predictions.GetAll().Where(p => p.GameId == model.Id).ToList();
            foreach (var prediction in currentGamePredictions)
            {
                currentPredictionPoints = 0;
                var finalResult = prediction.FinalResult.Split(new char[] { ':', '-' }).ToArray();
                var homeTeamGoalsPrediction = int.Parse(finalResult[0]);
                var guestTeamGoalsPrediction = int.Parse(finalResult[1]);
                var updatedPrediction = new UpdatePredictionPointsViewModel();

                updatedPrediction.FinalResultPredicted = CheckIfResultIsCorrectlyPredicted(homeTeamGoals, guestTeamGoals, homeTeamGoalsPrediction, guestTeamGoalsPrediction);
                if (!updatedPrediction.FinalResultPredicted)
                {
                    updatedPrediction.SignPredicted = CheckIfSignIsCorrectlyPredicted(homeTeamGoals, guestTeamGoals, homeTeamGoalsPrediction, guestTeamGoalsPrediction);
                }
                else
                {
                    updatedPrediction.SignPredicted = true;
                }

                var goalscorerPredictionPoints = CheckCorrectGoalscorer(model, prediction.Goalscorer);
                currentPredictionPoints += goalscorerPredictionPoints;
                if (goalscorerPredictionPoints > 0)
                {
                    updatedPrediction.GoalscorerPredicted = true;
                }

                var currentPrediction = predictions.GetById(prediction.Id).FirstOrDefault();
                updatedPrediction.TotalPoints = currentPredictionPoints;
                var dataModel = AutoMapper.Mapper.Map<UpdatePredictionPointsViewModel, LesGamblers.Models.Prediction>(updatedPrediction);

                predictions.UpdatePrediction(dataModel, prediction.Id);
            }
        }

        private static bool CheckIfSignIsCorrectlyPredicted(int homeTeamGoals, int guestTeamGoals, int homeTeamGoalsPrediction, int guestTeamGoalsPrediction)
        {
            if (homeTeamGoals == homeTeamGoalsPrediction && guestTeamGoals == guestTeamGoalsPrediction)
            {
                return true;
            }
            else if (homeTeamGoals == guestTeamGoals && homeTeamGoalsPrediction == guestTeamGoalsPrediction)
            {
                currentPredictionPoints += LesGamblers.Common.GlobalConstants.SignFinalResultOrGoalscorerPredictionPoints;
                return true;
            }
            else if (homeTeamGoals > guestTeamGoals && homeTeamGoalsPrediction > guestTeamGoalsPrediction)
            {
                currentPredictionPoints += LesGamblers.Common.GlobalConstants.SignFinalResultOrGoalscorerPredictionPoints;
                return true;
            }
            else if (homeTeamGoals < guestTeamGoals && homeTeamGoalsPrediction < guestTeamGoalsPrediction)
            {
                currentPredictionPoints += LesGamblers.Common.GlobalConstants.SignFinalResultOrGoalscorerPredictionPoints;
                return true;
            }

            return false;
        }

        private static bool CheckIfResultIsCorrectlyPredicted(int homeTeamGoals, int guestTeamGoals, int homeTeamGoalsPrediction, int guestTeamGoalsPrediction)
        {
            if (homeTeamGoals == homeTeamGoalsPrediction && guestTeamGoals == guestTeamGoalsPrediction)
            {
                currentPredictionPoints += LesGamblers.Common.GlobalConstants.ExactFinalResultPredictionPoints;
                return true;
            }

            return false;
        }

        private static int CheckCorrectGoalscorer(UpdateFinishedGameViewModel model, string predictedGoalscorer)
        {
            if (string.IsNullOrEmpty(predictedGoalscorer) && string.IsNullOrEmpty(model.Goalscorers))
            {
                return LesGamblers.Common.GlobalConstants.SignFinalResultOrGoalscorerPredictionPoints;
            }

            var actualGoalscorers = new string[predictedGoalscorer.Where(x => x == ',').Count() + 1];
            if (model.Goalscorers != null)
            {
                actualGoalscorers = model.Goalscorers.Trim().Split(new string[] { "," }, StringSplitOptions.None).ToArray();
            }

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
                    }
                    else
                    {
                        scorersGoalsCount[scorer]++;
                    }

                    if (mostGoals < scorersGoalsCount[scorer])
                    {
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