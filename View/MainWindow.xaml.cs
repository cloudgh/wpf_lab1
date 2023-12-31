﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using lab1.Model;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;
using static System.Reflection.Metadata.BlobBuilder;
using System.Windows.Media;
using System.Text;

namespace lab1
{
    public partial class MainWindow : Window
    {
        private CollectionViewSource usersCollectionView, booksCollectionView;
        List<User> user = new List<User>()
        {
            new User(0, "John", "Smith", "john.smith@example.com"),
            new User(1, "Emily", "Johnson", "emily.johnson@example.com"),
            new User(2, "Michael", "Davis", "michael.davis@example.com"),
            new User(3, "Sarah", "Wilson", "sarah.wilson@example.com"),
            new User(4, "Michael", "Brown", "michael.brown@example.com"),
            new User(5, "Olivia", "Miller", "olivia.miller@example.com"),
            new User(6, "James", "Wilson", "james.wilson@example.com"),
            new User(7, "Emma", "Moore", "emma.moore@example.com"),
            new User(8, "Liam", "Taylor", "liam.taylor@example.com")
        };
        List<Book> book = new List<Book>()
        {
            new Book(0, "George Orwell", "1984", new DateTime(1949, 6, 8), 5),
            new Book(1, "Jane Austen", "Pride and Prejudice", new DateTime(1813, 1, 28), 8),
            new Book(2, "F. Scott Fitzgerald", "The Great Gatsby", new DateTime(1925, 4, 10), 7),
            new Book(3, "J.K. Rowling", "Harry Potter and the Philosopher's Stone", new DateTime(1997, 6, 26), 6),
            new Book(4, "Hermann Hesse", "Siddhartha", new DateTime(1922, 9, 16), 4),
            new Book(5, "Leo Tolstoy", "War and Peace", new DateTime(1869, 1, 1), 9),
            new Book(6, "Fyodor Dostoevsky", "Crime and Punishment", new DateTime(1866, 11, 14), 3),
            new Book(7, "Gabriel Garcia Marquez", "One Hundred Years of Solitude", new DateTime(1967, 5, 30), 2),
            new Book(8, "Mark Twain", "The Adventures of Tom Sawyer", new DateTime(1876, 12, 1), 1)
        };
        private List<UserBook> userBooks = new List<UserBook>();
        private void ShowSuccessMessage(string userName, string bookTitle, int quantity, string action)
        {
            StringBuilder userBooksStringBuilder = new StringBuilder("Список выданных книг:\n");
            foreach (var record in userBooks)
            {
                userBooksStringBuilder.AppendLine($"Пользователь: {record.User.FullName}, Книга: {record.Book.Title}, Количество: {record.Quantity} шт.");
            }

            MessageBox.Show($"Книга \"{bookTitle}\" успешно {action} пользователем {userName}.\n\n{userBooksStringBuilder}", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        public MainWindow()
        {
            InitializeComponent();

            usersCollectionView = new CollectionViewSource();
            usersCollectionView.Source = user;
            ListUsers.ItemsSource = usersCollectionView.View;

            booksCollectionView = new CollectionViewSource();
            booksCollectionView.Source = book;
            ListBook.ItemsSource = booksCollectionView.View;

            UserComboBox.DisplayMemberPath = "FullName";
            UserComboBox.ItemsSource = user;
            BookComboBox.DisplayMemberPath = "FullBook";
            BookComboBox.ItemsSource = book;
            ListUsersBook.ItemsSource = userBooks;
        }

       
        private void FilterUsers_name(string filterText)
        {
            usersCollectionView.View.Filter = item =>
            {
                if (item is User user)
                {
                    return user.Name.IndexOf(filterText, StringComparison.OrdinalIgnoreCase) >= 0;
                }
                return false;
            };
        }
        private void InputTextName_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterUsers_name(InputTextName.Text);
           
        }
        


        private void FilterUsers_lastname(string filterText)
        {
            usersCollectionView.View.Filter = item =>
            {
                if (item is User user)
                {
                    return user.Lastname.IndexOf(filterText, StringComparison.OrdinalIgnoreCase) >= 0;
                }
                return false;
            };
        }
        private void InputTextLastname_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterUsers_lastname(InputTextLastname.Text);
        }



        private void FilterBooks_author(string filterText)
        {
            booksCollectionView.View.Filter = item =>
            {
                if (item is Book book)
                {
                    return book.Author.IndexOf(filterText, StringComparison.OrdinalIgnoreCase) >= 0;
                }
                return false;
            };
        }
        private void InputTextAuthor_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterBooks_author(InputTextAuthor.Text);
        }

        private void FilterBooks_title(string filterText)
        {
            booksCollectionView.View.Filter = item =>
            {
                if (item is Book book)
                {
                    return book.Title.IndexOf(filterText, StringComparison.OrdinalIgnoreCase) >= 0;
                }
                return false;
            };
        }

       

        private void InputTextTitle_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterBooks_title(InputTextTitle.Text);
        }
        private void UpdateUI()
        {
            ListUsersBook.Items.Refresh();
            ListBook.Items.Refresh();
            BookComboBox.Items.Refresh();
        }
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            if (UserComboBox.SelectedItem is User selectedUser && BookComboBox.SelectedItem is Book selectedBook && inputValue > 0 && selectedBook.Count >= inputValue)
            {
                var userBookRecord = userBooks.FirstOrDefault(record => record.User == selectedUser && record.Book == selectedBook);
                if (userBookRecord != null)
                {
                    selectedBook.Count -= inputValue;
                    userBookRecord.Quantity += inputValue;
                }
                else
                {
                    selectedBook.Count -= inputValue;
                    userBooks.Add(new UserBook(selectedUser, selectedBook, inputValue));
                }
                
                ShowSuccessMessage(selectedUser.FullName, selectedBook.Title, inputValue, "выдана");
            }
            else
            {
                MessageBox.Show("Упс. Похоже, вы не можете выдать книгу. Проверьте корректность введенных значений.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            UpdateUI();
        }

        int inputValue;

        private void GiveBtn_Click(object sender, RoutedEventArgs e)
        {
            MainTabControl.SelectedIndex = 3;
        }

        private void BooksBtn_Click(object sender, RoutedEventArgs e)
        {
            MainTabControl.SelectedIndex = 2;
        }

        private void UsersBtn_Click(object sender, RoutedEventArgs e)
        {
            MainTabControl.SelectedIndex = 1;
        }

        private void ButtonReturn_Click(object sender, RoutedEventArgs e)
        {
            if (UserComboBox.SelectedItem is User selectedUser && BookComboBox.SelectedItem is Book selectedBook && inputValue > 0)
            {
                var userBookRecord = userBooks.FirstOrDefault(record => record.User == selectedUser && record.Book == selectedBook);
                if (userBookRecord != null && inputValue <= userBookRecord.Quantity)
                {
                    selectedBook.Count += inputValue;
                    userBookRecord.Quantity -= inputValue;

                    if (userBookRecord.Quantity <= 0)
                    {
                        userBooks.Remove(userBookRecord);
                    }
                    ShowSuccessMessage(selectedUser.FullName, selectedBook.Title, inputValue, "возвращена");
                }
                else
                {
                    MessageBox.Show("Вы не можете вернуть больше книг, чем брали, или данный пользователь не брал данную книгу.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Упс. Похоже, вы не можете выполнить возврат книги. Проверьте корректность введенных значений.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            UpdateUI();
        }

       

        private void InputTCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            string inputText = InputCount.Text;
            
            if (int.TryParse(inputText, out inputValue))
            {
                
            }
        }

    }
}
