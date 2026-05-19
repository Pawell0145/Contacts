using System.Windows;

namespace Lab_7_9_Contacts
{
    public partial class ContactAdd : Window
    {
        private bool _isEditMode;
        private Person _currentPerson;
        private AppDbContext _context;

        public ContactAdd(Person personToEdit = null)
        {
            InitializeComponent();
            
            _context = new AppDbContext();
            
            _currentPerson = personToEdit ?? new Person();
            _isEditMode = personToEdit != null;

            this.Title = _isEditMode ? "Edit Contact" : "Add Contact";
            
            this.DataContext = _currentPerson;
        }

        private void SaveContact_Click(object sender, RoutedEventArgs e)
        {
            //validation
            if (string.IsNullOrWhiteSpace(_currentPerson.Name) ||
                string.IsNullOrWhiteSpace(_currentPerson.City))
            {
                MessageBox.Show("Please fill in required fields (Name and City).", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (_currentPerson.Phone > 999999999 || _currentPerson.Phone < 100000000)
            {
                MessageBox.Show("Please enter a valid 9-digit phone number.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (_isEditMode)
            {
                // The entity is already bound to the UI, so it has the new values
                _context.People.Update(_currentPerson);
            }
            else
            {
                // Adding new person
                _context.People.Add(_currentPerson);
            }

            _context.SaveChanges();
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        protected override void OnClosed(System.EventArgs e)
        {
            _context?.Dispose();
            base.OnClosed(e);
        }
    }
}
