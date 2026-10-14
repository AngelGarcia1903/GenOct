using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P04R01_APIREST.Models
{
    public class TBLUsuarios
    {
        [Key]
        public int idUsuarios { get; set; }
        public string nombre { get; set; }
        public string ? hobby{ get; set; }
        public DateTime fechanacimiento { get; set; }
        public string email { get; set; }

        //---------- Columnas del registro Thona (script P04R01_BD_AlterTable_Registro.sql) ----------
        //Son opcionales: los usuarios creados desde el CRUD no las llenan.
        public string? idUsuarioGenerado { get; set; }
        public string? preposiciones { get; set; }   //separadas por coma: "de,la,los"
        public string? cp { get; set; }
        public string? codEstado { get; set; }
        public string? estado { get; set; }
        public string? municipio { get; set; }
        public string? codColonia { get; set; }
        public string? colonia { get; set; }

        //La pone SQL Server (DEFAULT GETDATE()); EF Core no la envía al insertar ni al actualizar
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? fechaRegistro { get; set; }

    }//fin de la clase
}//fin del namespace
