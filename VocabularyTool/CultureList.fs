module CultureList

open Fabulous.XamarinForms
open Xamarin.Forms

let view (model : Culture.Model list) (dispatch:Culture.Msg -> unit) =
    let views =
        model |> List.map (fun (x) -> Culture.view x (dispatch))
    View.StackLayout(orientation = StackOrientation.Horizontal,
        heightRequest = 42.,
        children = views)