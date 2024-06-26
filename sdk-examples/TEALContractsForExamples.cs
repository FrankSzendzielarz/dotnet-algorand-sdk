﻿namespace sdk_examples
{
    /// <summary>
    /// Helper class for accessing the TEAL Examples
    /// </summary>
    static class TEALContractsForExamples
    {
        public static string Sample()
        {
            return "int 1";
        }

        public static string SimpleBox()
        {
            return @"#pragma version 8
                    txn ApplicationID
                    int 0
                    ==
                    bnz exit
                    byte ""boxtest""                     
                    byte ""teststring""
                    box_put
                exit:
                    int 1
                    ";
        }

        public static string SimpleBoxClear()
        {
            return @"#pragma version 8
                    int 1";
        }

        public static string HelloWorld()
        {
            return @"#pragma version 2
                    byte ""counter""
                    byte ""counter""
                    app_global_get
                    int 1
                    +
                    app_global_put
                    byte ""counter""
                    app_global_get
                    return";
        }

        public static string HelloWorldClear()
        {
            return @"#pragma version 2
                    int 1";
        }

        public static string HelloWorldUpdated()
        {
            return @"#pragma version 2
                    byte ""counter""
                    byte ""counter""
                    app_global_get
                    int 1
                    +
                    app_global_put
                    int 0
                    byte ""localcounter""
                    int 0
                    byte ""localcounter""
                    app_local_get
                    int 1
                    +
                    app_local_put
                    int 0
                    byte ""localcounter""
                    app_local_get
                    return";
        }

        public static string StatefulApprovalInit(string creatorAddress)
        {
            return $@"#pragma version 8
                    ///// Handle each possible OnCompletion type. We don't have to worry about
                    //// handling ClearState, because the ClearStateProgram will execute in that
                    //// case, not the ApprovalProgram.
                    txn OnCompletion
                    int NoOp
                    ==
                    bnz handle_noop
                    txn OnCompletion
                    int OptIn
                    ==
                    bnz handle_optin
                    txn OnCompletion
                    int CloseOut
                    ==
                    bnz handle_closeout
                    txn OnCompletion
                    int UpdateApplication
                    ==
                    bnz handle_updateapp
                    txn OnCompletion
                    int DeleteApplication
                    ==
                    bnz handle_deleteapp
                    //// Unexpected OnCompletion value. Should be unreachable.
                    err
                    handle_noop:
                    //// Handle NoOp
                    //// Check for creator
                    addr {creatorAddress}
                    txn Sender
                    ==
                    bnz handle_optin
                    //// read global state
                    byte ""counter""
                    dup
                    app_global_get
                    //// increment the value
                    int 1
                    +
                    //// store to scratch space
                    dup
                    store 0
                    //// update global state
                    app_global_put
                    //// read local state for sender
                    int 0
                    byte ""counter""
                    app_local_get
                    //// increment the value
                    int 1
                    +
                    store 1
                    //// update local state for sender
                    int 0
                    byte ""counter""
                    load 1
                    app_local_put
                    //// load return value as approval
                    load 0
                    return
                    handle_optin:
                    //// Handle OptIn
                    //// approval
                    int 1
                    return
                    handle_closeout:
                    //// Handle CloseOut
                    ////approval
                    int 1
                    return
                    handle_deleteapp:
                    //// Check for creator
                    addr {creatorAddress}
                    txn Sender
                    ==
                    return
                    handle_updateapp:
                    //// Check for creator
                    addr {creatorAddress}
                    txn Sender
                    ==
                    return";
        }

        public static string StatefulApprovalRefact(string creatorAddress)
        {
            return $@"#pragma version 8
                    //// Handle each possible OnCompletion type. We don't have to worry about
                    //// handling ClearState, because the ClearStateProgram will execute in that
                    //// case, not the ApprovalProgram.
                    txn OnCompletion
                    int NoOp
                    ==
                    bnz handle_noop
                    txn OnCompletion
                    int OptIn
                    ==
                    bnz handle_optin
                    txn OnCompletion
                    int CloseOut
                    ==
                    bnz handle_closeout
                    txn OnCompletion
                    int UpdateApplication
                    ==
                    bnz handle_updateapp
                    txn OnCompletion
                    int DeleteApplication
                    ==
                    bnz handle_deleteapp
                    //// Unexpected OnCompletion value. Should be unreachable.
                    err
                    handle_noop:
                    //// Handle NoOp
                    //// Check for creator
                    addr {creatorAddress}
                    txn Sender
                    ==
                    bnz handle_optin
                    //// read global state
                    byte ""counter""
                    dup
                    app_global_get
                    //// increment the value
                    int 1
                    +
                    //// store to scratch space
                    dup
                    store 0
                    //// update global state
                    app_global_put
                    //// read local state for sender
                    int 0
                    byte ""counter""
                    app_local_get
                    //// increment the value
                    int 1
                    +
                    store 1
                    //// update local state for sender
                    //// update ""counter""
                    int 0
                    byte ""counter""
                    load 1
                    app_local_put
                    //// update ""timestamp""
                    int 0
                    byte ""timestamp""
                    txn ApplicationArgs 0
                    app_local_put
                    //// load return value as approval
                    load 0
                    return
                    handle_optin:
                    //// Handle OptIn
                    //// approval
                    int 1
                    return
                    handle_closeout:
                    //// Handle CloseOut
                    ////approval
                    int 1
                    return
                    handle_deleteapp:
                    //// Check for creator
                    addr {creatorAddress}
                    txn Sender
                    ==
                    return
                    handle_updateapp:
                    //// Check for creator
                    addr {creatorAddress}
                    txn Sender
                    ==
                    return";
        }

        public static string StatefulClear()
        {
            return @"#pragma version 8
                    int 1";
        }
    }
}
