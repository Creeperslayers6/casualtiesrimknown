using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse.AI;

namespace CasualtiesRimknown.MentalStates
{
    public class MentalState_SelfharmEvent : MentalState
    {
        public override RandomSocialMode SocialModeMax()
        {
            return RandomSocialMode.Off;
        }

        public void Notify_FinishedAction()
        {
            RecoverFromState();
        }
    }
}
