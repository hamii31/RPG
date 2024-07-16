namespace RPG.Characters
{
    public class Entity
    {
        virtual public int Strength { get; set; }
        virtual public int Agility {  get; set; }
        virtual public int Intelligence {  get; set; }

        virtual public int Range { get; set; } 

        virtual public char BoardSymbol { get; set; }

        public int Health;
        public int Mana;
        public int Damage;

        public int I {  get; set; }
        public int J { get; set; }

        public Entity()
        {
            this.Setup();
        }

        private void Setup()
        {
            this.Health = this.Strength * 5;
            this.Mana = this.Intelligence * 3;
            this.Damage = this.Agility * 2;
        }

        public void UpdateStats()
        {
            this.Setup();
        }
    }
}
