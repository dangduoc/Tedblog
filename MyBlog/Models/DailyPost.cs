namespace MyBlog.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DailyPost")]
    public partial class DailyPost
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int postID { get; set; }

        [Required]
        [StringLength(100)]
        public string postTitle { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string postContent { get; set; }

        [Column(TypeName = "date")]
        public DateTime dateWrite { get; set; }

        [StringLength(500)]
        public string metaImage { get; set; }
    }
}
