using OdontoApp.Model.Paciente;
using System.Text.Json;

namespace OdontoApp.Storage
{
    public static class PacienteStorage
    {
        private static string filePath = "Data/pacientes.json";

        public static List<Paciente> ObtenerTodos()
        {
            if (!File.Exists(filePath))
                return new List<Paciente>();

            var json = File.ReadAllText(filePath);

            var opciones = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<List<Paciente>>(json, opciones) ?? new List<Paciente>();
        }

        public static void GuardarTodos(List<Paciente> pacientes)
        {
            string json = JsonSerializer.Serialize(pacientes, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }

        public static void AgregarPaciente(Paciente nuevo)
        {
            var pacientes = ObtenerTodos();
            nuevo.Id = pacientes.Count > 0 ? pacientes.Max(p => p.Id) + 1 : 1;
            pacientes.Add(nuevo);
            GuardarTodos(pacientes);
        }

        public static Paciente ObtenerPorId(int id)
        {
            return ObtenerTodos().FirstOrDefault(p => p.Id == id);
        }
    }
}