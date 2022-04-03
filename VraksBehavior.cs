using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Workshops;
using TaleWorlds.Core;

namespace SiegeofVraks
{
    public class VraksBehavior : CampaignBehaviorBase
    {
        public override void RegisterEvents()
        {
            CampaignEvents.OnWorkshopChangedEvent.AddNonSerializedListener(this, OWworkshopChangedEvent);
            CampaignEvents.DailyTickTownEvent.AddNonSerializedListener(this, DailiyTickTownEvent);
            CampaignEvents.LocationCharactersAreReadyToSpawnEvent.AddNonSerializedListener(this, this.LocationCharactersAreReadyToSpawnEvent);
        }

        private void LocationCharactersAreReadyToSpawnEvent(Dictionary<string, int> unusedUsablePointCount)
        {
            throw new NotImplementedException();
        }

        private void DailiyTickTownEvent(Town town)
        {
            foreach (var workshop in town.Workshops)
            {
                InformationManager.DisplayMessage(new InformationMessage(String.Format("{0} has a {1}", town.Name, workshop.Name)));
                workshop.ChangeGold(50);
                //if (workshop.WorkshopType.StringId == "brewery")
                //{
                //    workshop.ChangeGold(workshop.Expense * 0.15f);
                //}


            }
        }

        private void OWworkshopChangedEvent(Workshop workshop, Hero oldOwningHero, WorkshopType type)
        {
            throw new NotImplementedException();
        }

        public override void SyncData(IDataStore dataStore)
        {

        }
    }
}
