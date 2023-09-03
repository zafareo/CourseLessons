using System.ComponentModel.DataAnnotations;

namespace EmployeeMVC.Enities;

public class Employee
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    [StringLength(50, ErrorMessage = "Name should be less than or equal to 50 characters.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Birth date is required.")]
    [Display(Name = "Birth Date")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime BirthDate { get; set; }

    [Required(ErrorMessage = "Picture URL is required.")]
    [Url(ErrorMessage = "Invalid Picture URL.")]
    public string Picture { get; set; }

    [Required(ErrorMessage = "Profession is required.")]
    [EnumDataType(typeof(Profession), ErrorMessage = "Invalid profession.")]
    public Profession Profession { get; set; }
}