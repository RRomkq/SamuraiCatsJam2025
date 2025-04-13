// -------------------------------------------------------------------------
// Unauthorized copying of this file, via any medium is strictly prohibited.
// Proprietary and confidential.
// Copyright (c) 2019-2024 Gear Games, LTD. All rights reserved.
// -------------------------------------------------------------------------

using _Scripts.CoreScene.Player;
using _Scripts.CoreScene.Speech;
using _Scripts.CoreScene.Speech.Model;
using Cysharp.Threading.Tasks;

namespace _Scripts.CoreScene.Enviroment
{
    public class GhostStartTransferManager
    {
        private LevelGhostsRepository m_ghostsRepository;
        private BoatGhostsPositionHelper m_boatPositionHelper;
        private FinisPirsPositionHelper m_finishPositionHelper;
        private PlayerMoneyController m_playerMoneyController;

        public GhostStartTransferManager(LevelGhostsRepository mGhostsRepository,
            BoatGhostsPositionHelper mBoatPositionHelper,
            FinisPirsPositionHelper mFinishPositionHelper,
            PlayerMoneyController playerMoneyController)
        {
            m_ghostsRepository = mGhostsRepository;
            m_boatPositionHelper = mBoatPositionHelper;
            m_finishPositionHelper = mFinishPositionHelper;
            m_playerMoneyController = playerMoneyController;
        }

        public async UniTask TransferGhostsFromStartToBoat()
        {
            m_boatPositionHelper.Init();
            
            foreach (var ghost in m_ghostsRepository.SpawnedGhosts)
            {
                m_boatPositionHelper.Spawn(ghost);

                ghost.GetComponent<SpeechActor>().SpeechSituation = SpeechSituation.BOAT;
                await m_playerMoneyController
                    .AddMoneyOnBoard(2);
                
                await UniTask.Delay(200);
            }
        }

        public async UniTask TransferGhostsFromBoatToFinish()
        {
            foreach (var ghost in m_ghostsRepository.SpawnedGhosts)
            {
                m_finishPositionHelper.Spawn(ghost);

                ghost.GetComponent<SpeechActor>().SpeechSituation = SpeechSituation.LAST_PATH;
                
                await UniTask.Delay(1000);
            }
        }
    }
}