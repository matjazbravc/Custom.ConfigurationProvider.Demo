using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Custom.Configuration.Provider.Demo.Data.Entities
{
    [Table("AppCustomSettings")]
    public class AppSettingsCustomEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Description("CustomSetting A")]
        [Column("CustomSettingA", TypeName = "nvarchar(512)")]
        public string CustomSettingA { get; set; }

        [Description("CustomSetting B")]
        [Column("CustomSettingB", TypeName = "nvarchar(512)")]
        public string CustomSettingB { get; set; }

        [Column("Default", TypeName = "bit")]
        public bool Default { get; set; } = true;
    }
}