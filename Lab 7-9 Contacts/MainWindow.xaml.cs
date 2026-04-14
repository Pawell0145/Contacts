using Lab_7_9_Contacts;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Contacts
{
    public partial class MainWindow : Window
    {
        // The main collection of people
        private List<Person> _allPeople;

        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            // Initializing the collection
            _allPeople = new List<Person>
            {
                new Person { Name = "Alice", Age = 28, City = "New York", Phone = 123456789 },
                new Person { Name = "Bob", Age = 35, City = "London", Phone = 987654321 },
                new Person { Name = "Charlie", Age = 42, City = "Paris", Phone = 456789123 },
                new Person { Name = "Diana", Age = 22, City = "New York", Phone = 789123456 },
                new Person { Name = "Eve", Age = 30, City = "Tokyo", Phone = 321654987 }
            };

            ContactsGrid.ItemsSource = _allPeople;
        }



        // 1. Filtering Example
        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            // LINQ: Where (Filters the list to only include people older than 30)
            var filtered = _allPeople.Where(p => p.Age > 30).ToList();

            ContactsGrid.ItemsSource = filtered;
            ResultText.Text = $"Filtering: Found {filtered.Count} people over 30.";
        }

        private void Filter_Below30(object sender, RoutedEventArgs e)
        {
            var filtered = _allPeople.Where(p => p.Age < 30).ToList();

            ContactsGrid.ItemsSource = filtered;
            ResultText.Text = $"Filtering: Found {filtered.Count} people below 30.";
        }

        private void Filter_Equal30(object sender, RoutedEventArgs e)
        {
            var filtered = _allPeople.Where(p => p.Age == 30).ToList();

            ContactsGrid.ItemsSource = filtered;
            ResultText.Text = $"Filtering: Found {filtered.Count} people who have 30.";
        }

        // 2a. Sorting Example (Ascending)
        private void SortAsc_Click(object sender, RoutedEventArgs e)
        {
            // LINQ: OrderBy (Sorts alphabetically by Name)
            var sortedAsc = _allPeople.OrderBy(p => p.Name).ToList();

            ContactsGrid.ItemsSource = sortedAsc;
            ResultText.Text = "Sorting: Ordered by Name (Ascending).";
        }

        private void SortByAge_Click(object sender, RoutedEventArgs e)
        {
            // LINQ: OrderBy (Sorts by Age in ascending order)
            var sortedByAge = _allPeople.OrderBy(p => p.Age).ToList();
            ContactsGrid.ItemsSource = sortedByAge;
            ResultText.Text = "Sorting: Ordered by Age (Ascending).";
        }

        // 2b. Sorting Example (Descending)
        private void SortDesc_Click(object sender, RoutedEventArgs e)
        {
            // LINQ: OrderByDescending (Sorts reverse-alphabetically by Name)
            var sortedDesc = _allPeople.OrderByDescending(p => p.Name).ToList();

            ContactsGrid.ItemsSource = sortedDesc;
            ResultText.Text = "Sorting: Ordered by Name (Descending).";
        }

        private void SortByAgeDesc_Click(object sender, RoutedEventArgs e)
        {
            // LINQ: OrderByDescending (Sorts by Age in descending order)
            var sortedByAgeDesc = _allPeople.OrderByDescending(p => p.Age).ToList();
            ContactsGrid.ItemsSource = sortedByAgeDesc;
            ResultText.Text = "Sorting: Ordered by Age (Descending).";
        }

        // 3. Transformation (Projection) Example
        private void Project_Click(object sender, RoutedEventArgs e)
        {
            // LINQ: Select (Extracts only the 'Name' property, transforming Person into string)
            var names = _allPeople.Select(p => p.Name).ToList();

            ResultText.Text = "Projection (Names only): " + string.Join(", ", names);
        }

        private void Project_City_Click(object sender, RoutedEventArgs e)
        {
            // LINQ: Select (Extracts only the 'City' property, transforming Person into string)
            var cities = _allPeople.Select(p => p.City).ToList();
            ResultText.Text = "Projection (Cities only): " + string.Join(", ", cities);
        }

        private void Project_NameAge_Click(object sender, RoutedEventArgs e)
        {
            // LINQ: Select (Extracts 'Name' and 'Age' properties, transforming Person into an anonymous type)
            var nameAge = _allPeople.Select(p => new { p.Name, p.Age }).ToList();
            ResultText.Text = "Projection (Name and Age): " + string.Join(", ", nameAge.Select(na => $"{na.Name} ({na.Age})"));
        }

        // 4. Quantifiers / Checking Example
        private void Check_Click(object sender, RoutedEventArgs e)
        {
            // LINQ: Any (Checks if at least one element satisfies the condition)
            bool hasNewYorker = _allPeople.Any(p => p.City == "New York");

            ResultText.Text = $"Quantifier: Is anyone from New York? {hasNewYorker}";
        }

        private void Check_AllAbove20_Click(object sender, RoutedEventArgs e)
        {
            // LINQ: All (Checks if all elements satisfy the condition)
            bool allAbove20 = _allPeople.All(p => p.Age > 20);
            ResultText.Text = $"Quantifier: Are all people above 20? {allAbove20}";
        }

        private void Check_AnyAbove40_Click(object sender, RoutedEventArgs e)
        {
            // LINQ: Any (Checks if at least one element satisfies the condition)
            bool anyAbove40 = _allPeople.Any(p => p.Age > 40);
            ResultText.Text = $"Quantifier: Is anyone above 40? {anyAbove40}";
        }

        // 5. Aggregation Example
        private void Aggregate_Click(object sender, RoutedEventArgs e)
        {
            // LINQ: Average (Calculates the average of the 'Age' property)
            double avgAge = _allPeople.Average(p => p.Age);

            ResultText.Text = $"Aggregation: The average age is {avgAge:F1} years.";
        }

        private void Aggregate_Count_Click(object sender, RoutedEventArgs e)
        {
            // LINQ: Count (Counts the number of people in the collection)
            int count = _allPeople.Count();
            ResultText.Text = $"Aggregation: There are {count} people in the contact list.";
        }

        private void Aggregate_Max_Click(object sender, RoutedEventArgs e)
        {
            // LINQ: Max (Finds the maximum age in the collection)
            int maxAge = _allPeople.Max(p => p.Age);
            ResultText.Text = $"Aggregation: The oldest person is {maxAge} years old.";
        }

        // Reset the view to the default list
        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            ContactsGrid.ItemsSource = _allPeople;
            ResultText.Text = string.Empty;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            new ContactAdd().ShowDialog();
        }

        // Refresh the DataGrid to reflect changes in the collection
        public void RefreshGrid()
        {
            ContactsGrid.ItemsSource = null;
            ContactsGrid.ItemsSource = _allPeople;
        }

        // Method to add a new person to the collection
        public void AddPerson(Person person)
        {
            _allPeople.Add(person);
            RefreshGrid();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (ContactsGrid.SelectedItem is Person selectedPerson)
            {
                var editWindow = new ContactAdd(selectedPerson);
                editWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select a contact to edit.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            
            if (ContactsGrid.SelectedItem is Person selectedPerson)
            {
                _allPeople.Remove(selectedPerson);
                ContactsGrid.ItemsSource = null; // Refresh the grid
                ContactsGrid.ItemsSource = _allPeople;
                ResultText.Text = $"Deleted: {selectedPerson.Name}";
            }
            else
            {
                MessageBox.Show("Please select a contact to delete.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


    }

    // Data Model
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
        public int Phone { get; set; }
    }

    public class Contact { }

}