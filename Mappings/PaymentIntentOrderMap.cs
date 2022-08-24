using Gizmo.DAL.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Order payment intent map.
    /// </summary>
    public class PaymentIntentOrderMap : EntityTypeConfiguration<PaymentIntentOrder>
    {
        /// <summary>
        /// Creates new instance.
        /// </summary>
        public PaymentIntentOrderMap()
        {    
            ToTable(nameof(PaymentIntentOrder));
        }
    }
}
