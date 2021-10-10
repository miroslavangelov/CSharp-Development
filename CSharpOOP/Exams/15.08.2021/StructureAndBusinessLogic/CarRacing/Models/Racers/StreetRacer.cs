using CarRacing.Models.Cars.Contracts;

namespace CarRacing.Models.Racers
{
    public class StreetRacer : Racer
    {
        private const string StreetRacerRacerRacingBehavior = "aggressive";
        private const int StreetRacerDrivingExperience = 10;

        public StreetRacer(string username, ICar car)
            : base(username, StreetRacerRacerRacingBehavior, StreetRacerDrivingExperience, car)
        {
        }

        public override void Race()
        {
            base.Race();
            DrivingExperience += 5;
        }
    }
}