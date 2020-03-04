module CardGroupListView

open Fabulous

// Model
type Model = {
    groups : CardGroupView.Model list option
}

// Message
type Msg =
    | CardGroupSelected of bool
    | AddNewCardGroup of bool
    
// Init 
let init () = { groups = None }, Cmd.none


// update
let update msg model =
    match msg with
    | CardGroupSelected selected -> { groups = None }, Cmd.none
    | AddNewCardGroup clicked -> { groups = None }, Cmd.none



