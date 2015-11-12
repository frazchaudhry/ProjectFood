using Microsoft.AspNet.Identity.EntityFramework;

namespace ProjectFood.Domain.Entities
{
    public class User : IdentityUser
    {
        public string AboutMe { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
    }
}
