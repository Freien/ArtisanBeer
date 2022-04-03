using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Diamond;
using TaleWorlds.MountAndBlade.View.Missions;
using TaleWorlds.ObjectSystem;

namespace SiegeofVraks
{
    public class VraksMissionView : MissionView
    {
        private Boolean qPressed = false;
        private float timeonPressed = 0;





        public override void OnMissionScreenTick(float dt)
        {
            base.OnMissionScreenTick(dt);

            float currentTime = ((float)Mission.Current.MissionTimeTracker.NumberOfTicks) / 10000000;



            if (Input.IsKeyPressed(TaleWorlds.InputSystem.InputKey.Q) && qPressed == false)
            {
                HealSelf();
                timeonPressed = currentTime;
                currentTime = 0;
                qPressed = true;
            }

            if (currentTime -timeonPressed >= 10)
            {
                qPressed = false;

                //InformationManager.DisplayMessage(new InformationMessage("Ready to heal."));

            }
            //else
            //{

            //    InformationManager.DisplayMessage(new InformationMessage((currentTime /= 10000000).ToString()));

            //}

        }

        private void HealSelf()
        {


            if(Mission.Mode is MissionMode.Duel)
            {
                InformationManager.DisplayMessage(new InformationMessage("No loyal servant of the Emperor would ever heal in a duel."));

                return;

            }

            // check if has flag
            var itemRoster = MobileParty.MainParty.ItemRoster;

            var vraksFlagObject = MBObjectManager.Instance.GetObject<ItemObject>("krieg_banner_001");
            

            if (itemRoster.GetItemNumber(vraksFlagObject) <= 0)
            {
                InformationManager.DisplayMessage(new InformationMessage("You don't have any healing items."));

                return;
            }
                            
             

            var ma = Mission.MainAgent;
            if(ma.Health == ma.HealthLimit)
            {
                InformationManager.DisplayMessage(new InformationMessage("You haven't yet lost health in the Emperor's service.")); 
                
                return;

            }
                      
            //remove flag
            itemRoster.AddToCounts(vraksFlagObject, -1);

            //increase hp
            var oldHealth = ma.Health;
            Mission.MainAgent.Health += 20;
            if (ma.Health > ma.HealthLimit) ma.Health = ma.HealthLimit;
            InformationManager.DisplayMessage(new InformationMessage(String.Format("By his Divine Grace you get healed by {0}", Mission.MainAgent.Health -oldHealth)));

        }
    }
    }
