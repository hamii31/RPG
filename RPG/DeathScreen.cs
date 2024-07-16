namespace RPG
{
    public class DeathScreen
    {
        public void Choose()
        {
            Console.WriteLine("Game Over!");
            Console.WriteLine("1) Restart");
            Console.WriteLine("2) Quit");
            string input = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(input))
            {
                if(input == "1")
                {
                    MainMenu.Start();
                }
                else if (input == "2")
                {
                    Console.WriteLine("Thanks for playing!");
                    Environment.Exit(0);
                }
                else
                    Choose();


            }
            else
                Choose();
        }
    }
}
