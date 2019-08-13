module ColorList

open Color
open Fabulous.XamarinForms
open Xamarin.Forms


let view (model : Color.Model list) (dispatch:Msg -> unit) =
    let views =
        model |> List.map (fun (x) -> Color.view x (dispatch))
    View.StackLayout(orientation = StackOrientation.Horizontal,
        children = views)