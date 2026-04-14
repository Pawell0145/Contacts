using Contacts;
using System.Windows;

namespace Lab_7_9_Contacts
{
    public partial class ContactAdd : Window
    {
        private bool _isEditMode;
        private Person _personToEdit;

        public ContactAdd(Person personToEdit = null)
        {
            InitializeComponent();
            
            _personToEdit = personToEdit;
            _isEditMode = _personToEdit != null;

            if (_isEditMode)
            {
                NameTextBox.Text = _personToEdit.Name;
                AgeTextBox.Text = _personToEdit.Age.ToString();
                CityTextBox.Text = _personToEdit.City;
                PhoneTextBox.Text = _personToEdit.Phone.ToString();
                this.Title = "Edit Contact";
            }
            else
            {
                this.Title = "Add Contact";
            }
        }

        private void SaveContact_Click(object sender, RoutedEventArgs e)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(NameTextBox.Text) ||
                string.IsNullOrWhiteSpace(AgeTextBox.Text) ||
                string.IsNullOrWhiteSpace(CityTextBox.Text) ||
                string.IsNullOrWhiteSpace(PhoneTextBox.Text))
            {
                MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!int.TryParse(AgeTextBox.Text, out int age) || !int.TryParse(PhoneTextBox.Text, out int phone))
            {
                MessageBox.Show("Please enter a valid number for Age and Phone.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if ( phone > 999999999 || phone < 100000000)
            {
                MessageBox.Show("Please enter a valid 9-digit phone number.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (_isEditMode)
            {
                _personToEdit.Name = NameTextBox.Text;
                _personToEdit.Age = age;
                _personToEdit.City = CityTextBox.Text;
                _personToEdit.Phone = phone;

                if (Application.Current.MainWindow is MainWindow mainWindow)
                {
                    mainWindow.RefreshGrid();
                }
            }
            else
            {
                var newPerson = new Person
                {
                    Name = NameTextBox.Text,
                    Age = age,
                    City = CityTextBox.Text,
                    Phone = phone
                };

                if (Application.Current.MainWindow is MainWindow mainWindow)
                {
                    mainWindow.AddPerson(newPerson);
                }
            }

            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
