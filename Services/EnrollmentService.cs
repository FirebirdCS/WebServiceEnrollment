using Web_Final.Models;
using Web_Final.Services;
using System.Data.SqlClient;
using System.Data;

namespace Web_Final.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private SqlConnection connection = new SqlConnection("Server=localhost;Database=Kalum_Test;User Id=sa;Password=Inicio.2022;");
        private List<Aspirante> aspirantes = new List<Aspirante>();
        private List<CarreraTecnica> carreras = new List<CarreraTecnica>();

        private List<Cargo> cargos = new List<Cargo>();

        public EnrollmentService()
        {
            carreras.Add(new CarreraTecnica() { CarreraId = "1", Carrera = "Fullstack Dotnet Core" });
            carreras.Add(new CarreraTecnica() { CarreraId = "2", Carrera = "Fullstack Java Development" });
            carreras.Add(new CarreraTecnica() { CarreraId = "3", Carrera = "Fullstack Javascript Angular" });
            cargos.Add(new Cargo()
            {
                CargoId = "1",
                Descripcion = "Pago por inscripcion",
                Prefijo = "INSC",
                Monto = 1200.00,
                IsGeneraMora = false,
                PorcentajeMora = 0
            });
            cargos.Add(new Cargo()
            {
                CargoId = "2",
                Descripcion = "Pago de carné",
                Prefijo = "CARNE",
                Monto = 100.00,
                IsGeneraMora = false,
                PorcentajeMora = 0
            });
            cargos.Add(new Cargo()
            {
                CargoId = "3",
                Descripcion = "Pago mensual de colegiatura",
                Prefijo = "COL",
                Monto = 600.00,
                IsGeneraMora = false,
                PorcentajeMora = 0
            });
        }

        public EnrollmentResponse EnrollmentProcess(EnrollmentRequest request)
        {
            EnrollmentResponse respuesta = null;
            Aspirante aspirante = buscarAspirante(request.NoExpediente);
            if (aspirante == null)
            {
                respuesta = new EnrollmentResponse() { Codigo = 204, Respuesta = $"No existe el aspirante con el número de expediente {request.NoExpediente}" };
            }
            else
            {
                respuesta = EjecutarProcedimiento(request);
            }

            return respuesta;
        }

        public string Test(string s1)
        {
            Console.WriteLine("Test method executed");
            return s1;
        }

        private EnrollmentResponse EjecutarProcedimiento(EnrollmentRequest request)
        {
            EnrollmentResponse respuesta = null;
            SqlCommand cmd = new SqlCommand("sp_EnrollmentProcess", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@NoExpediente", request.NoExpediente));
            cmd.Parameters.Add(new SqlParameter("@Ciclo", request.Ciclo));
            cmd.Parameters.Add(new SqlParameter("@MesInicioPago", request.MesInicioPago));
            cmd.Parameters.Add(new SqlParameter("@CarreraId", request.CarreraId));
            try
            {
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
                respuesta = new EnrollmentResponse() { Codigo = 201, Respuesta = "Enrollment success!" };
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                respuesta = new EnrollmentResponse() { Codigo = 503, Respuesta = "Error en el servicio!" };
            }
            finally
            {
                connection.Close();
            }

            return respuesta;
        }

        private Aspirante buscarAspirante(string noExpediente)
        {
            Aspirante resultado = null;

            SqlDataAdapter daAspirante = new SqlDataAdapter($"SELECT * FROM Aspirante a WHERE a.NoExpediente = '{noExpediente}';", connection);
            DataSet dsAspirante = new DataSet();
            daAspirante.Fill(dsAspirante, "Aspirante");
            if (dsAspirante.Tables["Aspirante"].Rows.Count > 0)
            {
                resultado = new Aspirante()
                {
                    NoExpediente = dsAspirante.Tables["Aspirante"].Rows[0][0].ToString(),
                    Apellidos = dsAspirante.Tables["Aspirante"].Rows[0][1].ToString(),
                    Nombres = dsAspirante.Tables["Aspirante"].Rows[0][2].ToString(),
                    Direccion = dsAspirante.Tables["Aspirante"].Rows[0][3].ToString(),
                    Telefono = dsAspirante.Tables["Aspirante"].Rows[0][4].ToString(),
                    Email = dsAspirante.Tables["Aspirante"].Rows[0][5].ToString(),
                    Estatus = dsAspirante.Tables["Aspirante"].Rows[0][6].ToString(),
                    CarreraId = dsAspirante.Tables["Aspirante"].Rows[0][7].ToString(),
                    JornadaId = dsAspirante.Tables["Aspirante"].Rows[0][8].ToString(),
                };
            }


            return resultado;
        }
    }
}