using Microsoft.Data.SqlClient;

namespace ProjetTestIntegration
{
    public class MyIntegrationTest : IDisposable
    {
        private SqlConnection _connection;
        private SqlTransaction _transaction;

        public MyIntegrationTest()
        {
            // Connect to the test database
            _connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=\"Base de donnees pour test d'intégration\";Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            _connection.Open();

            // Start a transaction
            _transaction = _connection.BeginTransaction();
        }

        public void Dispose()
        {
            // Rollback the transaction to undo any changes made during the test
            _transaction.Rollback();

            // Close the connection
            _connection.Close();
        }

        [Fact]
        public void MyIntegrationTestMethod()
        {
            // Perform some database operation
            SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM MyTable", _connection, _transaction);
            int count = (int)command.ExecuteScalar();

            // Assert that the result is correct
            Assert.Equal(10, count);
        } }
       

//namespace MyProject.Tests.Integration
   // {
        public class ProductIntegrationTests
        {
            private const string ConnectionString = "Data Source=(local);Initial Catalog=MyDatabase;Integrated Security=True;";

            [Fact]
            public void Test_CreateProduct_AddsProductToCartAndStock()
            {
                // Arrange
                var productName = "My Product";
                var productPrice = 9.99m;
                var productDescription = "très utile";
                var productStock = 10;
            var productDetails = "vendu à l'unité";
    
            // Act
            var productId = CreateProduct(productName, productPrice, productDescription, productStock, productDetails);

                // Assert
                AssertProductInCart(productId);
                AssertProductInStock(productId);
            }

            private int CreateProduct(string name, decimal price, string description, int stock, string details)
            {
                int productId;
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    var command = new SqlCommand("INSERT INTO Products (Name, Price,Description,Stock,Details) OUTPUT INSERTED.Id VALUES (@Name, @Price,@Description, @Stock, @Details)", connection);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Price", price);
                    command.Parameters.AddWithValue("@Description", description);
                    command.Parameters.AddWithValue("@Stock", stock);
                    command.Parameters.AddWithValue("@Details", details);

                    productId = (int)command.ExecuteScalar();
                }
                return productId;
            }

            private void AssertProductInCart(int productId)
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    var command = new SqlCommand("SELECT COUNT(*) FROM CartItems WHERE ProductId = @ProductId", connection);
                    command.Parameters.AddWithValue("@ProductId", productId);
                    var count = (int)command.ExecuteScalar();
                    Assert.Equal(1, count);
                }
            }

            private void AssertProductInStock(int productId)
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    var command = new SqlCommand("SELECT COUNT(*) FROM StockItems WHERE ProductId = @ProductId", connection);
                    command.Parameters.AddWithValue("@ProductId", productId);
                    var count = (int)command.ExecuteScalar();
                    Assert.Equal(1, count);
                }
            }
        }
    }

    