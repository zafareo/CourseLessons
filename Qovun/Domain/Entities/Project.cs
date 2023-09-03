using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

[Table("projects")]
public class Project
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Qovuner> Qovuners { get; set;}
}
