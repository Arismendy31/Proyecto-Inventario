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
    public partial class Form3 : Form
    {
        private static string connectionString = "Server=localhost;Database=InventarioDB;Uid=root;Pwd=tu_contraseña;";
        public Form3()
        {
            InitializeComponent();
            CargarProveedores();
        }

        private void CargarProveedores()
        {
            // Consulta SQL para obtener los datos de la tabla Proveedores
            string query = "SELECT ID, Nombre, Direccion, Contacto, Telefono FROM Proveedores";

            try
            {
                // Crear la conexión
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Abrir la conexión
                    connection.Open();

                    // Crear el comando SQL
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Ejecutar la consulta y obtener los resultados
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Limpiar el ListBox antes de llenarlo
                            listBox1.Items.Clear();

                            // Leer cada fila de la consulta y agregarla al ListBox
                            while (reader.Read())
                            {
                                string proveedor = $"ID: {reader["ID"]}, Nombre: {reader["Nombre"]}, Dirección: {reader["Direccion"]}, Contacto: {reader["Contacto"]}, Teléfono: {reader["Telefono"]}";
                                listBox1.Items.Add(proveedor);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Mostrar mensaje de error en caso de excepción
                MessageBox.Show($"Error al cargar los proveedores: {ex.Message}");
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            // Obtener los valores de los TextBox
            string nombre = textBox1.Text.Trim();
            string direccion = textBox2.Text.Trim();
            string contacto = textBox3.Text.Trim();
            string telefono = textBox4.Text.Trim();

            // Validar que todos los campos estén llenos
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(direccion) ||
                string.IsNullOrWhiteSpace(contacto) || string.IsNullOrWhiteSpace(telefono))
            {
                MessageBox.Show("Por favor, llena todos los campos antes de guardar.");
                return;
            }

            // Llamar al método para guardar el proveedor
            GuardarProveedor(nombre, direccion, contacto, telefono);
            Form1 form1 = new Form1();

            // Mostrar el nuevo formulario
            form1.Show();

            // Cerrar el formulario actual
            this.Close();
        }

        private void GuardarProveedor(string nombre, string direccion, string contacto, string telefono)
        {
            // Consulta SQL para insertar un proveedor
            string query = "INSERT INTO Proveedores (Nombre, Direccion, Contacto, Telefono) VALUES (@Nombre, @Direccion, @Contacto, @Telefono)";

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
                        // Agregar los parámetros a la consulta
                        command.Parameters.AddWithValue("@Nombre", nombre);
                        command.Parameters.AddWithValue("@Direccion", direccion);
                        command.Parameters.AddWithValue("@Contacto", contacto);
                        command.Parameters.AddWithValue("@Telefono", telefono);

                        // Ejecutar el comando
                        command.ExecuteNonQuery();

                        MessageBox.Show("Proveedor guardado exitosamente.");
                    }
                }
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

                // Extraer el ID del proveedor del texto seleccionado
                // Supongamos que el formato es "ID: 1, Nombre: Proveedor1, Dirección: ..."
                string idProveedor = seleccion.Split(',')[0].Replace("ID: ", "").Trim();

                // Consulta SQL para eliminar
                string query = "DELETE FROM Proveedores WHERE ID = @ID";

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
                            // Agregar el parámetro ID
                            command.Parameters.AddWithValue("@ID", idProveedor);

                            // Ejecutar el comando
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Proveedor eliminado exitosamente.");
                            }
                            else
                            {
                                MessageBox.Show("No se encontró el proveedor.");
                            }
                        }
                    }

                    // Actualizar la lista después de eliminar
                    CargarProveedores();
                }
                catch (Exception ex)
                {
                    // Mostrar mensaje de error en caso de excepción
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un proveedor para eliminar.");
            }
        }
    }
}
