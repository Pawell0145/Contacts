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
                new Person { Name = "Alice", Age = 28, City = "New York" },
                new Person { Name = "Bob", Age = 35, City = "London" },
                new Person { Name = "Charlie", Age = 42, City = "Paris" },
                new Person { Name = "Diana", Age = 22, City = "New York" },
                new Person { Name = "Eve", Age = 30, City = "Tokyo" }
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

        // 2a. Sorting Example (Ascending)
        private void SortAsc_Click(object sender, RoutedEventArgs e)
        {
            // LINQ: OrderBy (Sorts alphabetically by Name)
            var sortedAsc = _allPeople.OrderBy(p => p.Name).ToList();

            ContactsGrid.ItemsSource = sortedAsc;
            ResultText.Text = "Sorting: Ordered by Name (Ascending).";
        }

        // 2b. Sorting Example (Descending)
        private void SortDesc_Click(object sender, RoutedEventArgs e)
        {
            // LINQ: OrderByDescending (Sorts reverse-alphabetically by Name)
            var sortedDesc = _allPeople.OrderByDescending(p => p.Name).ToList();

            ContactsGrid.ItemsSource = sortedDesc;
            ResultText.Text = "Sorting: Ordered by Name (Descending).";
        }

        // 3. Transformation (Projection) Example
        private void Project_Click(object sender, RoutedEventArgs e)
        {
            // LINQ: Select (Extracts only the 'Name' property, transforming Person into string)
            var names = _allPeople.Select(p => p.Name).ToList();

            ResultText.Text = "Projection (Names only): " + string.Join(", ", names);
        }

        // 4. Quantifiers / Checking Example
        private void Check_Click(object sender, RoutedEventArgs e)
        {
            // LINQ: Any (Checks if at least one element satisfies the condition)
            bool hasNewYorker = _allPeople.Any(p => p.City == "New York");

            ResultText.Text = $"Quantifier: Is anyone from New York? {hasNewYorker}";
        }

        // 5. Aggregation Example
        private void Aggregate_Click(object sender, RoutedEventArgs e)
        {
            // LINQ: Average (Calculates the average of the 'Age' property)
            double avgAge = _allPeople.Average(p => p.Age);

            ResultText.Text = $"Aggregation: The average age is {avgAge:F1} years.";
        }

        // Reset the view to the default list
        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            ContactsGrid.ItemsSource = _allPeople;
            ResultText.Text = string.Empty;
        }
    }

    // Data Model
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
    }
}