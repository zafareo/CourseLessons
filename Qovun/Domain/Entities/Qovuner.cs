using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

[Table("qovuners")]
public class Qovuner
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Project> Projects { get; set; }
}
