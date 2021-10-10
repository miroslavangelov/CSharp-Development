using CarRacing.Models.Maps.Contracts;
using CarRacing.Models.Racers.Contracts;
using CarRacing.Utilities.Messages;

namespace CarRacing.Models.Maps
{
    public class Map : IMap
    {
        public string StartRace(IRacer racerOne, IRacer racerTwo)
        {
            if (!racerOne.IsAvailable() && !racerTwo.IsAvailable())
            {
                return OutputMessages.RaceCannotBeCompleted;
            }

            if (racerOne.IsAvailable() && !racerTwo.IsAvailable())
            {
                return string.Format(OutputMessages.OneRacerIsNotAvailable, racerOne.Username, racerTwo.Username);
            }

            if (!racerOne.IsAvailable() && racerTwo.IsAvailable())
            {
                return string.Format(OutputMessages.OneRacerIsNotAvailable, racerTwo.Username, racerOne.Username);

            }
            
            racerOne.Race();
            racerTwo.Race();

            double racerOneRacingBehaviorMultiplier = racerOne.RacingBehavior == "strict" ? 1.2 : 1.1;
            double racerOneChanceOfWinning =
                racerOne.Car.HorsePower * racerOne.DrivingExperience * racerOneRacingBehaviorMultiplier;
            double racerTwoRacingBehaviorMultiplier = racerTwo.RacingBehavior == "strict" ? 1.2 : 1.1;
            double racerTwoChanceOfWinning =
                racerTwo.Car.HorsePower * racerTwo.DrivingExperience * racerTwoRacingBehaviorMultiplier;
            string winner = racerOneChanceOfWinning > racerTwoChanceOfWinning ? racerOne.Username : racerTwo.Username;

            return string.Format(OutputMessages.RacerWinsRace, racerOne.Username, racerTwo.Username, winner);
        }
    }
}