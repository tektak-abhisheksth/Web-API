using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Model.Common
{
    public class KvPair<TK, TV>
    {
        [Required]
        [Description("The required key.")]
        public TK K { get; set; }
        [Description("The optional value.")]
        public TV V { get; set; }
    }

    public class GeneralKvPair<TK, TV>
    {
        [Required]
        [Description("The required key.")]
        public TK Id { get; set; }

        [Description("The optional value.")]
        public TV Value { get; set; }
    }
}
