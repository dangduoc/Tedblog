namespace MyBlog.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GalleryHome")]
    public partial class GalleryHome
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [StringLength(500)]
        public string url { get; set; }

        [StringLength(500)]
        public string Title { get; set; }

        [Column(TypeName = "date")]
        public DateTime? dateCreate { get; set; }
    }
}
