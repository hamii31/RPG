
namespace RPG.Characters
{
    class Mage : Entity
    {
        private int strength = 2;
        private int agility = 1;
        private int intelligence = 3;
        private int range = 3;
        private char boardSymbol = '*';
        public override int Strength { get { return this.strength; } set { this.strength = value; } }
        public override int Agility { get { return this.agility; } set { this.agility = value; } }
        public override int Intelligence { get { return this.intelligence; } set { this.intelligence = value; } }
        public override int Range { get { return this.range; } }
        public override char BoardSymbol { get { return this.boardSymbol; } }
    }

}
