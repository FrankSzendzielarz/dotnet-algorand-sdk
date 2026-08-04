using Algorand.Algod.Model;
using Algorand.Algod.Model.Transactions;

namespace test
{
    /// <summary>
    /// Shared helper for tests that compare a block fetched via algod's JSON API against the same block fetched
    /// via msgpack (algod REST/gossip). A transaction's global/local state delta ("gd"/"ld") can hold arbitrary
    /// binary data in both its keys (state/box key names) and its ValueDelta.Bytes ("bs") values; algod's own
    /// JSON API is lossy for both - non-UTF8 byte sequences get replaced with U+FFFD server-side before the
    /// response ever reaches a client (confirmed by fetching a block's raw JSON directly: state delta "bs" values
    /// containing invalid UTF-8 literally contain U+FFFD in the JSON text). That's an algod-side JSON API
    /// limitation, not something a client can recover from - so byte-perfect JSON-vs-msgpack equality for a block
    /// containing such deltas is unachievable via full JSON comparison. This drops those specific lossy
    /// collections (on both sides, symmetrically) before comparison, the same way
    /// BlockFetcherTests.DeltaValueWorks55983440* already avoids comparing full block JSON and instead asserts
    /// against the (lossless) msgpack-decoded field values directly.
    /// </summary>
    internal static class BlockComparisonHelpers
    {
        public static void ClearLossyStateDeltaBytes(CertifiedBlock block)
        {
            if (block?.Block?.Transactions == null) return;
            foreach (var txn in block.Block.Transactions)
            {
                ClearLossyStateDeltaBytes(txn);
            }
        }

        private static void ClearLossyStateDeltaBytes(SignedTransaction txn)
        {
            if (txn?.Detail == null) return;

            txn.Detail.GlobalDelta = null;
            txn.Detail.LocalDelta = null;

            if (txn.Detail.InnerTxns == null) return;
            foreach (var inner in txn.Detail.InnerTxns)
            {
                ClearLossyStateDeltaBytes(inner);
            }
        }
    }
}
