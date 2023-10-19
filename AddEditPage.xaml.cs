﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IsmagilovAutoservice
{
    /// <summary>
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {

        private Service _currentService = new Service();

        public AddEditPage(Service SelectedService)
        {
            InitializeComponent();

            if(SelectedService != null)
               _currentService = SelectedService;

            DataContext = _currentService;
            _currentService.Стоимость = 0;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentService.Наименование_услуги))
            {
                errors.AppendLine("Укажите название услуги");
            }
           
            if (_currentService.Стоимость == 0)
            {
                
                errors.AppendLine("Укажите стоимость услуги");
            }
            
            if (string.IsNullOrWhiteSpace(Convert.ToString(_currentService.Действующая_скидка)))
            {
                _currentService.Действующая_скидка = 0;
                errors.AppendLine("Укажите скидку");
            }
            if(_currentService.Действующая_скидка < 0 || _currentService.Действующая_скидка > 99)
            {
                errors.AppendLine("Укажите корректный размер скидки услуги");
            }
                if (string.IsNullOrWhiteSpace(_currentService.Длительность))
            {
                errors.AppendLine("Укажите длительность услуги");
            }
            if(errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if(_currentService.ID == 0)
            {
                Ismagilov_autoserviceEntities2.GetContext().Service.Add(_currentService);
            }

            try
            {
                Ismagilov_autoserviceEntities2.GetContext().SaveChanges();
                MessageBox.Show("информация сохранена");
                Manager.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
