using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }
        public DateTime DOB { get; set; }
        public int RoleId { get; set; }
        public string ImageFile { get; set; }
        public string Address { get; set; }
        public int Zipcode { get; set; }
        public int StateId { get; set; }
        public int CountryId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted{ get; set;}
    }
}
