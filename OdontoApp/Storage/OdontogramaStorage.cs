using OdontoApp.Model.Odontograma;
using System.Text.Json;

namespace OdontoApp.Storage
{
    public class OdontogramaStorage
    {
        private static string ruta = Path.Combine("Data", "odontogramas.json");

        public static List<OdontogramaPaciente> LeerTodos()
        {
            if (!File.Exists(ruta)) return new();
            var json = File.ReadAllText(ruta);
            return JsonSerializer.Deserialize<List<OdontogramaPaciente>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new();
        }

        public static void GuardarOdontograma(string documento, OdontogramaEntrada nuevoOdontograma)
        {
            var todos = LeerTodos();

            var paciente = todos.FirstOrDefault(p => p.Documento == documento);
            if (paciente == null)
            {
                paciente = new OdontogramaPaciente
                {
                    Documento = documento,
                    Odontogramas = new List<OdontogramaEntrada> { nuevoOdontograma }
                };
                todos.Add(paciente);
            }
            else
            {
                var existente = paciente.Odontogramas.FirstOrDefault(o => o.Fecha == nuevoOdontograma.Fecha);
                if (existente != null)
                {
                    paciente.Odontogramas.Remove(existente); // reemplazar
                }
                paciente.Odontogramas.Add(nuevoOdontograma);
            }

            var jsonActualizado = JsonSerializer.Serialize(todos, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(ruta, jsonActualizado);
        }

        public static List<OdontogramaEntrada> BuscarPorDocumento(string documento)
        {
            var todos = LeerTodos();
            return todos.FirstOrDefault(p => p.Documento == documento)?.Odontogramas ?? new();
        }
    }
}
