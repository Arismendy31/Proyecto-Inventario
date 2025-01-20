using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalProyect
{
    public partial class Form2 : Form
    {
        private static string connectionString = "Server=localhost;Database=InventarioDB;Uid=root;Pwd=tu_contraseña;";
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nombre = textBox1.Text.Trim();
            string codigo = textBox1.Text.Trim();
            decimal precio = decimal.Parse(textBox3.Text.Trim());
            decimal cantidad = decimal.Parse(textBox4.Text.Trim());
            string categoria = comboBox1.Text.Trim();
            string proveedor = comboBox2.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(codigo))
            {
                MessageBox.Show("Por favor, completa todos los campos.");
                return;
            }

            CrearProducto(nombre, codigo, precio,cantidad, categoria, proveedor);
        }

        private void CrearProducto(string nombre, string codigo, decimal precio, decimal cantidad, string categoria, string proveedor)
        {
            string query = "INSERT INTO Producto (Nombre, Codigo, Precio, Cantidad, Categoria, Proveedor) VALUES (@Nombre, @Codigo, @Precio, @Cantidad, @Categoria, @Proveedor)";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Nombre", nombre);
                        command.Parameters.AddWithValue("@Codigo", codigo);
                        command.Parameters.AddWithValue("@Precio", precio);
                        command.Parameters.AddWithValue("@Catidad", cantidad);
                        command.Parameters.AddWithValue("@Categoria", categoria);
                        command.Parameters.AddWithValue("@Proveedor", proveedor);

                        command.ExecuteNonQuery();
                        MessageBox.Show("Producto creado exitosamente.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
