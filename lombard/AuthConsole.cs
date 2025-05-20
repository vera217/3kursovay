using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using logic.Facade;

namespace lombard
{
    class AuthConsole
    {
        private readonly IAuthFacade _facade;

        public AuthConsole(IAuthFacade facade)
        {
            _facade = facade;
        }

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(" ><((°> -- ДОБРО ПОЖАЛОВАТЬ В ЛОМАБРД ЗОЛОТАЯ РЫБКА -- <°))><  \n \n1. Вход\n2. Регистрация\n3. Выход");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Login();
                        break;
                    case "2":
                        Register();
                        break;
                    case "3":
                        return;
                }
            }
        }

        private void Login()
        {
            Console.Write("Логин: ");
            var username = Console.ReadLine();
            Console.Write("Пароль: ");
            var password = Console.ReadLine();

            var user = _facade.Login(username, password);
            if (user == null)
            {
                Console.WriteLine("Ошибка входа!");
                return;
            }

            if (user.role == "employee")
                new AdminConsole((IAdminFacade)_facade, user.username).Run();
            else
                new ClientConsole((IClientFacade)_facade, user.username).Run();
        }

        private void Register()
        {
            string clientId;
            while (true)
            {
                Console.Write("Паспортные данные (только цифры): ");
                clientId = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(clientId) || !clientId.All(char.IsDigit))
                {
                    Console.WriteLine("Ошибка: паспортные данные должны содержать только цифры.");
                    Console.WriteLine("Нажмите любую клавишу для возврата в меню.");
                    Console.ReadKey();
                    return;
                }
                break;
            }

            string phone;
            while (true)
            {
                Console.Write("Телефон (только цифры): ");
                phone = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(phone) || !phone.All(char.IsDigit))
                {
                    Console.WriteLine("Ошибка: телефон должен содержать только цифры.");
                    Console.WriteLine("Нажмите любую клавишу для возврата в меню.");
                    Console.ReadKey();
                    return;
                }
                break;
            }

            Console.Write("Email: ");
            var email = Console.ReadLine();

            string password;
            while (true)
            {
                Console.Write("Пароль (не менее 5 символов): ");
                password = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(password) || password.Length < 5)
                {
                    Console.WriteLine("Ошибка: пароль должен быть не менее 5 символов.");
                    Console.WriteLine("Нажмите любую клавишу для возврата в меню.");
                    Console.ReadKey();
                    return;
                }
                break;
            }

            if (_facade.RegisterClient(clientId, phone, email, password))
                Console.WriteLine("Регистрация успешна!");
            else
                Console.WriteLine("Ошибка регистрации!");
        }

    }
}
