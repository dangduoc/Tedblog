namespace MyBlog.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SlideImage")]
    public partial class SlideImage
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [StringLength(500)]
        public string url { get; set; }

        [StringLength(100)]
        public string title { get; set; }

        [Column(TypeName = "date")]
        public DateTime? date { get; set; }
    }
}
