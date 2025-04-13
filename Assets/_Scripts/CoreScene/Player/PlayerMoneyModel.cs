using System;

namespace _Scripts.CoreScene.Player
{
    public class PlayerMoneyModel
    {
        private int m_money = 0;
        private int m_moneyOnBoard = 0;

        public int Money => m_money;
        
        public int MoneyOnBoard => m_moneyOnBoard;
        
        public void AddMoneyOnBoard(int value)
        {
            m_moneyOnBoard += value;
            OnMoneyOnBoardChanged?.Invoke();
        }
        
        public void SubMoneyOnBoard(int value)
        {
            m_moneyOnBoard -= value;
            OnMoneyOnBoardChanged?.Invoke();
        }
        
        public void AddMoney(int value)
        {
            m_money += value;
            OnMoneyChanged?.Invoke();
        }

        public event Action OnMoneyChanged;

        public event Action OnMoneyOnBoardChanged;
    }
}