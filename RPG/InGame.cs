using RPG.Characters;

namespace RPG
{
    public class InGame
    {
        // 10x10 matrix
        // place hero on [1,1] and add monsters randomly
        // fight!

        // collection of monsters
        HashSet<Monster> Monsters = new HashSet<Monster>();

        public void BuildBoard(Entity hero)
        {
            char[,] board = new char[10, 10];
            hero.I = 1; // player row
            hero.J = 1; // player column

            // Initial spawn
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if(i == hero.I && j == hero.J)
                    {
                        board[i, j] = hero.BoardSymbol;
                    }
                    else
                    {
                        board[i, j] = '▒';
                    }
                }
            }
            board = SpawnEnemy(board, hero);

            ActionDisplay(board, hero);
            
        }
        public void ActionDisplay(char[,] board, Entity hero)
        {
            Console.WriteLine($"Health: {hero.Health}        Mana: {hero.Mana}");
            Console.WriteLine();

            // print board
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    Console.Write(board[i, j]);
                }
                Console.WriteLine();
            }

            // print options
            Console.WriteLine("Choose action");
            Console.WriteLine("1) Attack");
            Console.WriteLine("2) Move");
            Console.Write("Your pick: ");
            string input = Console.ReadLine();
            
            if(input == "1" || input == "2" || input == "3")
            {
                if (input == "1")
                    Attack(board, hero);
                else
                {
                    board = MoveHero(hero, board); // update board 
                }


                // Move monster on every move
                board = MoveEnemy(board, hero);// update board

                // check if monster can attack hero
                EnemyAttack(hero, board);

                // Spawn new monster on every move
                board = SpawnEnemy(board, hero); 

                // If hero's health is equal or less than 0, game over
                if (hero.Health <= 0)
                {
                    DeathScreen deathScreen = new DeathScreen();
                    deathScreen.Choose();
                }


                // display board again
                ActionDisplay(board, hero);
            }
            else
            {
                Console.WriteLine("Incorrect input!");
                // reload
                ActionDisplay(board, hero);
            }
        }

        private void EnemyAttack(Entity hero, char[,] board)
        {
            foreach (var monster in Monsters)
            {
                if(Math.Abs(monster.I - hero.I) <= monster.Range && Math.Abs(monster.J - hero.J) <= monster.Range)
                {
                    Console.WriteLine("A monster hit you!");
                    hero.Health -= monster.Damage;
                }
            }
        }

        public char[,] MoveHero(Entity hero, char[,] board)
        {
            // clear previous hero location
            board[hero.I, hero.J] = '▒';

            Console.Write("Command: ");
            string command = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(command))
            {
                switch (command.ToUpper())
                {
                    case "W":
                        if (hero.I - hero.Range >= 0)
                            hero.I -= hero.Range; // Move [hero.Range] rows up
                        else
                        {
                            Console.WriteLine("Cannot go there!");
                            // Reload
                            MoveHero(hero, board);
                        }
                        break;
                    case "S":
                        if (hero.I + hero.Range <= board.GetLength(0))
                            hero.I += hero.Range; // Move [hero.Range] rows down
                        else
                        {
                            Console.WriteLine("Cannot go there!");
                            // Reload
                            MoveHero(hero, board);
                        }
                        break;
                    case "D":
                        if (hero.J + hero.Range <= board.GetLength(1))
                            hero.J += hero.Range; // Move [hero.Range] cols right
                        else
                        {
                            Console.WriteLine("Cannot go there!");
                            // Reload
                            MoveHero(hero, board);
                        }
                        break;
                    case "A":
                        if (hero.J - hero.Range >= 0)
                            hero.J -= hero.Range; // Move [hero.Range] cols left
                        else
                        {
                            Console.WriteLine("Cannot go there!");
                            // Reload
                            MoveHero(hero, board);
                        }
                        break;
                    case "E":
                        if (hero.J + hero.Range <= board.GetLength(1) && hero.I - hero.Range >= 0)
                        {
                            hero.I -= hero.Range; // Move [hero.Range] rows up
                            hero.J += hero.Range; // Move [hero.Range] cols right
                        }
                        else
                        {
                            Console.WriteLine("Cannot go there!");
                            // Reload
                            MoveHero(hero, board);
                        }
                        break;
                    case "X":
                        if (hero.J + hero.Range <= board.GetLength(1) && hero.I + hero.Range <= board.GetLength(0))
                        {
                            hero.I += hero.Range; // Move [hero.Range] rows down
                            hero.J += hero.Range; // Move [hero.Range] cols right
                        }
                        else
                        {
                            Console.WriteLine("Cannot go there!");
                            // Reload
                            MoveHero(hero, board);
                        }
                        break;
                    case "Q":
                        if (hero.J - hero.Range >= 0 && hero.I - hero.Range >= 0)
                        {
                            hero.I -= hero.Range; // Move [hero.Range] rows up
                            hero.J -= hero.Range; // Move [hero.Range] cols left
                        }
                        else
                        {
                            Console.WriteLine("Cannot go there!");
                            // Reload
                            MoveHero(hero, board);
                        }
                        break;
                    case "Z":
                        if (hero.J - hero.Range >= 0 && hero.I + hero.Range <= board.GetLength(0))
                        {
                            hero.I += hero.Range; // Move [hero.Range] rows down
                            hero.J -= hero.Range; // Move [hero.Range] cols left
                        }
                        else
                        {
                            Console.WriteLine("Cannot go there!");
                            // Reload
                            MoveHero(hero, board);
                        }
                        break;
                    default: 
                        Console.WriteLine("Incorrect input!");
                        MoveHero(hero, board);
                        break;
                }
            }
            else
            {
                Console.WriteLine("Incorrect input!");
                MoveHero(hero, board);
            }

            // update current hero location
            board[hero.I, hero.J] = hero.BoardSymbol;

            return board;
        }

        public void Attack(char[,] board, Entity hero)
        {
            int count = 0;
            int lowestHealth = int.MaxValue;
            foreach (var monster in Monsters)
            {
                // check if monster is in range
                if (Math.Abs(hero.I - monster.I) <= hero.Range && Math.Abs(hero.J - monster.J) <= hero.Range)
                {
                    count++;

                    if(monster.Health < lowestHealth)
                    {
                        lowestHealth = monster.Health;
                    }
                }
            }

            if(count != 0)
            {
                Console.WriteLine($"There are {count} monsters in range! The lowest monster health is: {lowestHealth}");
                Console.Write("Attack? Y\\N: ");
                string input = Console.ReadLine();

                if (!string.IsNullOrEmpty(input))
                {
                    if(input == "Y")
                    {
                        try
                        {
                            Monster monster = Monsters.FirstOrDefault(x => x.Health == lowestHealth);

                            if (monster != null)
                            {
                                monster.Health -= hero.Damage;

                                if (monster.Health <= 0)
                                {
                                    try
                                    {
                                        // remove monster from board
                                        board[monster.I, monster.J] = '▒';

                                        // remove monster from collection
                                        Monsters.Remove(monster);

                                        Console.WriteLine("A monster has been killed!");
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e.Message);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine($"A monster has been hit! Their current health is: {monster.Health}");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Monster does not exist!");
                            }

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                }
                else
                    Console.WriteLine("Your swung missed! Next time be more accurate!");
            }
            else
                Console.WriteLine("There are no monsters nearby!");
        }

        public char[,] SpawnEnemy(char[,] board, Entity hero)
        {
            // spawn enemy
            Monster monster = new Monster();

            // add monster to collection
            Monsters.Add(monster);

            // random location
            Random rng = new Random();
            monster.I = rng.Next(0, board.GetLength(0) - 1);
            monster.J = rng.Next(0, board.GetLength(1) - 1);

            // if monster is about to be spawned on top of the hero, reload
            if(monster.I == hero.I && monster.J == hero.J || board[monster.I, monster.J] != '▒')
                SpawnEnemy(board, hero);

            // place monster on board
            board[monster.I, monster.J] = monster.BoardSymbol;

            return board;
        }

        public char[,] MoveEnemy(char[,] board, Entity hero)
        {
            foreach (var monster in Monsters)
            {
                // if the monster is in range of the hero, don't move
                if (monster.I == hero.I && monster.J == hero.J)
                    continue;
                else
                {
                    board[monster.I, monster.J] = '▒';

                    if (hero.I > monster.I && monster.I - monster.Range >= 0)
                    {
                        if(monster.I + monster.Range != hero.I)
                            monster.I += monster.Range; // go down
                    }
                    if (hero.I < monster.I && monster.I + monster.Range <= board.GetLength(0))
                    {
                        if(monster.I - monster.Range != hero.I)
                            monster.I -= monster.Range; // go up
                    }
                    if (hero.J > monster.J && monster.J + monster.Range <= board.GetLength(1))
                    {
                        if(monster.J + monster.Range != hero.J)
                            monster.J += monster.Range; // go right
                    }
                    if (hero.J < monster.J && monster.J - monster.Range >= 0)
                    {
                        if(monster.J - monster.Range != hero.J)
                            monster.J -= monster.Range; // go left
                    }

                    board[monster.I, monster.J] = monster.BoardSymbol;
                }
            }
            return board;
        }
    }
}
