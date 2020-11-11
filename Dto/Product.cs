using System;

namespace RestfulAPI.Dto
{
    /// <summary>
    /// DTO for reading product (-s)
    /// </summary>

    public class Product
    {
        /// <summary>
        /// Product id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Product name
        /// </summary>
        /// <example>lime</example>
        public string Name { get; set; }
        public string Company { get; set; }
        public string Status { get; set; }
        public int Activity { get; set; }
        public Representative Representative { get; set; }
        public DateTime Date { get; set; }
        public Country Country { get; set; }
    }

    public class Country
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }

    public class Representative
    {
        public string Name { get; set; }
        public string Image { get; set; }
    }

}
