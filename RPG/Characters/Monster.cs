namespace RPG.Characters
{
    class Monster : Entity
    {
        private int range = 1;
        private char boardSymbol = 'M'; // I changed the monster symbol because it wouldn't show on the console
        static Random rng = new Random();
        public override int Strength { get { return rng.Next(1, 3); } }
        public override int Agility { get { return rng.Next(1, 3); } }
        public override int Intelligence { get { return rng.Next(1, 3); } }
        public override int Range { get { return this.range; } }
        public override char BoardSymbol { get { return this.boardSymbol; } }
    }
}
