using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Domain.Constants;

public class DataAnnotationMessages
{

    public const string REQUIRED = "O campo {0} é obrigatório.";

    public const string STRLEN = "O campo {0} deve ter entre {2} e {1} caracteres.";

    public const string STRLEN_MAX = "O campo {0} deve ter no máximo {1} caracteres.";

    public const string STRLEN_MIN = "O campo {0} deve ter no mínimo {1} caracteres.";

    public const string DECIMAL_RANGE = "O campo {0} deve ser um número decimal entre {1} e {2}.";


}
