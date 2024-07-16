
using RPG.Characters;
using RPG.Data;
using RPG.Data.Models;

namespace RPG
{
    public class CharacterSelect
    {
        public void ChooseCharacter()
        {
            Console.WriteLine("Choose character type:");
            Console.WriteLine("Options:");
            Console.WriteLine("1) Warrior");
            Console.WriteLine("2) Mage");
            Console.WriteLine("3) Archer");
            Console.Write("Your pick: ");

            string input = Console.ReadLine();

            // check if input is as expected
            if (!int.TryParse(input, out _) || string.IsNullOrEmpty(input))
            {
                Console.WriteLine("The input you entered is incorrect!");
                ChooseCharacter();
            }

            switch (input)
            {
                case "1":
                    Warrior warrior = new Warrior();

                    Console.WriteLine("Would you like to buff up your stats before starting?                    (Limit: 3 points total)");

                    Console.Write("Response (Y\\N): ");

                    input = Console.ReadLine();

                    if (input.ToUpper() == "Y")
                        BuffUp(warrior);
                    else
                        Console.WriteLine("Starting game without additional buffs!");


                    // Save to DB 
                    SaveCharacter(warrior, "Warrior");

                    // Continue to InGame
                    InGame game = new InGame();
                    game.BuildBoard(warrior);

                    break;
                case "2":
                    Mage mage = new Mage();

                    Console.WriteLine("Would you like to buff up your stats before starting?                    (Limit: 3 points total)");

                    Console.Write("Response (Y\\N): ");

                    input = Console.ReadLine();

                    if (input.ToUpper() == "Y")
                        BuffUp(mage);
                    else
                        Console.WriteLine("Starting game without additional buffs!");

                    // Save to DB 
                    SaveCharacter(mage, "Mage");

                    // Continue to InGame
                    game = new InGame();
                    game.BuildBoard(mage);

                    break;
                case "3":
                    Archer archer = new Archer();

                    Console.WriteLine("Would you like to buff up your stats before starting?                    (Limit: 3 points total)");

                    Console.Write("Response (Y\\N): ");

                    input = Console.ReadLine();

                    if (input.ToUpper() == "Y")
                        BuffUp(archer);
                    else
                        Console.WriteLine("Starting game without additional buffs!");

                    // Save to DB 
                    SaveCharacter(archer, "Archer");

                    // Continue to InGame
                    game = new InGame();
                    game.BuildBoard(archer);

                    break;
                default:
                    Console.WriteLine("The number doesn't match an existing hero!");

                    // Reset
                    ChooseCharacter();
                    break;
            }
        }

        private void SaveCharacter(Entity hero, string name)
        {
            using (var context = new CharacterContext())
            {
                var character = new Character { Id = Guid.NewGuid(), Name = name, Health = hero.Health, Mana = hero.Mana, Damage = hero.Damage, CreatedOn = DateTime.Now };
                context.Characters.Add(character);
                context.SaveChanges();
            }
        }

        private void BuffUp(Entity hero)
        {
            int points = 3;

            try
            {
                Console.WriteLine($"Remaining points: {points}");
                Console.Write("Add to Strength: ");
                int add = int.Parse(Console.ReadLine());

                if(add < points)
                {
                    hero.Strength += add;
                    points -= add;

                    Console.Write("Add to Agility: ");
                    add = int.Parse(Console.ReadLine());

                    if (add < points)
                    {
                        hero.Agility += add;
                        points -= add;

                        Console.Write("Add to Intelligence: ");
                        add = int.Parse(Console.ReadLine());

                        hero.Intelligence += add;

                    }
                    else
                    {
                        // skip the rest
                        hero.Agility += add;
                    }

                }
                else
                {
                    // skip the rest 
                    hero.Strength += points;
                }

                // Update stats
                hero.UpdateStats();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                BuffUp(hero);
            }
        }
    }
}
