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
    public partial class Form1 : Form
    {
        private static string connectionString = "Server=localhost;Database=InventarioDB;Uid=root;Pwd=tu_contraseña;";
        public Form1()
        {
            InitializeComponent();
            ConsultarProductos();
        }


        private void ConsultarProductos()
        {
            string query = "SELECT Id, Nombre, Codigo, Precio, Categoria, Proveedor FROM Producto";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            listBox1.Items.Clear();

                            while (reader.Read())
                            {
                                string producto = $"ID: {reader["Id"]}, Nombre: {reader["Nombre"]}, Código: {reader["Codigo"]}, Precio: {reader["Precio"]}, Categoría: {reader["Categoria"]}, Proveedor: {reader["Proveedor"]}";
                                listBox1.Items.Add(producto);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al consultar los productos: {ex.Message}");
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();

            // Mostrar el nuevo formulario
            form2.Show();

            // Cerrar el formulario actual
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                string seleccion = listBox1.SelectedItem.ToString();
                string idProducto = seleccion.Split(',')[0].Replace("ID: ", "").Trim();

                string query = "DELETE FROM Producto WHERE Id = @Id";

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Id", idProducto);
                            command.ExecuteNonQuery();
                            MessageBox.Show("Producto eliminado exitosamente.");
                        }
                    }
                    ConsultarProductos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un producto para eliminar.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();

            // Mostrar el nuevo formulario
            form4.Show();

            // Cerrar el formulario actual
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();

            // Mostrar el nuevo formulario
            form3.Show();

            // Cerrar el formulario actual
            this.Close();
        }
    }
}
