using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalProduction.Maui.Validation;

internal abstract class FileExistsBase : ValidationRuleBase<string>
{
public List<string>? Values { get; set; }
}
