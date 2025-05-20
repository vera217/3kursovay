using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using logic.Facade;
using logic.Services;
using model;

namespace lombard
{
    class AdminConsole
    {
        private readonly IAdminFacade _facade;
        private readonly string _adminUsername;
        public AdminConsole(IAdminFacade facade, string adminUsername)
        {
            _facade = facade;
            _adminUsername = adminUsername;
        }

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("АДМИНИСТРАТОР");
                Console.WriteLine("1. Просмотреть товары");
                Console.WriteLine("2. Добавить товар");
                Console.WriteLine("3. Удалить товар");
                Console.WriteLine("4. Оценить товар");
                Console.WriteLine("5. Назад");
                Console.WriteLine("6. Выход");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowAllItems();
                        break;
                    case "2":
                        AddItem();
                        break;
                    case "3":
                        DeleteItem();
                        break;
                    case "4":
                        AppraiseItem(_adminUsername);
                        break;
                    case "5":
                        return;
                    case "6":
                        return;
                }
            }
        }

        private void ShowAllItems()
        {
            var items = _facade.GetAllItems();
            Console.WriteLine("\nСписок товаров:");
            foreach (var item in items)
            {
                Console.WriteLine($"ID: {item.ItemId}, Название: {item.ItemName}, Цена: {item.price}, Фото: {item.PhotoPath}");
            }
            Console.WriteLine("\nНажмите любую клавишу...");
            Console.ReadKey();
        }

        private void AddItem()
        {
            Console.WriteLine("\nДобавление нового товара:");
            var item = new Items();

            Console.Write("Введите название: ");
            item.ItemName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(item.ItemName) || item.ItemName.Length < 3 || !item.ItemName.All(char.IsLetter))
            {
                Console.WriteLine("Ошибка: Название должно содержать только буквы и быть не короче 3 символов.");
                Console.WriteLine("Нажмите любую клавишу для возврата в меню.");
                Console.ReadKey();
                return;
            }

            Console.Write("Введите путь к фото: ");
            item.PhotoPath = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(item.PhotoPath))
            {
                Console.WriteLine("Ошибка: Путь к фото не может быть пустым.");
                Console.WriteLine("Нажмите любую клавишу для возврата в меню.");
                Console.ReadKey();
                return;
            }

            Console.Write("Введите цену: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal price) || price <= 0)
            {
                Console.WriteLine("Ошибка: Цена должна быть положительным числом.");
                Console.WriteLine("Нажмите любую клавишу для возврата в меню.");
                Console.ReadKey();
                return;
            }
            item.price = price;

            Console.Write("Укажите склад (Склад 1, Склад 2, Склад 3): ");
            item.StorageLocation = Console.ReadLine();
            if (item.StorageLocation != "Склад 1" && item.StorageLocation != "Склад 2" && item.StorageLocation != "Склад 3")
            {
                Console.WriteLine("Ошибка: Доступны только 'Склад 1', 'Склад 2', 'Склад 3'.");
                Console.WriteLine("Нажмите любую клавишу для возврата в меню.");
                Console.ReadKey();
                return;
            }

            item.EmployeeId = _adminUsername;

            Console.Write("Укажите айди клиента: ");
            item.ClientId = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(item.ClientId) || !item.ClientId.All(char.IsDigit))
            {
                Console.WriteLine("Ошибка: ID клиента должен содержать только цифры.");
                Console.WriteLine("Нажмите любую клавишу для возврата в меню.");
                Console.ReadKey();
                return;
            }

            item.DateAdded = DateTime.UtcNow;

            if (_facade.AddItem(item))
                Console.WriteLine("Товар успешно добавлен!");
            else
                Console.WriteLine("Ошибка при добавлении товара!");

            Console.ReadKey();
        }

        private void DeleteItem()
        {
            var items = _facade.GetAllItems();
            if (items.Count == 0)
            {
                Console.WriteLine("Нет товаров для удаления.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nСписок товаров:");
            foreach (var item in items)
            {
                Console.WriteLine($"ID: {item.ItemId}, Название: {item.ItemName}, Цена: {item.price}");
            }

            Console.Write("\nВведите ID товара для удаления: ");
            string input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input) || !int.TryParse(input, out int id) || id < 0)
            {
                Console.WriteLine("Ошибка: ID должен быть целым неотрицательным числом.");
                Console.WriteLine("Нажмите любую клавишу для возврата в меню.");
                Console.ReadKey();
                return;
            }

            if (_facade.DeleteItem(id))
                Console.WriteLine("Товар успешно удален!");
            else
                Console.WriteLine("Ошибка при удалении товара!");

            Console.ReadKey();
        }

        private void AppraiseItem(string employeeId)
        {
            var itemsForAppraisal = _facade.GetItemsForAppraisal(); 

            if (itemsForAppraisal.Count == 0)
            {
                Console.WriteLine("Нет товаров, доступных для оценки.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nСписок товаров для оценки:");
            foreach (var item in itemsForAppraisal)
            {
                Console.WriteLine($"ID: {item.ItemId}, Название: {item.ItemName}, Фото: {item.PhotoPath}");
            }

            int id;
            while (true)
            {
                Console.Write("\nВведите ID товара для оценки: ");
                string idInput = Console.ReadLine();

                if (!int.TryParse(idInput, out id))
                {
                    Console.WriteLine("Ошибка: введите корректный числовой ID.");
                    Console.WriteLine("Нажмите любую клавишу для возврата в меню.");
                    Console.ReadKey();
                    return;
                }

                if (!itemsForAppraisal.Any(i => i.ItemId == id))
                {
                    Console.WriteLine("Ошибка: такого ID нет в списке доступных товаров.");
                    Console.WriteLine("Нажмите любую клавишу для возврата в меню.");
                    Console.ReadKey();
                    return;
                }
                break;
            }

            decimal price;
            while (true)
            {
                Console.Write("Введите стоимость (положительное число): ");
                string priceInput = Console.ReadLine();

                if (!decimal.TryParse(priceInput, out price) || price <= 0)
                {
                    Console.WriteLine("Ошибка: введите корректную положительную стоимость.");
                    Console.WriteLine("Нажмите любую клавишу для возврата в меню.");
                    Console.ReadKey();
                    return;
                }
                break;
            }

            bool success = _facade.AppraiseItem(id, price, employeeId);
            if (success)
                Console.WriteLine("Товар успешно оценен!");
            else
                Console.WriteLine("Ошибка при оценке товара!");

            Console.ReadKey();
        }


    }
}
