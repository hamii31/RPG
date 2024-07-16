namespace RPG.Characters
{
    class Warrior : Entity
    {
        private int strength = 3;
        private int agility = 3;
        private int intelligence = 0;
        private int range = 1;
        private char boardSymbol = '@';
        public override int Strength { get {return this.strength;} set { this.strength = value;} }
        public override int Agility { get { return this.agility; } set { this.agility = value; } }
        public override int Intelligence { get { return this.intelligence; } set { this.intelligence = value; } }
        public override int Range { get { return this.range; } }
        public override char BoardSymbol { get { return this.boardSymbol; } }
    }
}
