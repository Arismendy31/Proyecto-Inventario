using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FinalProyect
{
    public partial class Form4 : Form
    {
        private static string connectionString = "Server=localhost;Database=InventarioDB;Uid=root;Pwd=tu_contraseña;";

        public Form4()
        {
            InitializeComponent();
            CargarCategorias();
        }

        private void CargarCategorias()
        {
            // Consulta SQL para obtener todas las categorías
            string query = "SELECT Nombre, Descripcion FROM Categoria";

            try
            {
                // Limpiar el ListBox antes de agregar nuevos datos
                listBox1.Items.Clear();

                // Crear la conexión
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Abrir la conexión
                    connection.Open();

                    // Crear el comando SQL con la consulta y la conexión
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Ejecutar la consulta y obtener un SqlDataReader
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Recorrer los resultados
                            while (reader.Read())
                            {
                                // Obtener los valores de las columnas
                                string nombre = reader["Nombre"].ToString();
                                string descripcion = reader["Descripcion"].ToString();

                                // Agregar el resultado al ListBox
                                listBox1.Items.Add($"Nombre: {nombre}, Descripción: {descripcion}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Mostrar mensaje de error en caso de excepción
                MessageBox.Show($"Error: {ex.Message}");
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            // Consulta SQL con parámetros
            string query = "INSERT INTO Categoria (Nombre, Descripcion) VALUES (@Nombre, @Descripcion)";

            try
            {
                // Crear la conexión
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Abrir la conexión
                    connection.Open();

                    // Crear el comando SQL con la consulta y la conexión
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Agregar los parámetros a la consulta desde los TextBox
                        command.Parameters.AddWithValue("@Nombre", textBox1.Text); // Aquí capturas el texto del TextBox txtNombre
                        command.Parameters.AddWithValue("@Descripcion", textBox3.Text); // Aquí capturas el texto del TextBox txtDescripcion

                        // Ejecutar el comando
                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Categoría creada exitosamente.");
                Form1 form1 = new Form1();

                // Mostrar el nuevo formulario
                form1.Show();

                // Cerrar el formulario actual
                this.Close();
            }
            catch (Exception ex)
            {
                // Mostrar mensaje de error en caso de excepción
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                // Obtener el texto completo del elemento seleccionado
                string seleccion = listBox1.SelectedItem.ToString();

                // Extraer el nombre de la categoría del texto seleccionado
                // Supongamos que el formato en el ListBox es "Nombre: NombreCategoria, Descripción: DescripciónCategoria"
                string nombreCategoria = seleccion.Split(',')[0].Replace("Nombre: ", "").Trim();

                // Consulta SQL para eliminar
                string query = "DELETE FROM Categoria WHERE Nombre = @Nombre";

                try
                {
                    // Crear la conexión
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        // Abrir la conexión
                        connection.Open();

                        // Crear el comando SQL con la consulta
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            // Agregar el parámetro del nombre
                            command.Parameters.AddWithValue("@Nombre", nombreCategoria);

                            // Ejecutar el comando
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Categoría eliminada exitosamente.");
                            }
                            else
                            {
                                MessageBox.Show("No se encontró la categoría.");
                            }
                        }
                    }

                    // Actualizar la lista después de eliminar
                    CargarCategorias();
                }
                catch (Exception ex)
                {
                    // Mostrar mensaje de error en caso de excepción
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona una categoría para eliminar.");
            }
        }
    }
}
