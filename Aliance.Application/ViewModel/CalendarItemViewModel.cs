using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliance.Application.ViewModel;

public class CalendarItemViewModel
{
    public string Title { get; set; }          // Nome do evento ou culto
    public DateTime Date { get; set; }        // Data/hora inicial
    public string Type { get; set; }           // "Evento" ou "Culto"
    public Guid? ReferenceId { get; set; }     // ID do evento/culto (para navegação ou edição)
}
