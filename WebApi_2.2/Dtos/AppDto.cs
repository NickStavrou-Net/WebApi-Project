using System;

namespace WebApi.Dtos
{
    public class AppDto
    {
        public string Name { get; set; }
        public DateTime TokenExpiration { get; set; }
    }
}