using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;

namespace Prog122_S24_Midterm
{
    // Define the Product class
    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    // Define the Order class
    public class Order
    {
        public string CustomerName { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();

        public string FormattedOrder()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine($"Customer: {CustomerName}");
            builder.AppendLine("Products:");
            foreach (var product in Products)
            {
                builder.AppendLine($"- {product.Name}: ${product.Price}");
            }
            return builder.ToString();
        }
    }

    // Define the Inventory class
    public class Inventory
    {
        public List<Product> CoffeeProducts { get; set; } = new List<Product>();
        public List<Product> TeaProducts { get; set; } = new List<Product>();
        public List<Product> BreakfastProducts { get; set; } = new List<Product>();
    }

    public partial class MainWindow : Window
    {
        List<Order> previousOrders = new List<Order>(); // Will hold all completed orders
        Inventory inventory = new Inventory(); // Used to access list of products
        Order currentOrder; // Class scope variable to easily pass the current order around.

        public MainWindow()
        {
            InitializeComponent();

            // Attach previous orders to your combo box ItemsSource
            comboBoxPreviousOrders.ItemsSource = previousOrders;

            // Initialize a new Order to your current order
            currentOrder = new Order();
        }

        #region Button Click Events

        // Click event for Dark Coffee button
        private void btnDarkCoffee_Click(object sender, RoutedEventArgs e)
        {
            AddProductToOrder(inventory.CoffeeProducts[0]); // Assuming Dark Roast Coffee is the first item
        }

        // Click event for Green Tea button
        private void btnGreenTea_Click(object sender, RoutedEventArgs e)
        {
            AddProductToOrder(inventory.TeaProducts[1]); // Assuming Green Tea is the second item
        }

        // Click event for Fruit Breakfast button
        private void btnFruit_Click(object sender, RoutedEventArgs e)
        {
            AddProductToOrder(inventory.BreakfastProducts[2]); // Assuming Fruit is the third item
        }

        // Click event for Complete Purchase button
        private void btnCompletePurchase_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCustomerName.Text))
            {
                // Add name to order
                currentOrder.CustomerName = txtCustomerName.Text;

                // Add current order to previous order list
                previousOrders.Add(currentOrder);

                // Display formatted order in previous orders RichTextBox
                DisplayFormattedOrder(previousOrders.Last(), richTextBoxPreviousOrders);

                // Clear UI elements
                ClearUI();

                // Create a new instance of Order for currentOrder
                currentOrder = new Order();

                // Refresh the combo box
                comboBoxPreviousOrders.Items.Refresh(); // If it works you should see the last order appear in the drop down when you click on it
            }
            else
            {
                MessageBox.Show("Please enter a name.");
            }
        }

        #endregion

        #region Combo Box Selection Changed Event

        // Combo box selection changed event
        private void comboBoxPreviousOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxPreviousOrders.SelectedItem != null)
            {
                Order selectedOrder = comboBoxPreviousOrders.SelectedItem as Order;
                DisplayFormattedOrder(selectedOrder, richTextBoxPreviousOrderDetails);
            }
        }

        #endregion

        #region Helper Methods

        // Method to add a product to the current order
        private void AddProductToOrder(Product product)
        {
            currentOrder.Products.Add(product);
            DisplayFormattedOrder(currentOrder, richTextBoxCurrentOrderDetails);
        }

        // Method to display the formatted order in a RichTextBox
        private void DisplayFormattedOrder(Order order, RichTextBox richTextBox)
        {
            richTextBox.Document.Blocks.Clear();
            richTextBox.AppendText(order.FormattedOrder());
        }

        // Method to clear UI elements
        private void ClearUI()
        {
            richTextBoxCurrentOrderDetails.Document.Blocks.Clear();
            richTextBoxPreviousOrderDetails.Document.Blocks.Clear();
            txtCustomerName.Clear();
        }

        #endregion

        private void btnSandwich_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
