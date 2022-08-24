using Gizmo.DAL.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Gizmo.DAL.Mappings
{
    /// <summary>
    /// Deposit payment intent map.
    /// </summary>
    public class PaymentIntentDepositMap : EntityTypeConfiguration<PaymentIntentDeposit>
    {
        /// <summary>
        /// Creates new instance.
        /// </summary>
        public PaymentIntentDepositMap()
        {    
            ToTable(nameof(PaymentIntentDeposit));
        }
    }
}
