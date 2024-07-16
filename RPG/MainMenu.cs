using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RPG.Data;

namespace RPG
{
    public class MainMenu
    {
        static void Main(string[] args)
        {
            // Configure the Db
            var services = new ServiceCollection();
            services.AddDbContext<CharacterContext>(options =>
                options.UseSqlServer("Data Source=.;Initial Catalog=RPG2024;User ID=sa;Password=H@meleon3109;TrustServerCertificate=True;"));
            var serviceProvider = services.BuildServiceProvider();
            var dbContext = serviceProvider.GetService<CharacterContext>();

            // Start Game
             Start();
        }

        public static void Start()
        {
            // Game Start
            Console.WriteLine("Welcome!");
            Console.WriteLine("Press any key to play.");
            Console.ReadLine();

            try
            {
                CharacterSelect characterSelect = new CharacterSelect();
                characterSelect.ChooseCharacter();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Start();
            }
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CharacterContext>(options =>
                options.UseSqlServer("Data Source=.;Initial Catalog=RPG2024;User ID=sa;Password=H@meleon3109;TrustServerCertificate=True;"));
        }
    }
}