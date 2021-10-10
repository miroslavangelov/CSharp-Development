using CarRacing.Models.Cars.Contracts;

namespace CarRacing.Models.Racers
{
    public class ProfessionalRacer : Racer
    {
        private const string ProfessionalRacerRacingBehavior = "strict";
        private const int ProfessionalRacerDrivingExperience = 30;
        
        public ProfessionalRacer(string username, ICar car)
            : base(username, ProfessionalRacerRacingBehavior, ProfessionalRacerDrivingExperience, car)
        {
        }

        public override void Race()
        {
            base.Race();
            DrivingExperience += 10;
        }
    }
}